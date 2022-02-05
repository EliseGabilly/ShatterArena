using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

    public int level = 1;
    public int gold = 0;
    public bool isInverseCam = false;

    public enum Up {
        speed ,
        nbThrow ,
        explosion,
        grenade 
    }
    public int lvlSpeed = 1;
    public int lvlNbThrow = 1;
    public int lvlExplosion = 1;
    public int lvlGrenade = 1;

    public Player ChangeData(PlayerData data) {
        level = data.level;
        gold = data.gold;

        lvlSpeed = data.lvlSpeed;
        lvlNbThrow = data.lvlNbThrow;
        lvlExplosion = data.lvlExplosion;
        lvlGrenade = data.lvlGrenade;

        return this;
    }

    public void ChangeLvl(int change) {
        level += change;
        SaveSystem.SavePlayer(this);
    }
    public void ChangeGold(int change) {
        gold += change;
        SaveSystem.SavePlayer(this);
    }
    public void ChangeIsInverseCam(bool isInverse) {
        isInverseCam = isInverse;
        SaveSystem.SavePlayer(this);
    }

    public void ChangeUpgrade(Up upgrade, int change) {
        switch (upgrade) {
            case Up.speed:
                lvlSpeed += change;
                break;
            case Up.nbThrow:
                lvlNbThrow += change;
                break;
            case Up.explosion:
                lvlExplosion += change;
                break;
            case Up.grenade:
                lvlGrenade += change;
                break;
        }
        SaveSystem.SavePlayer(this);
    }
    public int GetUpgradeLvl(Up upgrade) {
        switch (upgrade) {
            case Up.speed:
                return lvlSpeed;
            case Up.nbThrow:
                return lvlNbThrow;
            case Up.explosion:
                return lvlExplosion;
            case Up.grenade:
                return lvlGrenade;
        }
        return 0;
    }

}
