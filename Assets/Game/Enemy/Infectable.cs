using System;
using UnityEngine;

public  class Infectable : MonoBehaviour
{
    // TODO : crete register and shange static
    public static Action<Infectable> OnInfectable;
    public static Action<Infectable> OnInfectableDeath;
    //TODO: create factory and spawner for zenject
    [SerializeField]private Renderer _renderer;
    private IInfection _infection;
    private bool _isInfected;
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
        Debug.Log("Infection applied to " + gameObject.name);
        OnInfectable?.Invoke(this);
        _infection = infection;
        
        _infection.StartInfection(this);
        _isInfected = true;

    }
    private void UpdateInfection()
    {
        if (_infection!= null)
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
        OnInfectableDeath?.Invoke(this);
        Debug.Log("Infectable object " + gameObject.name + " has died.");
        Destroy(gameObject);
    }

}