using System;
using UnityEngine;

public class Shoot
{
    private  Transform _shootPoint;
    
    [SerializeField] private GameObject _projectilePrefab;
    
    private bool _isCharging;
    private Projectile _projectile;



    

    public void StartCharging(float startSize,DeathInfection infection,Transform shotPoint, GameObject projectilePrefab)
    {
        _shootPoint = shotPoint;
        _isCharging = true;
        
        GameObject projectile = GameObject.Instantiate(projectilePrefab, _shootPoint.position, _shootPoint.rotation);
        projectile.transform.localScale = Vector3.one * startSize;
        _projectile = projectile.GetComponent<Projectile>();
        Debug.Log(_projectile+"!!!!!!!!!!!!AAAAAAAAAAAAAAAAAAAaaaaaaa");
        _projectile.SetInfection(infection);
    }

    public void UpdateCharging(float sazeIndex)
    {
        _projectile.Increase(sazeIndex);
    }

    public void ReleaseShot()
    {
   
        _projectile.StartMove();
    }
}
