namespace IdleTycoon.Ads.Implements
{
    public interface IUnityConfigAds
    {
        bool IsDebugMode { get; }
        bool IsTestMode { get; }
        string AndroidBannerUnitId { get; }
        string IOSBannerUnitId { get; }
        AdsBannerPosition BannerPosition { get; }
        string AndroidInterstitialUnitId { get; }
        string IOSInterstitialUnitId { get; }
        string AndroidRewardedUnitId { get; }
        string IOSRewardedUnitId { get; }
        string AndroidGameID { get; }
        string IOSGameID { get; }
    }
}