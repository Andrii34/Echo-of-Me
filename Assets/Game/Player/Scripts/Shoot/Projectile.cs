using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _infectableLayer;
    private DeathInfection _infection;
    [SerializeField] private float _infectionBaseRadius;
    private Contamination _contamination;
    private bool _isMove;

    private bool _infectingPhase = false;
    private float _remainingDistance;

    public void SetInfection(DeathInfection infection)
    {
        _infection = infection;
    }

    public void Increase(float increment)
    {
        transform.localScale += Vector3.one * increment;
        _infectionBaseRadius += increment;
    }

    public void StartMove()
    {
        _isMove = true;
    }

    private void Update()
    {
        if (_isMove)
        {
            float moveStep = _speed * Time.deltaTime;
            transform.Translate(Vector3.forward * moveStep);

            if (_infectingPhase)
            {
                

                _remainingDistance -= moveStep;
                if (_remainingDistance <= 0f)
                {
                    _isMove = false;
                    Explosion();

                }
            }
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Infectable>(out var infectable))
        {       
                _infectingPhase = true;               
                float diameter = transform.localScale.x/2 ;
                _remainingDistance = diameter * 0.8f;
            
        }
    }

    private void Explosion()
    {
        _isMove = false;
        _contamination = new Contamination(_infection, _infectionBaseRadius, _infectableLayer);
        _contamination.StartContamination(transform.position);
        Destroy(gameObject);
    }
}

