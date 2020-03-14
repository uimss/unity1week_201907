using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class ButtonDoor : Door {
    [SerializeField] Button[] buttons = default;
    float size;

    void Awake() {
        var collider = GetComponent<BoxCollider2D>();
        Debug.Assert(collider != null);
        size = collider.size.x;
        Debug.Assert(buttons.Length > 0);
    }

    void Update() {
        CtrlOpen();
    }

    void CtrlOpen() {
        if (isOpen) return;
        isOpen = buttons.All(b => b.IsPushed);
        if (isOpen) {
            var endPosition = transform.position.x + size - (PixelPerfect.UPP * 2f);
            transform.DOMoveX(endPosition, 0.5f);
            // transform.position = transform.position.Add(x: transform.localScale.x);
        }
    }

    void OnDrawGizmos() {
        foreach (var button in buttons) {
            Gizmos.DrawLine(
                transform.position,
                button.transform.position
            );
        }
    }
}
