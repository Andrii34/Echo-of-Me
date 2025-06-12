using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ContagiousInfection", menuName = "Infection/ContagiousInfection")]
public class ContagiousInfection :DeathInfection
{
   [SerializeField ] private ContagiousInfectionConfigs _infectionConfigs;
    [SerializeField] private DeathInfection _deathInfection;
    [SerializeField] private GameObject _sphereEffect;
    private Contamination _contamination;
    
    
    

    protected override void InfectionEnd()
    {
        base.InfectionEnd();
        _contamination = new Contamination(Instantiate(_deathInfection), _infectionConfigs.ContagionRadius,_infectionConfigs.InfectionLayer);
        _contamination.StartContaminationWithVisualize(_sphereEffect,_infectable.gameObject.transform.position);

    }
    

}
