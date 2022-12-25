using UnityEngine;
using UnityEngine.Advertisements;

public class ReviveAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private string AppStoreID;
    [SerializeField] private string GooglePlayID; //Сюда забей свой токен
    private GameObject player;
    private void Start()
    {
        Advertisement.AddListener(this);
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(GooglePlayID, true);
        }
        player = GameObject.Find("Player");
    }
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        Debug.Log("0");
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            GameObject.Find("Death Canvas/Death Buttons/Resume").SetActive(false);
            player.SetActive(true);
            player.SendMessage("Revive");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("3");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == "rewardedVideo")
        {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}
