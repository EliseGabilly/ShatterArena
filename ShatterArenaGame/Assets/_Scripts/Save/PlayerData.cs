using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public int gold;
    public bool isInverseCam;

    public int lvlSpeed;
    public int lvlNbThrow;
    public int lvlExplosion;
    public int lvlGrenade;

    public PlayerData(Player player) {
        level = player.level;
        gold = player.gold;
        isInverseCam = player.isInverseCam;

        lvlSpeed = player.lvlSpeed;
        lvlNbThrow = player.lvlNbThrow;
        lvlExplosion = player.lvlExplosion;
        lvlGrenade = player.lvlGrenade;
    }

}
