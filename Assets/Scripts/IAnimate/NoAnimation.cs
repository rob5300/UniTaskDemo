using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NoAnimation : MonoBehaviour, IAnimate
{
    public UniTask Animate(float duration, CancellationToken token)
    {
        return UniTask.CompletedTask;
    }
}
