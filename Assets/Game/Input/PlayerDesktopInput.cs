using System;
using UnityEngine;

public class PlayerDesktopInput: IPlayerIInput
{
    private PlayerDesktopInputAction _desktopInput;

    public PlayerDesktopInput()
    {
        _desktopInput = new PlayerDesktopInputAction();
    }

    public event Action OnStartCharging;
    public event Action OnShot;
    public void Enable()
    {
        _desktopInput.Enable();
        _desktopInput.Player.Shoot.started += ctx => OnStartCharging?.Invoke();
        _desktopInput.Player.Shoot.canceled += ctx => OnShot?.Invoke();
    }
}
