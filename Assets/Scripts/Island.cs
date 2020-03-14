using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Island : MonoBehaviour {
    [SerializeField] bool isFirst = false;
    Transform ground;
    Transform others;

    public void Show() {
        ground.gameObject.SetActive(true);
        var originalScale = ground.localScale;
        ground.localScale = ground.localScale * 0.7f;
        ground.DOScale(originalScale, 1f)
            .OnComplete(() => others.gameObject.SetActive(true));
    }

    void Awake() {
        ground = transform.Find("Ground");
        others = transform.Find("Others");
        Debug.Assert(ground != null);
        Debug.Assert(others != null);
    }

    void Start() {
        if (!isFirst) {
            ground.gameObject.SetActive(false);
            others.gameObject.SetActive(false);
        }
    }
}
