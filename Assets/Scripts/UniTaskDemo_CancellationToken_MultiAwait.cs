using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[ExecuteAlways]
public class UniTaskDemo_CancellationToken_MultiAwait : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objectsToAnimate = new List<GameObject>();

    [SerializeField]
    float duration = 1f;

    CancellationTokenSource animateTokenSource;

    [Button]
    private void Animate()
    {
        AnimateAsync().Forget();
    }

    private async UniTask AnimateAsync()
    {
        animateTokenSource?.Cancel();
        animateTokenSource = new CancellationTokenSource();
        animateTokenSource.RegisterRaiseCancelOnDestroy(this);

        CancellationToken animateToken = animateTokenSource.Token;

        List<UniTask> tasks = new List<UniTask>(objectsToAnimate.Count);
        foreach (var toAnimate in objectsToAnimate)
        {
            //Animate and put returned task into list
            UniTask animateTask = toAnimate.GetComponent<IAnimate>().Animate(duration, animateToken);
            tasks.Add(animateTask);
        }

        await UniTask.WhenAll(tasks);

        if(!animateToken.IsCancellationRequested)
        {
            Debug.Log("All animations are complete!");
        }
        else
        {
            Debug.LogError("Waiting for all animations cancelled!");
        }
    }

    [Button]
    private void Cancel()
    {
        animateTokenSource?.Cancel();
    }

    private void OnValidate()
    {
        for(int i = objectsToAnimate.Count - 1; i >= 0; i--)
        {
            var obj = objectsToAnimate[i];
            if (obj != null && obj.GetComponent<IAnimate>() == null)
            {
                objectsToAnimate.RemoveAt(i);
            }
        }
    }

    [Button]
    private void ResetChildren()
    {
        int children = transform.childCount;
        float childRot = 360f / children;
        Vector3 newPosition = new Vector3(0f, -5f, -1.5f);

        for(int i = 0; i < children; i++)
        {
            var child = transform.GetChild(i);
            child.transform.localPosition = Quaternion.Euler(0f, childRot * i, 0f) * newPosition;
            child.transform.rotation = Quaternion.identity;
        }
    }
}
