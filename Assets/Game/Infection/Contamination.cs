using UnityEngine;

public class Contamination
{
    private float _infectionRadius;
     private LayerMask _infectableLayer;
    private IInfection _infection;
    public Contamination(IInfection infection,float radius, LayerMask infectableLayer)
    {
        _infection = infection;
        _infectionRadius = radius;
        _infectableLayer = infectableLayer;
    }
    public void StartContamination(Vector3 position)
    {
        Collider[] infectables = Physics.OverlapSphere(position, _infectionRadius, _infectableLayer);
        foreach (var item in infectables)
        {
            if (item.TryGetComponent<Infectable>(out var infection))
            {
                infection.ApplyInfection(_infection);
            }
        }
    }

}
