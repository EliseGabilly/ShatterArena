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
    [SerializeField]
    private Canvas infoGameCanvas;
    [SerializeField]
    private Canvas endCanvas;

    [Header("Values")]
    [SerializeField]
    private Text goldValue;
    [SerializeField]
    private Text destructionValue;
    private bool isFirstEvol;

    [Header("Elements")]
    [SerializeField]
    private GameObject lvlUpBtn;
    [SerializeField]
    private Toggle cameraToggle;
    #endregion

    #region Dev
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text goldTxt;

    private void Update() {
        lvlTxt.text = Player.Instance.level.ToString();
        goldTxt.text = Player.Instance.gold.ToString();
    }

    #endregion

    private void Start() {
        cameraToggle.isOn = Player.Instance.isInverseCam;
    }

    public void OpenMenu() {
        menuCanvas.enabled = true;
        shopCanvas.enabled = false;
        infoCanvas.enabled = false;
        inGameCanvas.enabled = false;
        endCanvas.enabled = false;
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
        endCanvas.enabled = false;
        lvlUpBtn.SetActive(false);
        GameManager.Instance.StartGame();
        isFirstEvol = true;
        UpdateGameValues();
    }

    public void OpenEnd() {
        endCanvas.enabled = true;
    }

    public void OpenInfoGame(bool isOpen) {
        infoGameCanvas.enabled = isOpen;
    }

    public void BackToMenu() {
        GameManager.Instance.EndGame();
        menuCanvas.enabled = true;
        inGameCanvas.enabled = false;

    }
    public void Replay() {
        endCanvas.enabled = false;
        lvlUpBtn.SetActive(false);
        GameManager.Instance.EndGame();
        GameManager.Instance.StartGame();
        isFirstEvol = true;
        UpdateGameValues();
    }

    public void Quit() {
        Application.Quit();
    }

    public void UpdateGameValues() {
        goldValue.text = GameManager.Instance.GameGold.ToString();
        int destructionVal = (int)(((GameManager.Instance.NbObstacles - GameManager.Instance.NbObstaclesLeft) / GameManager.Instance.NbObstacles) * 100);
        destructionValue.text = destructionVal.ToString() + " %";
        if (destructionVal>=100) {
            //arena cleared
            endCanvas.enabled = true;
            Player.Instance.ChangeLvl(1);
            GameManager.Instance.FinishGame();
        } else if (destructionVal >= Const.DestructionObjectif && isFirstEvol) {
            lvlUpBtn.SetActive(true);
            isFirstEvol = false;
        }
    }

    public void LvlUp() {
        Player.Instance.ChangeLvl(1);
        lvlUpBtn.SetActive(false);
    }

    public void ToggleCamera(bool isOn) {
        Player.Instance.ChangeIsInverseCam(isOn);
    }
}
