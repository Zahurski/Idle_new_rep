using Cysharp.Threading.Tasks;

namespace IdleTycoon.Ads
{
    public interface IRewardedAd : IAd
    {
        UniTask<AdShowResultEnum> ShowAsync();
    }
}