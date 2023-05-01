using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CoroutineAnimate : MonoBehaviour, IAnimate
{
    public Vector3 destination;

    public async UniTask Animate(float duration, CancellationToken token)
    {
        //UniTask lets us await a coroutine easily
        await AnimateRoutine(duration, token);
    }

    private IEnumerator AnimateRoutine(float duration, CancellationToken token)
    {
        float lerp = 0f;
        Vector3 startPos = transform.position;

        while (this != null && !token.IsCancellationRequested && lerp < 1f)
        {
            lerp = Mathf.Clamp01(lerp + Time.deltaTime / duration);
            transform.position = Vector3.Lerp(startPos, destination, lerp);

            yield return null;
        }
    }
}
