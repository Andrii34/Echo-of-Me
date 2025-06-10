using UnityEngine;

[CreateAssetMenu(fileName = "Infection/Configs/InfectionConfigs", menuName = "InfectionConfigs/Infections")]
public class InfectionConfigs : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private float _timeToDeath;
    

    public Color Color => _color;
    public float TimeToDeath => _timeToDeath;


}
