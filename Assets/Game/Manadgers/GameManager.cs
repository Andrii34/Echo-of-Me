using UnityEngine;

using Zenject;

public class GameManager : MonoBehaviour
{
    private IPlayerIInput _playerInput;
    [SerializeField] private MoverToGate _moverToGate;
    [SerializeField] private Player _player;
    [SerializeField] private GameOver _gameOver;
   
    private int infectedCount = 0;
    private bool infectionStarted = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        _player.OnMinimalSize += _gameOver.LevelFail;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Infectable.OnInfectable += OnInfectableApplied;
        Infectable.OnInfectableDeath += OnInfectableDeath;
    }

    private void OnDestroy()
    {
        Infectable.OnInfectable -= OnInfectableApplied;
        Infectable.OnInfectableDeath -= OnInfectableDeath;
    }
    [Inject]
    private void Construct(IPlayerIInput playerIInput)
    {
        _playerInput = playerIInput;
    }

    private void OnInfectableApplied(Infectable infectable)
    {
        infectedCount++;
        Debug.Log("Infectable applied: " + infectedCount);

        if (!infectionStarted)
        {
            infectionStarted = true;
            StartInfection();
        }
    }

    private void OnInfectableDeath(Infectable infectable)
    {
        infectedCount--;
        Debug.Log("Infectable death: " + infectedCount);

        if (infectedCount <= 0)
        {
            infectionStarted = false;
            Debug.Log("All infectables are dead, ending infection.");
            EndInfection();
        }
    }

    private void StartInfection()
    {
        Debug.Log("Infection Started");
        _playerInput?.Enable();
    }

    private void EndInfection()
    {
       
        _playerInput.Disable();
        _moverToGate.StartMove();
    }
    private void OnDisable()
    {
        Infectable.OnInfectable -= OnInfectableApplied;
        Infectable.OnInfectableDeath -= OnInfectableDeath;
        _player.OnMinimalSize -= _gameOver.LevelFail;
    }
}

