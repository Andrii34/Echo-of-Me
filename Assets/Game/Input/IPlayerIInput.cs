using System;
using UnityEngine;

public interface IPlayerIInput
{
    public void Enable();
    public void Disable();
    public event Action OnStartCharging;
   public event Action OnCharging;
    public event Action OnShot;
}
