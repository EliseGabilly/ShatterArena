using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public bool InGame { get; set; } = false;
    [SerializeField]
    private AimManager aimManager;
    private Vector3 spawnPoint = new Vector3(0, 0.601f, -5);

    public void StartGame() {
        InGame = true;
        Time.timeScale = 1;
        aimManager.enabled = true;
    }
    public void EndGame() {
        InGame = false;
        Time.timeScale = 0;
        aimManager.enabled = false;
    }

}
