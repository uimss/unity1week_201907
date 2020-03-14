using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    Transform target;

    void Awake() {
        var player = GameObject.FindGameObjectWithTag(TagName.Player);
        Debug.Assert(player != null);
        target = player.transform;
    }

    void LateUpdate() {
        transform.position = target.position.Overwrite(z: transform.position.z);
    }
}
