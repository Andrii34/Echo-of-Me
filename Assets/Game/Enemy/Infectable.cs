using UnityEngine;

public  class Infectable : MonoBehaviour
{
    private Renderer _renderer;
    private IInfection _infection;
    public Renderer Renderer
    {
        get => _renderer;
        set => _renderer = value;
    }
    private void Update()
    {
        UpdateInfection();
    }

    public void ApplyInfection(IInfection infection)
    {
       infection.StartInfection(this);
        
    }
    private void UpdateInfection()
    {
        if (_infection != null)
        {
            _infection.Update();
        }
        else
        {
            return;
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }

}