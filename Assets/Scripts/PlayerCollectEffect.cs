using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollectEffect : MonoBehaviour {

    Vector3 originalScale;
    new SpriteRenderer renderer;
    PlayState state = PlayState.None;

    bool IsPlaying {
        get { return state != PlayState.None; }
    }

    public bool IsPlayingCollect {
        get { return state == PlayState.Collect; }
    }

    public bool IsPlayingRelease {
        get { return state == PlayState.Release; }
    }

    public enum PlayState {
        None,
        Collect,
        Release
    }

    public void PlayCollectLoop() {
        if (IsPlayingCollect) return;
        PlayCommon(PlayState.Collect);
        transform.localScale = originalScale;
        transform.DOScale(originalScale * 0.75f, 0.5f)
            .SetLoops(-1)
            .SetEase(Ease.OutQuad);
    }

    public void PlayRelease() {
        PlayCommon(PlayState.Release);
        transform.localScale = originalScale * 0.75f;
        transform.DOScale(originalScale, 0.2f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => renderer.enabled = false);
    }

    public void Stop() {
        transform.DOKill();
        state = PlayState.None;
        renderer.enabled = false;
    }

    void Awake() {
        originalScale = transform.localScale;
        renderer = GetComponent<SpriteRenderer>();
        Debug.Assert(renderer != null);
        renderer.enabled = false;
    }

    void PlayCommon(PlayState newState) {
        transform.DOKill();
        state = newState;
        renderer.enabled = true;
    }
}
