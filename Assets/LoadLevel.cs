using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private void Awake()
    {
        
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}

