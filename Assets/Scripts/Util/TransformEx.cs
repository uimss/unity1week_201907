using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformEx {
    public static Vector2 Position2D(this Transform transform) {
        return ((Vector2)transform.position);
    }
}
