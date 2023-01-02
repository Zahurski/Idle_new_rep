using Cysharp.Threading.Tasks;
using System.Threading;

namespace IdleTycoon.Ads
{
    public interface IAdsInitializer
    {
        bool IsInitialized { get; }
        void Initialize();
        UniTask WaitInitialize(CancellationToken cancellationToken);
    }
}