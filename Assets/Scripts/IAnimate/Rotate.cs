using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Rotate : MonoBehaviour, IAnimate
{
    public Vector3 rotationToAdd;

    public async UniTask Animate(float duration, CancellationToken token)
    {
        float lerp = 0f;

        while (this != null && !token.IsCancellationRequested && lerp < 1f)
        {
            transform.rotation *= Quaternion.Euler(rotationToAdd * Time.deltaTime);
            lerp += Time.deltaTime / duration;

            await UniTask.Yield();
        }
    }
}
