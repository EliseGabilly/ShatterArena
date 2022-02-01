using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {

    public static Vector3 GetRandomPosition (float z = 0.0f, float round = 0.5f) {
        return new Vector3(RandomExclusive(-10f, 10f, round), (z == 0 ? 0 : RandomExclusive(-10f, 10f, round)), RandomExclusive(-10f, 10f, round));
    }

    public static float RandomExclusive(float minInclusive, float maxInclusive, float exclude = 0.5f, float round = 0) {
        float rand = Random.Range(minInclusive+exclude, maxInclusive+exclude);
        return rand - (rand % round);
    }
}
