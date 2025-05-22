using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text coinsText;
    public TMP_Text hintsText;
    public TMP_Text skipsText;
    public TMP_Text eliminationsText;

    public Button buyHintButton;
    public Button buySkipButton;
    public Button buyEliminationButton;

    void Start()
    {
        // Initialize all displays
        UpdateAllDisplays();

        // Hook up purchase buttons
        buyHintButton.onClick.AddListener(OnBuyHint);
        buySkipButton.onClick.AddListener(OnBuySkip);
        buyEliminationButton.onClick.AddListener(OnBuyElimination);
    }

    void OnBuyHint()
    {
        bool ok = GameDataManager.Instance.TryBuyHint();
        if (!ok) Debug.Log("Not enough coins for a Hint!");
        UpdateAllDisplays();
    }

    void OnBuySkip()
    {
        bool ok = GameDataManager.Instance.TryBuySkip();
        if (!ok) Debug.Log("Not enough coins for a Skip!");
        UpdateAllDisplays();
    }

    void OnBuyElimination()
    {
        bool ok = GameDataManager.Instance.TryBuyElimination();
        if (!ok) Debug.Log("Not enough coins for an Elimination!");
        UpdateAllDisplays();
    }

    /// <summary>
    /// Updates all UI text fields in one go.
    /// </summary>
    void UpdateAllDisplays()
    {
        coinsText.text        = GameDataManager.Instance.Coins.ToString();
        hintsText.text        = GameDataManager.Instance.Hints.ToString();
        skipsText.text        = GameDataManager.Instance.Skips.ToString();
        eliminationsText.text = GameDataManager.Instance.Eliminations.ToString();
    }
}
