using Cysharp.Threading.Tasks;
using IdleTycoon.Ads;
using IdleTycoon.Meta;

namespace IdleTycoon
{
    public class Game
    {
        private readonly IAdShowcase adShowcase;

        private bool isApplySoftMoneyBonus;
        private IMetaValues metaValues;

        public Game(IAdShowcase adShowcase, IMetaValues metaValues)
        {
            this.metaValues = metaValues;
            this.adShowcase = adShowcase;
        }

        public async UniTask<bool> TryUpSoftMoneyCoefficientAsync()
        {
            var result = await adShowcase.ShowRewardAdAsync();
            if (result.isCompleted)
            {
                if (result.status == AdShowResultEnum.Completed)
                {
                    metaValues.SoftMoneyCoefficient = 2;
                    return true;
                }
            }

            return false;
        }
    }
}