using System;
using UnityEngine;

public class ContagiousInfection :DeathInfection
{
   [SerializeField ] private ContagiousInfectionConfigs _infectionConfigs;
    private Contamination _contamination;
    protected override void InfectionEnd()
    {
        base.InfectionEnd();
        _contamination = new Contamination(new DeathInfection(), _infectionConfigs.ContagionRadius,_infectionConfigs.InfectionLayer);
        _contamination.StartContamination(_infectable.gameObject.transform.position);

    }

}
