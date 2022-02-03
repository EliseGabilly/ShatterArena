using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public int gold;

    public PlayerData(Player player) {
        level = player.level;
        gold = player.gold;
    }

}
