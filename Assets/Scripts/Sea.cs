using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour {
    Transform target;
    const float nearSeaEnd = 20f;

    void Awake() {
        var player = GameObject.FindGameObjectWithTag(TagName.Player);
        Debug.Assert(player != null);
        target = player.transform;
    }

    void Update() {
        var distance = transform.Position2D().Distance(target.position);
        if (distance > nearSeaEnd) {
            transform.position = target.position.Map(n => Mathf.Round(n));
        }
    }
}
