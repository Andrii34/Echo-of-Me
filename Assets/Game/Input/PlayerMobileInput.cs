using System;
using UnityEngine;

public class PlayerMobileInput : IPlayerIInput
{
    public event Action OnStartCharging;
    
    public event Action OnShot;

    PlayerMobileControlsAction _playerInput;
    public PlayerMobileInput()
    {
        _playerInput = new PlayerMobileControlsAction();
    }
    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Shot.started += ctx => OnStartCharging?.Invoke();
        _playerInput.Player.Shot.canceled += ctx => OnShot?.Invoke();
    }

}
