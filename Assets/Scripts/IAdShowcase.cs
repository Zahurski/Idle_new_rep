using Cysharp.Threading.Tasks;
using IdleTycoon.Ads;

namespace IdleTycoon
{
    public interface IAdShowcase
    {
        UniTask<(bool isCompleted, AdShowResultEnum status)> ShowRewardAdAsync();
    }
}