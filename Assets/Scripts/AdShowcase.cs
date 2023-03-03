using Cysharp.Threading.Tasks;
using IdleTycoon.Ads;

namespace IdleTycoon
{
    public class AdShowcase : IAdShowcase
    {
        private readonly IAdsInitializer adsInitializer;
        private readonly IRewardedAd rewardedAd;

        public AdShowcase(IAdsInitializer adsInitializer, IRewardedAd rewardedAd)
        {
            this.rewardedAd = rewardedAd;
            this.adsInitializer = adsInitializer;
        }

        public async UniTask<(bool isCompleted, AdShowResultEnum status)> ShowRewardAdAsync()
        {
            if (adsInitializer.IsInitialized)
            {
                AdShowResultEnum result = await rewardedAd.ShowAsync();
                return (true, result);
            }
            else
            {
                return (false, default(AdShowResultEnum));
            }
        }
    }
}