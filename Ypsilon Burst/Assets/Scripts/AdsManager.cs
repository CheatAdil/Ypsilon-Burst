using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private string AppStoreID;
    [SerializeField] private string GooglePlayID; //Сюда забей свой токен
    private GameObject player;
    private void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize(GooglePlayID, false);
        }
        player = GameObject.Find("Player");
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }
    
}
