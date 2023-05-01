using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[ExecuteAlways, ExecuteInEditMode]
public class CompletionSourceAnimate : MonoBehaviour, IAnimate
{
    public Vector3 target;

    UniTaskCompletionSource animateCompletionSource;

    float lerp = 0f;
    Vector3 startPos;
    bool doAnimation;
    float duration;

    void Update()
    {
        if(doAnimation)
        {
            transform.position = Vector3.Lerp(startPos, target, lerp);
            lerp += Time.deltaTime / duration;

            if(lerp >= 1f)
            {
                animateCompletionSource?.TrySetResult();
            }
        }
    }

    public async UniTask Animate(float duration, CancellationToken token)
    {
        lerp = 0f;
        startPos = transform.position;
        this.duration = duration;
        doAnimation = true;

        //Make task completion source
        animateCompletionSource = new UniTaskCompletionSource();

        //Run func when Cancellation Token is cancelled
        token.Register(() =>
        {
            doAnimation = false;

            animateCompletionSource.TrySetResult();
        });

        await animateCompletionSource.Task;

        doAnimation = false;
    }
}
