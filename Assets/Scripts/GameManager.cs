using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int goldOre = 0;
    public float gold = 0;
    public float forgingEfficiency = 1f;
    public int efficiencyUpgradeLevel = 0;

    [Header("UI Elements (TMP)")]
    public TextMeshProUGUI oreText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI efficiencyText;

    public Button collectOreButton;
    public Button smeltButton;
    public Button upgradeEfficiencyButton;

    void Start()
    {
        UpdateUI();

        collectOreButton.onClick.AddListener(() =>
        {
            goldOre++;
            UpdateUI();
        });

        smeltButton.onClick.AddListener(() =>
        {
            int oreCost = 10;
            if (goldOre >= oreCost)
            {
                float goldProduced = forgingEfficiency;
                goldOre -= oreCost;
                gold += goldProduced;
                UpdateUI();
            }
        });

        upgradeEfficiencyButton.onClick.AddListener(() =>
        {
            float cost = efficiencyUpgradeLevel + 1;
            if (gold >= cost)
            {
                gold -= cost;
                forgingEfficiency += 0.5f;
                efficiencyUpgradeLevel++;
                UpdateUI();
            }
        });
    }

    public void UpdateUI()
    {
        oreText.text = $"Ore: {goldOre}";
        goldText.text = $"Gold: {gold:F1}";
        efficiencyText.text = $"Efficiency: x{forgingEfficiency:F1} (Upgrade Cost: {efficiencyUpgradeLevel + 1}G)";
    }
}
