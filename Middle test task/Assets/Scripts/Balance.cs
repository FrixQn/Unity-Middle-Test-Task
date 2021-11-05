using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Balance : MonoBehaviour
{
    private static string BalancePrefs = "Balance";
    private static int PlayerBalance;

    private Text BalanceText {get => gameObject.GetComponent<Text>();}
    [SerializeField] private Canvas ParentCanvas;
    private bool IsCanvasActive {get {return ParentCanvas.enabled;}}

    private void Start()
    {

        if (PlayerPrefs.HasKey(BalancePrefs))
        {
            PlayerBalance = PlayerPrefs.GetInt(BalancePrefs);
        }
        else
        {
            PlayerPrefs.SetInt(BalancePrefs, 1000000);
            PlayerBalance = PlayerPrefs.GetInt(BalancePrefs);
        }

        PlayerPrefs.Save();
        DrawBalance();
    }

    void Update()
    {
        if (IsCanvasActive)
        {
            DrawBalance();
        }
    }

    private void DrawBalance()
    {
        BalanceText.text = PlayerBalance.ToString();
    }

    public void MakeOrder(int Price, out bool isConfirmTransaction)
    {
        isConfirmTransaction = false;
        if (Price < 0)
        {
            isConfirmTransaction = false;
            throw new System.Exception("The price of can not be negative");
        }
        
        if (PlayerBalance - Price >= 0)
        {
            PlayerBalance -= Price;
            isConfirmTransaction = true;
            DrawBalance();
        }

        UpdatePlayerPrefs();
    }

    public void ReturnBalance(int valueToBack)
    {
        PlayerBalance += valueToBack;
        DrawBalance();
        UpdatePlayerPrefs();
    }

    private void UpdatePlayerPrefs()
    {
        PlayerPrefs.SetInt(BalancePrefs, PlayerBalance);
        PlayerPrefs.Save();
    }
}
