using System;
using System.Collections;
using UnityEngine;

public interface IInfection
{
    void Update();
    void StartInfection(Infectable infectable);

    event Action OnInfectionEnd;
}
