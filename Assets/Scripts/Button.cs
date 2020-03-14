using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Button : MonoBehaviour {
    [SerializeField] bool isOnce = false;
    const float radius = 0.5f;
    bool isPushed = false;
    SimpleAnimation simpleAnimation;

    public bool IsPushed {
        get { return isPushed; }
    }

    void Awake() {
        simpleAnimation = GetComponent<SimpleAnimation>();
        Debug.Assert(simpleAnimation != null);
    }

    void Update() {
        CtrlPush();
    }

    void CtrlPush() {
        if (isOnce && isPushed) return;
        var prevIsPushed = isPushed;
        isPushed = Collectable.Exists(
            transform.position,
            radius,
            includeCollected: true
        );
        if (prevIsPushed != isPushed) {
            var animation = isPushed ? "Pushed" : "Default";
            simpleAnimation.Play(animation);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
