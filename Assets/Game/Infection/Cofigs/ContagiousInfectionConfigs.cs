using UnityEngine;

[CreateAssetMenu(fileName = "ContagiousInfection", menuName = "InfectionConfigs/ContagiousInfection")]
public class ContagiousInfectionConfigs : InfectionConfigs
{
    [SerializeField] private float _contagionRadius;
    [SerializeField] private LayerMask _infectionLayer;
    public float ContagionRadius => _contagionRadius;
    public LayerMask InfectionLayer => _infectionLayer;

}

