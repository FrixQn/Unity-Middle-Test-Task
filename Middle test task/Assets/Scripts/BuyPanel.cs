using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    private GameObject Child { get => gameObject.transform.GetChild(0).gameObject;}
    private int Price {get; set;}
    [SerializeField] private Text PriceText;
    private Balance PlayerBalance {get => FindObjectOfType<Balance>();}
    private bool isTransactionSuccess;
    public bool IsTransactionSuccessed() {return isTransactionSuccess;}

    public void SetPrice(int _Price)
    {
        Price = _Price;
        PriceText.text = _Price.ToString();
    }

    public void SetState(bool state)
    {
        gameObject.GetComponent<Button>().interactable = state;
        Child.SetActive(state);
    }

    public void BuyImage()
    {
        PlayerBalance.MakeOrder(Price, out isTransactionSuccess);
        SetState(false);
        if (isTransactionSuccess)
        {
            SoundController.Play(SoundController.statPurchasingSound);
        }
    }
}
