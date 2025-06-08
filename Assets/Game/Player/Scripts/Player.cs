using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private IPlayerIInput _playerIInput;
    [Inject]
    private void Construct(IPlayerIInput playerIInput)
    {
        _playerIInput = playerIInput;
    }
  
}
