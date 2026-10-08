using UnityEngine;
using YG;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private bool wasShownThisFill = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        float progress = GameManager.Instance.UpgradeProgress;

        if (progress < 0.5f)
            wasShownThisFill = false;

        if (progress >= 0.5f && !wasShownThisFill)
        {
            ShowInterstitial();
            wasShownThisFill = true;
        }
    }

    private void ShowInterstitial()
    {
        YG2.InterstitialAdvShow();
        Debug.Log("[AdManager] Запрос на interstitial");
    }
}