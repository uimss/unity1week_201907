using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Player : MonoBehaviour {
    const float moveSpeed = 7f;
    const float collectRadius = 2f;
    Rigidbody2D rigidBody;
    PlayerCollectEffect collectEffect;
    Collectable[] collectableList = Array.Empty<Collectable>();

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        Debug.Assert(rigidBody != null);
        collectEffect = GetComponentInChildren<PlayerCollectEffect>();
        Debug.Assert(collectEffect != null);
    }

    void Update() {
        var isCollecting = false;
        if (Input.GetKeyDown(KeyCode.X)) {
            CtrlRelease();
        } else if (Input.GetKey(KeyCode.Z)) {
            isCollecting = true;
            CtrlCollect();
        }
        CtrlCollectEffect(isCollecting);
    }

    void FixedUpdate() {
        CtrlMove();
    }

    void CtrlMove() {
        var x = Mathf.Min(Input.GetAxis("Horizontal") * 1.2f, 1f);
        var y = Mathf.Min(Input.GetAxis("Vertical") * 1.2f, 1f);
        rigidBody.velocity = new Vector2(x, y) * moveSpeed;
    }

    void CtrlCollect() {
        var hits = Collectable.Search(transform.position, collectRadius);
        foreach (var collectable in hits) {
            collectable.Collect(this);
        }
        collectableList = collectableList.Concat(hits).ToArray();
    }

    void CtrlRelease() {
        foreach (var collectable in collectableList) {
            collectable.Release();
        }
        collectableList = Array.Empty<Collectable>();

        collectEffect.PlayRelease();
    }

    void CtrlCollectEffect(bool isCollecting) {
        // start
        if (isCollecting && !collectEffect.IsPlayingCollect) {
            collectEffect.PlayCollectLoop();
        }
        // end
        if (!isCollecting && collectEffect.IsPlayingCollect) {
            collectEffect.Stop();
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, collectRadius);
    }
}
