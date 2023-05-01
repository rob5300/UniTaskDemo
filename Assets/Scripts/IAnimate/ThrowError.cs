using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThrowError : MonoBehaviour, IAnimate
{
    public float ErrorAfterS = 0.25f;

    public async UniTask Animate(float duration, CancellationToken token)
    {
        float errorDelay = Mathf.Min(ErrorAfterS, duration);

        await UniTask.Delay(TimeSpan.FromSeconds(errorDelay), cancellationToken: token);

        if(!token.IsCancellationRequested)
        {
            throw new Exception("IAnimate error!");
        }
    }
}
