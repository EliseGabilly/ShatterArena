using 
[System.Serializable]
public class PlayerData {

    #region Variables
    public int level;
    public int gold;
    public bool isInverseCam;

    public int lvlSpeed;
    public int lvlNbThrow;
    public int lvlExplosion;
    public int lvlGrenade;
    #endregion

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
