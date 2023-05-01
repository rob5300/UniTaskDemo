using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Threading;
using UnityEngine;

[ExecuteAlways]
public class UniTaskDemo_CancellationToken : MonoBehaviour
{
    [SerializeField]
    Vector3 add = new Vector3(0f, 0f, -1f);

    [SerializeField]
    float resetTime = 2f;

    Vector3 originalPos;

    CancellationTokenSource animateTokenSource;

    [Button]
    public async UniTask Animate()
    {
        //Cancel old token, and make new source
        animateTokenSource?.Cancel();
        animateTokenSource = new CancellationTokenSource();

        //Get token
        CancellationToken token = animateTokenSource.Token;

        RestorePos(token).Forget();

        while (this != null && !token.IsCancellationRequested)
        {
            transform.position += add * Time.deltaTime;

            await UniTask.Yield();
        }
    }

    [Button]
    private void Cancel()
    {
        animateTokenSource?.Cancel();
    }

    [Button("Cancel 1s")]
    private void CancelTimed()
    {
        animateTokenSource?.CancelAfterSlim(1000);
    }

    private async UniTaskVoid RestorePos(CancellationToken token)
    {
        originalPos = transform.position;

        while (this != null && !token.IsCancellationRequested)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(resetTime), cancellationToken: token);

            if (token.IsCancellationRequested) return;

            transform.position = originalPos;
            Debug.Log($"Reset Position for {gameObject.name} to {originalPos}");
        }
    }
}
