using UnityEngine;
using GoogleMobileAds.Api;

public class AdIniitialize : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initState => { });
    }
}
