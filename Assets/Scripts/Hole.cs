using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Hole : MonoBehaviour {
    [SerializeField] Island nextIsland = default;
    [SerializeField] int numNeedToFill = 2;
    const float fallCompleteRadius = 0.1f;
    int numFilled = 0;
    float holeSizePerFill = 0.6f;

    public float FallCompleteRadius {
        get { return fallCompleteRadius; }
    }

    float FallRadius {
        get { return (float)numNeedToFill / 2; }
    }

    Vector3 HoleSize {
        get {
            var scale = Mathf.Clamp(numNeedToFill - numFilled, 1, numNeedToFill) * holeSizePerFill;
            return Vector3.one * scale;
        }
    }

    public void FallCollectableComplete() {
        numFilled++;
    }

    void Awake() {
        Debug.Assert(nextIsland != null);
    }

    void Start() {
        transform.DORotate(new Vector3(0f, 0f, 360f), 4f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }

    void Update() {
        CtrlFallCollectable();
        CtrlHoleSize();
        CtrlFilled();
    }

    void CtrlFallCollectable() {
        var collectableList = Collectable.Search(
            transform.position,
            FallRadius
        ).Where(collectable => !collectable.IsFalling);

        foreach (var collectable in collectableList) {
            collectable.Fall(this);
        }
    }

    void CtrlHoleSize() {
        transform.localScale = HoleSize;
    }

    void CtrlFilled() {
        Debug.Assert(numNeedToFill - numFilled >= 0);
        if (numNeedToFill - numFilled == 0) {
            nextIsland.Show();
            gameObject.SetActive(false);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, FallRadius);
        Gizmos.DrawWireSphere(transform.position, fallCompleteRadius);
    }

    void OnValidate() {
        transform.localScale = HoleSize;
    }
}
