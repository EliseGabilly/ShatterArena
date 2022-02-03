using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    #region Variables
    [Header("Panels")]
    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas shopCanvas;
    [SerializeField]
    private Canvas infoCanvas;
    [SerializeField]
    private Canvas inGameCanvas;

    [Header("Values")]
    [SerializeField]
    private Text goldValue;
    [SerializeField]
    private Text destructionValue;

    [Header("Elements")]
    [SerializeField]
    private GameObject lvlUpBtn;
    #endregion

    #region Dev
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text goldTxt;
    [SerializeField]
    private Player player;

    private void Update() {
        lvlTxt.text = player.level.ToString();
        goldTxt.text = player.gold.ToString();
    }

    #endregion

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
        lvlUpBtn.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void BackToMenu() {
        GameManager.Instance.EndGame();
        menuCanvas.enabled = true;
        inGameCanvas.enabled = false;

    }
    public void Replay() {
        lvlUpBtn.SetActive(false);
        GameManager.Instance.EndGame();
        GameManager.Instance.StartGame();
    }

    public void Quit() {
        Application.Quit();
    }

    public void UpdateGameValues() {
        goldValue.text = GameManager.Instance.Gold.ToString();
        int destructionVal = (int)(((GameManager.Instance.NbObstacles - GameManager.Instance.NbObstaclesLeft) / GameManager.Instance.NbObstacles) * 100);
        destructionValue.text = destructionVal.ToString() + " %";
        if (destructionVal>=Const.DestructionObjectif) lvlUpBtn.SetActive(true);
    }
    public void LvlUp() {
        //TODO
    }
}
