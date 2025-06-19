using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for scene loading


public class ResourceManager : MonoBehaviour
{
    // Resources
    private int gold = 0;
    private int ore = 0;

    // Automation
    private int mineCount = 0;
    private int smelterCount = 0;

    // UI References
    public TMP_Text goldText;
    public TMP_Text oreText;
    public TMP_Text mineText;
    public TMP_Text smelterText;

    public Button mineOreButton;
    public Button smeltOreButton;
    public Button buyMineButton;
    public Button buySmelterButton;

    // Timing
    private float autoMineTimer = 0f;
    private float autoSmeltTimer = 0f;
    private const float interval = 1f;

    public TMP_Text statusText;

    private float statusMessageTimer = 0f;
    private const float statusMessageDuration = 2f;

public Button forgeCrownButton;



    void Start()
    {
        UpdateUI();

        // Hook buttons
        mineOreButton.onClick.AddListener(MineOre);
        smeltOreButton.onClick.AddListener(SmeltOre);
        buyMineButton.onClick.AddListener(BuyMine);
        buySmelterButton.onClick.AddListener(BuySmelter);
        forgeCrownButton.onClick.AddListener(ForgeCrown);
    }

    void Update()
{
    autoMineTimer += Time.deltaTime;
    autoSmeltTimer += Time.deltaTime;

    if (autoMineTimer >= interval)
    {
        ore += mineCount;
        autoMineTimer = 0f;
    }

    if (autoSmeltTimer >= interval)
    {
        int smeltable = Mathf.Min(smelterCount, ore);
        ore -= smeltable;
        gold += smeltable;
        autoSmeltTimer = 0f;
    }

    if (statusMessageTimer > 0f)
    {
        statusMessageTimer -= Time.deltaTime;
        if (statusMessageTimer <= 0f)
        {
            statusText.text = "";
        }
    }

    UpdateUI();
}


    void MineOre()
    {
        ore += 1;
        UpdateUI();
    }

    void SmeltOre()
{
    if (ore > 0)
    {
        ore -= 1;
        gold += 1;
    }
    else
    {
        ShowStatus("Not enough ore to smelt!");
    }
    UpdateUI();
}

void BuyMine()
{
    if (gold >= 20)
    {
        gold -= 20;
        mineCount += 1;
    }
    else
    {
        ShowStatus("Not enough gold to buy a mine!");
    }
    UpdateUI();
}

    void BuySmelter()
    {
        if (gold >= 10)
        {
            gold -= 10;
            smelterCount += 1;
        }
        else
        {
            ShowStatus("Not enough gold to buy a smelter!");
        }
        UpdateUI();
    }
void ForgeCrown()
{
    if (gold >= 10000)
    {
        gold -= 10000;
        UpdateUI();
        SceneManager.LoadScene("Victory");
    }
    else
    {
        ShowStatus("You need 10,000 gold to forge the crown!");
    }
}

void ShowStatus(string message)
{
    statusText.text = message;
    statusMessageTimer = statusMessageDuration;
}

    void UpdateUI()
    {
        goldText.text = $"Gold: {gold}";
        oreText.text = $"Ore: {ore}";
        mineText.text = $"Mines: {mineCount}";
        smelterText.text = $"Smelters: {smelterCount}";
    }
}
