using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

[ExecuteAlways]
public class UniTaskDemo : MonoBehaviour
{
    [SerializeField]
    Vector3 from;

    [SerializeField]
    Vector3 to;

    [SerializeField]
    float speed = 1f;

    void OnEnable()
    {
        Animate().Forget();
    }

    [Button]
    private async UniTask Animate()
    {
        float dir = 1f;
        float lerp = 0f;

        while(enabled)
        {
            lerp = Mathf.Clamp01(lerp + ((Time.deltaTime / speed) * dir));
            transform.position = Vector3.Lerp(from, to, lerp);

            if(lerp <= 0f || lerp >= 1f)
            {
                dir *= -1f;
            }

            await UniTask.Yield();
        }
    }
}
