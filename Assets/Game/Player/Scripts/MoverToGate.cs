using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MoverToGate : MonoBehaviour
{
    public event Action<bool> OnMove;

    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float safeDistanceToObstacle = 1.5f;
    [SerializeField] private GameObject _gate;
    private Transform gate;

    private IPlayerIInput _playerInput;
    private bool isMoving = false;
    private Coroutine moveCoroutine;

    [Inject]
    private void Construct(IPlayerIInput playerIInput)
    {
        _playerInput = playerIInput;
    }

    private void Start()
    {
        gate = _gate.transform;
    }

    private void Update()
    {
        if (gate != null)
        {
            transform.LookAt(gate);
        }
    }

    public void StartMove()
    {
        if (CanMoveToGate(out Vector3 targetPosition))
        {
            isMoving = true;
            _playerInput.Disable();
            moveCoroutine = StartCoroutine(MoveStep(targetPosition));
        }
        else
        {
            _playerInput.Enable();
            Debug.Log("Obstacle detected, cannot move");
            OnMove?.Invoke(false);
        }
    }

    private IEnumerator MoveStep(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            OnMove?.Invoke(true);
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        _playerInput?.Enable();
        OnMove?.Invoke(false);
        StartMove();
    }

    public void StopMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }

        isMoving = false;
        _playerInput?.Enable();
        OnMove?.Invoke(false);
        Debug.Log("Movement stopped and state reset.");
    }

    private bool CanMoveToGate(out Vector3 targetPosition)
    {
        if (gate == null)
        {
            targetPosition = transform.position;
            Debug.LogWarning("Gate reference is missing.");
            return false;
        }

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

    private void OnDisable()
    {
        StopMove();
    }
}



