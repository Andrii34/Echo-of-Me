using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public event Action OnMinimalSize;
    [SerializeField] private float _minimalSize =5f;
    [SerializeField] private float _startSizeProjectile;
    [SerializeField] private Transform _projectilePoint;
    [SerializeField] private float _energyDeliveryRate;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _startProjectileSize =1;
    [SerializeField] private Transform Gate;
    [SerializeField] private ContagiousInfection _contagiousInfection;
    [SerializeField] private DeathInfection _deathInfection;
    
    
    private IPlayerIInput _playerIInput;
    private Shoot _shoot;
    private DeathInfection _infection;

    [Inject]
    private void Construct(IPlayerIInput playerIInput)
    {
        _playerIInput = playerIInput;
        _shoot = new Shoot();  
    }
    private void Start()
    {
        _playerIInput.OnStartCharging += StartCharging;
        _playerIInput.OnCharging += Charging;
        _playerIInput.OnShot += Shoot;
        _infection =Instantiate(_contagiousInfection);
    }
   
    private void Update()
    {
        transform.LookAt(Gate);
        _projectilePoint.LookAt(Gate);
    }
    private void SetProjectilePoint()
    {
        Vector3 forwardPoint = transform.forward*transform.localScale.z/2;
        Vector3 position = transform.position + forwardPoint;
        _projectilePoint.position = position;
        _projectilePoint.rotation = transform.rotation;


    }
    private void Shoot()
    {
        Debug.Log("Shoot");
        _shoot.ReleaseShot();
        
    }

    private void Charging()
    {
        if(transform.localScale.x <= _minimalSize)
        {
            OnMinimalSize?.Invoke();
            return;
        }
        Debug.Log("Charging");
        float energyDelivery = _energyDeliveryRate * Time.deltaTime;
        transform.localScale -= Vector3.one * energyDelivery;
        _shoot.UpdateCharging(energyDelivery);

    }

    private void StartCharging()
    {
        Debug.Log("StartCharging");
        SetProjectilePoint();
        _shoot.StartCharging(_startSizeProjectile, _infection, _projectilePoint, _projectile);
        _shoot.UpdateCharging(_startProjectileSize);
        transform.localScale -= Vector3.one * _startProjectileSize;
    }


    public void SetDeathInfection()
    {
        _infection = Instantiate(_deathInfection);
    }
    public void SetContaminationInfection()
    {
        _infection = Instantiate(_contagiousInfection);
    }
}


