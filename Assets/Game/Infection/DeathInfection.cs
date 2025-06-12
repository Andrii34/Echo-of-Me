using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathInfection", menuName = "Infection/DeathInfection")]
public class DeathInfection :ScriptableObject, IInfection
{
    [SerializeField] private InfectionConfigs _infectionConfigs;
    private InfectionTimer _infectionTimer;
    private ColorChangeProcess _colorProcess;
    protected Infectable _infectable;
    public event Action OnInfectionEnd;



   

    public void StartInfection(Infectable infectable)
    {
        _infectionTimer = new InfectionTimer();
        
        _infectable = infectable;
        
        _colorProcess = new ColorChangeProcess(_infectable.Renderer, _infectionConfigs.Color, _infectionConfigs.TimeToDeath);
        _infectionTimer.StartInfection(_infectionConfigs.TimeToDeath);
        _infectionTimer.AddProcess(_colorProcess);
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
