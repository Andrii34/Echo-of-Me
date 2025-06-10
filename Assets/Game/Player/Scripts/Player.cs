using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private float _startSizeProjectile;
    [SerializeField] private Transform _projectilePoint;
    [SerializeField] private float _energyDeliveryRate;
    private IPlayerIInput _playerIInput;
    private Shoot _shoot;
    private IInfection _infection;

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
        _infection =new DeathInfection();
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
        _shoot.ReleaseShot();
    }

    private void Charging()
    {
         float energyDelivery = _energyDeliveryRate * Time.deltaTime;
        transform.localScale -= Vector3.one * energyDelivery;
        _shoot.UpdateCharging(energyDelivery);
    }

    private void StartCharging()
    {
        _shoot.StartCharging(_startSizeProjectile,_infection , _projectilePoint);
    }
    
    
    public void SetDeathInfection()
    {
        _infection = new DeathInfection();
    }
    public void SetContaminationInfection()
    {
        _infection = new ContagiousInfection();
    }
}


