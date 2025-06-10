using System;
using UnityEngine;

public class Shoot
{
    private readonly Transform _shootPoint;
    private readonly GameObject _projectilePrefab;

    private float _chargeTime;
    private bool _isCharging;

    public event Action<float> OnShotFired;

    public Shoot(Transform shootPoint, GameObject projectilePrefab)
    {
        _shootPoint = shootPoint;
        _projectilePrefab = projectilePrefab;
    }

    public void StartCharging()
    {
        _isCharging = true;
        _chargeTime = 0f;
    }

    public void UpdateCharging(float deltaTime)
    {
        if (_isCharging)
            _chargeTime += deltaTime;
    }

    public void ReleaseShot()
    {
        if (!_isCharging) return;

        _isCharging = false;

        var projectile = UnityEngine.Object.Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
       // projectile.GetComponent<Projectile>().Init(_chargeTime);

        OnShotFired?.Invoke(_chargeTime);
    }
}
