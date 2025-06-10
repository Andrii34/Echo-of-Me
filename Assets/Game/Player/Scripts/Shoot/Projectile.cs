using UnityEngine;

public class Projectile:MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _infectableLayer;
    private IInfection _infection;
    private float _infectionRadius;
    private Contamination _contamination;
    private bool _isMove;

    public void SetInfection(IInfection infection)
    {
        _infection = infection;
        _contamination = new Contamination(infection,_infectionRadius, _infectableLayer);
    }
    public void Increase(float increment)
    {
        transform.localScale +=Vector3.one* increment;
        _infectionRadius += increment;
    }
    public void StartMove()
    {
        _isMove = true;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(TryGetComponent<IInfection>(out var infection))
        { 
        _contamination.StartContamination(transform.position);
            _isMove = false;
            Explosion();
        }
    }
    private void Update()
    {
        if (_isMove)
        {

        Move();
        }
    }

    private void Explosion()
    {
        Destroy(gameObject);
    }
    private void Move()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
