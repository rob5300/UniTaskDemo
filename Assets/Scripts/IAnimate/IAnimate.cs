using Cysharp.Threading.Tasks;
using System.Threading;

public interface IAnimate
{
    public UniTask Animate(float duration, CancellationToken token);
}
