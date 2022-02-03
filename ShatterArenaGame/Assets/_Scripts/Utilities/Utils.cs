using UnityEngine;

public static class Utils {

    public static Vector3 GetRandomPosition () {
        int x = Mathf.RoundToInt(Random.Range(Const.Instance.MinTerrain + 1, Const.Instance.MaxTerrain-1));
        int z = Mathf.RoundToInt(Random.Range(Const.Instance.MinTerrain + 1, Const.Instance.MaxTerrain-1));
        return new Vector3(x, 0, z);
    }

    public static float RandomExclusive(float minInclusive, float maxInclusive, float exclude = 0.5f) {
        float rand = Random.Range(minInclusive+exclude, maxInclusive+exclude);
        float res = RoundUpToNearest(rand, 0.5f);
        return res;
    }
    public static float RoundUpToNearest(float passednumber, float roundto) {
        // for 105.5 round to
        // 1 = 106, 10 = 110, 7 = 112, 100 = 200, 0.2 = 105.6, 0.3 = 105.6

        //if no rounto then just pass original number back
        if (roundto == 0) {
            return passednumber;
        } else {
            return Mathf.Ceil(passednumber / roundto) * roundto;
        }
    }

}
