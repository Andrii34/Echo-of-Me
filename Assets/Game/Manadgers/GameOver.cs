using System;
using UnityEngine;

internal class GameOver:MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    internal void LevelFail()
    {
        Time.timeScale = 0f;
        _gameOverPanel.SetActive(true);
    }
}