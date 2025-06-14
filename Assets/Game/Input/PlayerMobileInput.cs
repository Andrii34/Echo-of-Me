using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using Zenject;



using System;
using UnityEngine;
using Zenject;

public class PlayerMobileInput : IPlayerIInput, ILateTickable, IInitializable, IDisposable
{
    private readonly ITouchInputService _touchInputService;

    private bool _isCharging = false;
    private bool _enabled = false;

    public event Action OnStartCharging;
    public event Action OnCharging;
    public event Action OnShot;

    [Inject]
    public PlayerMobileInput(ITouchInputService touchInputService)
    {
        _touchInputService = touchInputService;
    }

    public void Initialize()
    {
        Enable();
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
        _isCharging = false;
    }

    public void Dispose()
    {
        Disable();
    }

    public void LateTick()
    {
        if (!_enabled)
            return;

        HandleInput();
    }

    private void HandleInput()
    {
        if (!_touchInputService.TryGetTouch(out var touch))
            return;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                _isCharging = true;
                OnStartCharging?.Invoke();
                break;

            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                if (_isCharging)
                {
                    OnCharging?.Invoke();
                }
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                _isCharging = false;
                OnShot?.Invoke();
                break;
        }
    }
}


