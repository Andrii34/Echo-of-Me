using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerDesktopInput : IPlayerIInput,  ITickable, IInitializable, IDisposable
{
    private readonly PlayerDesktopInputAction _desktopInput;

    private bool _isCharging = false;

    public event Action OnStartCharging;
    public event Action OnCharging;
    public event Action OnShot;

    public PlayerDesktopInput()
    {
        _desktopInput = new PlayerDesktopInputAction();
    }

    public void Initialize()
    {
        Enable();
    }

    public void Enable()
    {
        _desktopInput.Enable();

        _desktopInput.Player.Shoot.started += OnShootStarted;
        _desktopInput.Player.Shoot.canceled += OnShootCanceled;
    }

    public void Disable()
    {
        _desktopInput.Player.Shoot.started -= OnShootStarted;
        _desktopInput.Player.Shoot.canceled -= OnShootCanceled;

        _desktopInput.Disable();
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
