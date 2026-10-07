using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Тексты")]
    public TextMeshProUGUI perSecondText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI upgradeBonusText;
    public TextMeshProUGUI progressText;

    [Header("Прогресс-бар")]
    public RectTransform progressBg;
    public RectTransform progressFill;

    [Header("Кнопка")]
    public Button upgradeButton;

    private void Update()
    {
        if (GameManager.Instance == null) return;
        var gm = GameManager.Instance;

        if (perSecondText) perSecondText.text = FormatNumber(gm.coinsPerSecond);
        if (upgradeCostText) upgradeCostText.text = FormatNumber(gm.upgradeCost);
        if (upgradeBonusText) upgradeBonusText.text = "+" + FormatNumber(gm.upgradeIncomeBonus);
        
        if (progressText)
        {
            int current = Mathf.Min(gm.totalCoins, gm.upgradeCost);
            progressText.text = $"{FormatNumber(current)}/{FormatNumber(gm.upgradeCost)}";
        }

        if (progressFill != null && progressBg != null)
        {
            float progress = gm.UpgradeProgress;
            float maxWidth = progressBg.rect.width - 6;
            progressFill.sizeDelta = new Vector2(maxWidth * progress, progressFill.sizeDelta.y);
        }

        if (upgradeButton) upgradeButton.interactable = gm.CanUpgrade();
    }

    private string FormatNumber(int n)
    {
        if (n >= 1_000_000_000) return (n / 1_000_000_000f).ToString("0.#") + "B";
        if (n >= 1_000_000)     return (n / 1_000_000f).ToString("0.#") + "M";
        if (n >= 1_000)         return (n / 1_000f).ToString("0.#") + "k";
        return n.ToString();
    }
}