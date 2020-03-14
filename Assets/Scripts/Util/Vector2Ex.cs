using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Ex {
    public static Vector2 Direction(this Vector2 from, Vector2 to) {
        return (to - from).normalized;
    }

    public static float Distance(this Vector2 from, Vector2 to) {
        return Vector2.Distance(from, to);
    }

    public static Vector2 Diff(this Vector2 from, Vector2 to) {
        return to - from;
    }

    public static Vector2 Reverse(this Vector2 vector) {
        return -1f * vector;
    }
}
