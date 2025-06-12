using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMobileInput : IPlayerIInput, ITickable, IInitializable, IDisposable
{
    private readonly PlayerMobileControlsAction _playerMobileInput;

    private bool _isCharging = false;

    public event Action OnStartCharging;
    public event Action OnCharging;
    public event Action OnShot;

    public PlayerMobileInput()
    {
        _playerMobileInput = new PlayerMobileControlsAction();
    }  

    public void Initialize()
    {
        Enable();
    }

    public void Enable()
    {
        _playerMobileInput.Enable();

        _playerMobileInput.Player.Shot.started += OnShootStarted;
        _playerMobileInput.Player.Shot.canceled += OnShootCanceled;
    }

    public void Disable()
    {
        _playerMobileInput.Player.Shot.started -= OnShootStarted;
        _playerMobileInput.Player.Shot.canceled -= OnShootCanceled;

        _playerMobileInput.Disable();
    }

    public void Dispose()
    {
        Disable();
    }

    public void Tick()
    {
        if (_isCharging)
        {
            OnCharging?.Invoke();
        }
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        _isCharging = true;
        OnStartCharging?.Invoke();
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        _isCharging = false;
        OnShot?.Invoke();
    }

}
