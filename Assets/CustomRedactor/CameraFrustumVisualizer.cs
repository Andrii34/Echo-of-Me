using UnityEngine;

[ExecuteAlways]
public class CameraFrustumVisualizer : MonoBehaviour
{
    public Camera cam;
    public float planeHeight = 0f;
    public float maxRayDistance = 100f; // Максимальная длина луча для поиска пересечения

    private void OnDrawGizmos()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null) return;
        }

        Vector3[] points = GetFrustumPlanePoints(cam, planeHeight);
        if (points == null)
        {
            Debug.LogWarning("[FrustumVisualizer] Нет пересечений с плоскостью y=" + planeHeight);
            return;
        }

        // Рисуем сферы в точках пересечения
        Gizmos.color = Color.blue;
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawSphere(points[i], 0.2f);
        }

        // Рисуем замкнутый контур (толщину можно увеличить рисованием кубиков - здесь обычные линии)
        Gizmos.color = Color.red;
        for (int i = 0; i < points.Length; i++)
        {
            Vector3 start = points[i];
            Vector3 end = points[(i + 1) % points.Length];
            DrawThickLine(start, end, 0.05f);
        }
    }

    Vector3[] GetFrustumPlanePoints(Camera camera, float y)
    {
        Plane plane = new Plane(Vector3.up, new Vector3(0, y, 0));
        Vector3[] corners = new Vector3[4];

        Vector2[] viewportCorners = new Vector2[]
        {
            new Vector2(0, 0), // Нижний левый
            new Vector2(1, 0), // Нижний правый
            new Vector2(1, 1), // Верхний правый
            new Vector2(0, 1)  // Верхний левый
        };

        for (int i = 0; i < 4; i++)
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(viewportCorners[i].x, viewportCorners[i].y, 0));

            if (plane.Raycast(ray, out float distance))
            {
                // Если луч пересекает плоскость — берем точку пересечения
                corners[i] = ray.GetPoint(distance);
            }
            else
            {
                // Если луч не пересекает плоскость, пытаемся найти точку на фиксированном расстоянии вдоль луча,
                // проецируем её на плоскость по y (возможна потеря точности, но гарантирует визуализацию)
                Vector3 fallbackPoint = ray.origin + ray.direction * maxRayDistance;
                // Проецируем fallbackPoint на плоскость y=planeHeight, сохраняя x и z, меняем y на planeHeight
                corners[i] = new Vector3(fallbackPoint.x, y, fallbackPoint.z);
            }
        }

        return corners;
    }

    // Рисуем "толстую" линию кубиком
    void DrawThickLine(Vector3 start, Vector3 end, float thickness)
    {
        Vector3 direction = end - start;
        float length = direction.magnitude;
        Vector3 center = (start + end) * 0.5f;

        Quaternion rotation = Quaternion.LookRotation(direction);

        Gizmos.matrix = Matrix4x4.TRS(center, rotation, new Vector3(thickness, thickness, length));
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.identity;
    }
}


