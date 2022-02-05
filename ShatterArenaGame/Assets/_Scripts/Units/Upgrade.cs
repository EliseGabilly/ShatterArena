using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour {

    #region Variables
    [SerializeField]
    private Upgrades upgrade;
    [SerializeField]
    private Text nameTxt;
    [SerializeField]
    private Text lvlTxt;
    [SerializeField]
    private Text priceTxt;
    #endregion

    private void Start() {
        nameTxt.text = upgrade.UpgradeName;
        StartCoroutine(nameof(LateStart));
    }
    private IEnumerator LateStart() {
        yield return new WaitForSeconds(0.01f);
        UpdateValues();
    }

    private void UpdateValues() {
        lvlTxt.text = Player.Instance.GetUpgradeLvl(upgrade.UpgradeType).ToString();
        priceTxt.text = (upgrade.EntryPrice + upgrade.PricePerLvl * Player.Instance.GetUpgradeLvl(upgrade.UpgradeType)).ToString();
    }

    public void ChangeUpgrade(int change) {
        Player.Instance.ChangeUpgrade(upgrade.UpgradeType, change);
        UpdateValues();
    }

    public void BuyUpgrade() {
        int price = (upgrade.EntryPrice + upgrade.PricePerLvl * Player.Instance.GetUpgradeLvl(upgrade.UpgradeType));
        if(Player.Instance.gold >= price) {
            Player.Instance.ChangeUpgrade(upgrade.UpgradeType, 1);
            Player.Instance.ChangeGold(-price);
            UpdateValues();
        }
    }
}
