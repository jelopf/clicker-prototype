using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Клик")]
    public int coinsPerClick = 10;

    [Header("Авто-доход")]
    public int coinsPerSecond = 0;

    [Header("Улучшение")]
    public int upgradeCost = 100;
    public int upgradeIncomeBonus = 2;
    public float upgradeCostMultiplier = 1.2f;

    [Header("Состояние")]
    public int totalCoins = 0;

    public float UpgradeProgress => upgradeCost <= 0
        ? 0
        : Mathf.Clamp01((float)totalCoins / upgradeCost);

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InvokeRepeating(nameof(AddAutoIncome), 1f, 1f);
    }

    public void OnClick()
    {
        if (totalCoins >= upgradeCost) return;
        totalCoins = Mathf.Min(totalCoins + coinsPerClick, upgradeCost);
    }

    private void AddAutoIncome()
    {
        if (coinsPerSecond > 0)
            totalCoins = Mathf.Min(totalCoins + coinsPerSecond, upgradeCost);
    }

    public bool CanUpgrade() => totalCoins >= upgradeCost;

    public void TryUpgrade()
    {
        if (!CanUpgrade()) return;

        totalCoins -= upgradeCost;
        coinsPerSecond += upgradeIncomeBonus;
        upgradeCost = Mathf.RoundToInt(upgradeCost * upgradeCostMultiplier);
    }
}