using UnityEngine;

public class LevelWin : MonoBehaviour
{
    [SerializeField] private GameObject levelWinPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            ShowLevelWinPanel();
        }
    }
    private void ShowLevelWinPanel()
    {
        levelWinPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}