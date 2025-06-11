using UnityEngine;
using static UnityEngine.Rendering.HableCurve;


[RequireComponent(typeof(LineRenderer))]
public class RoadToGate : MonoBehaviour
{
    [SerializeField] private Transform _gate;
    [SerializeField] private Transform _planeTransform;
    
    

    private LineRenderer _lineRenderer;

    private void Start()
    {
       
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.alignment = LineAlignment.TransformZ;
       

       // transform.rotation = Quaternion.LookRotation(Vector3.up);
        _lineRenderer.positionCount = 2;

    }
    void Update()
    {
        
        if (_gate == null || _planeTransform == null) return;

        
        Vector3 start = ProjectPointOnPlane(_planeTransform, transform.position);   // игрок
        Vector3 end = ProjectPointOnPlane(_planeTransform, _gate.position);         // ворота

        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        // Ўирина линии = ширине игрока
        float width = GetPlayerWidth();
        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
    }

    

    Vector3 ProjectPointOnPlane(Transform plane, Vector3 point)
    {
        Vector3 pos = point;
        return new Vector3(
            pos.x,
            plane.position.y+0.1f, 
            pos.z
        );
    }

    float GetPlayerWidth()
    {       
        return transform.localScale.x;
    }
}
