using System;
using System.Collections.Generic;
using UnityEngine;

public class InfectionTimer 
{
    private float _timeLeft;
    private List<IInfectionProcess> _processes = new List<IInfectionProcess>();
    private bool _isRunning = false;

    public event Action OnTimerFinished;
    public void StartInfection(float duration)
    {
        _timeLeft = duration;
        _isRunning = true;
    }

    public void AddProcess(IInfectionProcess process)
    {
        _processes.Add(process);
    }

    public void RemoveProcess(IInfectionProcess process)
    {
        _processes.Remove(process);
    }

    public void Update()
    {
        if (!_isRunning)
            return;

        float deltaTime = Time.deltaTime;
        _timeLeft -= deltaTime;

        foreach (var process in _processes)
        {
            process.UpdateProcess(deltaTime);
        }

        if (_timeLeft <= 0)
        {
            _isRunning = false;
            foreach (var process in _processes)
            {
                process.OnInfectionEnd();
            }
            _processes.Clear();
            OnTimerFinished?.Invoke();
        }
    }
}
