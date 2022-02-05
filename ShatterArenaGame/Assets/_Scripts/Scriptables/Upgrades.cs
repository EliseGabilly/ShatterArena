using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Scriptable/Upgrades")]
public class Upgrades : ScriptableObject {


    [SerializeField]
    private string upgradeName = "up";
    public string UpgradeName { get => upgradeName; }

    [SerializeField]
    private Player.Up upgradeType = Player.Up.speed;
    public Player.Up UpgradeType { get => upgradeType; }

    [SerializeField]
    private int entryPrice = 100;
    public int EntryPrice { get => entryPrice; }

    [SerializeField]
    private int pricePerLvl = 20;
    public int PricePerLvl { get => pricePerLvl; }

}
