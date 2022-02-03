using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

    public int level = 1;
    public int gold = 0;

    public Player ChangeData(PlayerData data) {
        level = data.level;
        gold = data.gold;
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

}
