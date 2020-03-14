using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Collectable : MonoBehaviour {
    const float collectedSpeed = 5f;
    const float fallSpeed = 3f;
    const float fallCompleteRadius = 0.1f;
    const float jointRadius = 0.35f;
    Hole fallTo;
    Player collectedFrom;
    bool isJointed = false;

    public bool IsJointed {
        get { return isJointed; }
    }

    public bool IsCollected {
        get { return collectedFrom != null; }
    }

    public bool IsFalling {
        get { return fallTo != null; }
    }

    public void Collect(Player target) {
        if (IsCollected) return;
        fallTo = null;
        collectedFrom = target;
        transform.parent = target.transform;
    }

    public void Release() {
        if (!IsCollected) return;
        collectedFrom = null;
        transform.parent = null;
        isJointed = false;
    }

    public void Fall(Hole target) {
        if (IsCollected) return;
        fallTo = target;
    }

    public static Collectable[] Search(Vector2 position, float radius, bool includeCollected = false) {
        return Physics2D.OverlapCircleAll(
            position,
            radius,
            LayerName.CollectableMask
        ).Select(hit => {
            var collectable = hit.GetComponent<Collectable>();
            Debug.Assert(collectable != null);
            return collectable;
        }).Where(collectable => {
            return includeCollected ? collectable.IsCollected : true;
        }).ToArray();
    }

    public static bool Exists(Vector2 position, float radius, bool includeCollected = false) {
        if (includeCollected) {
            return Physics2D.OverlapCircle(
                position,
                radius,
                LayerName.CollectableMask
            );
        } else {
            return Collectable.Search(position, radius, false).Length > 0;
        }
    }

    void Update() {
        CtrlMove();
    }

    void CtrlMove() {
        if (IsFalling) {
            CtrlFall();
            return;
        }

        if (IsCollected) {
            CtrlCollect();
        } else {
            // TODO: リリース処理
        }
    }

    void CtrlFall() {
        var distance = transform.Position2D().Distance(fallTo.transform.position);
        if (distance <= fallTo.FallCompleteRadius) {
            fallTo.FallCollectableComplete();
            gameObject.SetActive(false);
            return;
        }
        transform.position = Vector2.MoveTowards(
            transform.position,
            fallTo.transform.position,
            fallSpeed * Time.deltaTime
        );
    }

    void CtrlCollect() {
        if (isJointed) return;
        isJointed = IsJointedRoot() || IsJointedOther();
        if (isJointed) {
            transform.localPosition = PixelPerfect.Snap(transform.localPosition);
            return;
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            collectedFrom.transform.position,
            collectedSpeed * Time.deltaTime
        );
    }

    bool IsJointedRoot() {
        return transform.Position2D().Distance(collectedFrom.transform.position) < jointRadius;
    }

    bool IsJointedOther() {
        var direction = transform.Position2D().Direction(collectedFrom.transform.position);
        var hit = Physics2D.Raycast(
            transform.position,
            direction,
            jointRadius,
            LayerName.CollectableMask
        );
        Debug.DrawRay(
            transform.position,
            direction * jointRadius,
            Color.green,
            1f
        );
        if (!hit) return false;
        var collectable = hit.collider.GetComponent<Collectable>();
        if (collectable == null) return false;
        return collectable.IsJointed;
    }
}
