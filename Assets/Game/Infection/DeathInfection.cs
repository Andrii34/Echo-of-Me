using System;
using UnityEngine;

public class DeathInfection : IInfection
{
    [SerializeField] private InfectionConfigs _infectionConfigs;
    private InfectionTimer _infectionTimer;
    private ColorChangeProcess _colorProcess;
    protected Infectable _infectable;
    public event Action OnInfectionEnd;

    public DeathInfection()
    {
        
        _infectionTimer = new InfectionTimer();
    }
    public void StartInfection(Infectable infectable)
    {
        _infectable = infectable;
        _infectable.ApplyInfection(this);
        _colorProcess = new ColorChangeProcess(_infectable.Renderer, _infectionConfigs.Color, _infectionConfigs.TimeToDeath);
        _infectionTimer.StartInfection(_infectionConfigs.TimeToDeath);
        _infectionTimer.OnTimerFinished += InfectionEnd;
        
    }
    public void Update()
    {
        _infectionTimer.Update();

    }
    
    protected virtual void InfectionEnd()
    {
        OnInfectionEnd?.Invoke();
        _infectable.Death();
        _infectionTimer.OnTimerFinished -= InfectionEnd;
    }
   
    
}
