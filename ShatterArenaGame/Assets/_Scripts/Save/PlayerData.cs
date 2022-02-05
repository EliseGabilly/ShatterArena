using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public int gold;
    public bool isInverseCam;

    public PlayerData(Player player) {
        level = player.level;
        gold = player.gold;
        isInverseCam = player.isInverseCam;
    }

}
