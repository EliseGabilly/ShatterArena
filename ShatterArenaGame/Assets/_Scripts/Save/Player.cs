/// <summary>
/// Player class containing the information that are "translated" in playerdata then saved
/// </summary>
public class Player : Singleton<Player> {

    #region Variables
    public int level = 1;
    public int gold = 0;
    public bool isInverseCam = false;
    public bool isSoundOn = true;

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
    #endregion

    public Player ChangeData(PlayerData data) {
        level = data.level;
        gold = data.gold;
        isInverseCam = data.isInverseCam;
        isSoundOn = data.isSoundOn;

        lvlSpeed = data.lvlSpeed;
        lvlNbThrow = data.lvlNbThrow;
        lvlExplosion = data.lvlExplosion;
        lvlGrenade = data.lvlGrenade;

        return this;
    }

    public void ChangeLvl(int change) {
        level += change;
        UIManager.Instance.UpdateShopValues();
        SaveSystem.SavePlayer(this);
    }
    public void ChangeGold(int change) {
        gold += change;
        UIManager.Instance.UpdateShopValues();
        SaveSystem.SavePlayer(this);
    }
    public void ChangeIsInverseCam(bool isInverse) {
        isInverseCam = isInverse;
        SaveSystem.SavePlayer(this);
    }
    public void ChangeIsMusicOn(bool isOn) {
        isSoundOn = isOn;
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
        UIManager.Instance.UpdateShopValues();
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
