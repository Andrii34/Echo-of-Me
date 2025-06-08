using System;
using UnityEngine;

public interface IPlayerIInput
{
    public event Action OnStartCharging;
   
    public event Action OnShot;
}
