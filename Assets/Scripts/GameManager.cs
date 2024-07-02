using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public UIManager uiManager;
    public bool gameOver;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        spawnManager.StartSpawn();
        uiManager.ShowIntro();
    }
}
