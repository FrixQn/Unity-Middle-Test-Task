using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuyPanel : MonoBehaviour
{
#region  SerializedFields
    [SerializeField] private Text PriceText;
#endregion

#region GameObjects
    private GameObject Child { get => gameObject.transform.GetChild(0).gameObject;}
#endregion

#region Balance
    private Balance PlayerBalance {get {return FindObjectOfType<Balance>();}}
#endregion

#region Integer
    private int Price {get; set;}
#endregion

#region  Boolean
    private bool isTransactionSuccess;
    public bool IsTransactionSuccessed {get {return isTransactionSuccess;}}
#endregion

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
        PlayerBalance.TryMakeOrder(Price, out isTransactionSuccess);
        if (isTransactionSuccess)
        {
            SoundController.Play(SoundController.statPurchasingSound);
            SetState(false);
        }
    }
}
