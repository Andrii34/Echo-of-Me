using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MoverToGate : MonoBehaviour
{
    public event Action<bool> OnMove;

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float safeDistanceToObstacle = 1.5f; // расстояние до препятствия
    [SerializeField] private Transform gate;

    private IPlayerIInput _playerInput;
    private bool isMoving = false;

    [Inject]
    private void Construct(IPlayerIInput playerIInput)
    {
        _playerInput = playerIInput;
    }

    private void Update()
    {
        if (gate != null)
            transform.LookAt(gate);
    }

    public void StartMove()
    {
        Debug.Log("StartMove called");
        if (isMoving)
            return;

        if (gate == null)
        {
            Debug.LogError("Gate transform is null");
            return;
        }

        if (CanMoveToGate(out Vector3 targetPosition))
        {
            isMoving = true;
            _playerInput?.Disable();
            StartCoroutine(MoveStep(targetPosition));
        }
        else
        {
            _playerInput?.Enable();
            Debug.Log("Obstacle detected, cannot move");
            OnMove?.Invoke(false);
        }
    }

    private IEnumerator MoveStep(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        _playerInput?.Enable();
        OnMove?.Invoke(true);
    }

    private bool CanMoveToGate(out Vector3 targetPosition)
    {
        Vector3 direction = (gate.position - transform.position).normalized;
        Vector3 origin = transform.position;
        float totalDistanceToGate = Vector3.Distance(transform.position, gate.position);
        float sphereRadius = transform.localScale.x / 2;

        
        bool hasObstacle = Physics.SphereCast(
            origin,
            sphereRadius,
            direction,
            out RaycastHit hit,
            totalDistanceToGate,
            obstacleLayer
        );

        if (hasObstacle)
        {
            float adjustedDistance = hit.distance - safeDistanceToObstacle;
            if (adjustedDistance <= 0.1f)
            {
                targetPosition = transform.position;
                Debug.Log("Obstacle too close, cannot move");
                return false;
            }

            targetPosition = origin + direction * adjustedDistance;
            Debug.Log($"Obstacle detected, moving to safe distance: {adjustedDistance}");
            return true;
        }
        else
        {
            
            targetPosition = gate.position;
            Debug.Log("No obstacles, moving directly to gate");
            return true;
        }
    }
}


