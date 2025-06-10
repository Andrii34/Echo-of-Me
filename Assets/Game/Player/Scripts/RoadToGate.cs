using UnityEngine;
using static UnityEngine.Rendering.HableCurve;


[RequireComponent(typeof(LineRenderer))]
public class RoadToGate : MonoBehaviour
{
    [SerializeField] private Transform _gate;
    [SerializeField] private Transform _planeTransform;
    
    private int _segmentCount = 10;

    private LineRenderer _lineRenderer;

    private void Start()
    {
       
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount + 1;

    }
    void Update()
    {
        if (_gate == null || _planeTransform == null) return;

        Vector3 start = ProjectPointOnPlane(_planeTransform, transform.position);
        Vector3 end = ProjectPointOnPlane(_planeTransform, _gate.position);

        
        for (int i = 0; i <= _segmentCount; i++)
        {
            float t = i / (float)_segmentCount;
            Vector3 point = Vector3.Lerp(start, end, t);
            _lineRenderer.SetPosition(i, point);
        }

       
        float width = GetPlayerWidth();

        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
    }

    Vector3 ProjectPointOnPlane(Transform plane, Vector3 point)
    {
        Vector3 planeNormal = plane.up;
        float distance = Vector3.Dot(planeNormal, point - plane.position);
        return point - planeNormal * distance;
    }

    float GetPlayerWidth()
    {
        
        Renderer rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            
            return rend.bounds.size.x;
        }

        
        return transform.localScale.x;
    }
}
