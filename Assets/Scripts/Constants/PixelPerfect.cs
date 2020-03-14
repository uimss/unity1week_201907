using UnityEngine;

public static class PixelPerfect {

    public const int PPU = 16;
    public const float UPP = 1f / (float)PixelPerfect.PPU;

    public static float Snap(float original) {
        return Mathf.Round(original / PixelPerfect.UPP) * PixelPerfect.UPP;
    }

    public static Vector3 Snap(Vector3 original) {
        return new Vector3(
            PixelPerfect.Snap(original.x),
            PixelPerfect.Snap(original.y),
            PixelPerfect.Snap(original.z)
        );
    }
}
