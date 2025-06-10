using System;
using UnityEngine;

public interface IPlayerIInput
{
    public event Action OnStartCharging;
   public event Action OnCharging;
    public event Action OnShot;
}
