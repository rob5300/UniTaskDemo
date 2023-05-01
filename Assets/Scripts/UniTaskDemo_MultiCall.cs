using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using UnityEngine;

public class UniTaskDemo_MultiCall : MonoBehaviour
{
    [SerializeField]
    Vector3 add;

    [SerializeField]
    float resetTime = 2f;

    Vector3 originalPos;

    [Button]
    private async UniTask Animate()
    {
        RestorePos().Forget();

        while (this != null)
        {
            transform.position += add * Time.deltaTime;

            await UniTask.Yield();
        }
    }

    private async UniTaskVoid RestorePos()
    {
        originalPos = transform.position;

        while (this != null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(resetTime));

            transform.position = originalPos;
            Debug.Log($"Reset Position for {gameObject.name} to {originalPos}");
        }
    }
}
