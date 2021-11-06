using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Balance : MonoBehaviour
{
#region String
    private static string BalancePrefs = "Balance";
#endregion

#region SerializeField
    [SerializeField] private Canvas ParentCanvas;
#endregion

#region Integer
    private static int PlayerBalance;
#endregion

#region Text
    private Text BalanceText {get => gameObject.GetComponent<Text>();}
#endregion

#region Boolean
    private bool IsCanvasActive {get {return ParentCanvas.enabled;}}
#endregion

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

    public void TryMakeOrder(int Price, out bool isConfirmTransaction)
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
        }

        UpdatePlayerPrefs();
    }

    public void ReturnBalance(int valueToBack)
    {
        PlayerBalance += valueToBack;
        UpdatePlayerPrefs();
    }

    private void UpdatePlayerPrefs()
    {
        PlayerPrefs.SetInt(BalancePrefs, PlayerBalance);
        PlayerPrefs.Save();
    }
}
