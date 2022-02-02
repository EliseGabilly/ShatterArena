using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [Header("Panels")]
    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas shopCanvas;
    [SerializeField]
    private Canvas infoCanvas;
    [SerializeField]
    private Canvas inGameCanvas;

    public void OpenMenu() {
        menuCanvas.enabled = true;
        shopCanvas.enabled = false;
        infoCanvas.enabled = false;
        inGameCanvas.enabled = false;
    }
    public void OpenShop() {
        menuCanvas.enabled = false;
        shopCanvas.enabled = true;
        infoCanvas.enabled = false;
        inGameCanvas.enabled = false;
    }
    public void OpenInfo() {
        menuCanvas.enabled = false;
        shopCanvas.enabled = false;
        infoCanvas.enabled = true;
        inGameCanvas.enabled = false;
    }
    public void OpenInGame() {
        menuCanvas.enabled = false;
        shopCanvas.enabled = false;
        infoCanvas.enabled = false;
        inGameCanvas.enabled = true;
        GameManager.Instance.StartGame();
    }

    public void BackToMenu() {
        GameManager.Instance.EndGame();
        menuCanvas.enabled = true;
        inGameCanvas.enabled = false;

    }
    public void Replay() {
        GameManager.Instance.EndGame();
        GameManager.Instance.StartGame();
    }

    public void Quit() {
        Application.Quit();
    }

}
