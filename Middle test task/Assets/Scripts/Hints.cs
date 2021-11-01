using UnityEngine;

public class Hints : MonoBehaviour
{
    [SerializeField] private int Hint1Price;
    [SerializeField] private int Hint2Price;
    private Canvas HintsCanvas {get => gameObject.GetComponent<Canvas>();} 
    private WordsLine WordsLine { get => FindObjectOfType<WordsLine>();}
    private Balance Balance {get => FindObjectOfType<Balance>();}

    public void ShowHints()
    {
        HintsCanvas.enabled = true;
        SoundController.Play(SoundController.statClickOnTheButton);
    }

    public void HideHints()
    {
        HintsCanvas.enabled = false;
        SoundController.Play(SoundController.statClickOnTheButton);
    }

    public void ShowRandomCell()
    {
        bool PurchaseResultSuccesed;
        bool MayToHint;
        
        Balance.MakeOrder(Hint2Price, out PurchaseResultSuccesed);
        if (PurchaseResultSuccesed == true)
        {
            WordsLine.ShowRandomChar(out MayToHint);
            if (MayToHint == false)
            {
                Balance.ReturnBalance(Hint2Price);
                SoundController.Play(SoundController.statMistake);
            }else
            {
                SoundController.Play(SoundController.statPurchasingSound);
            }
        }
    }

    public void DeactivateNonUsefulChars()
    {
        bool PurchaseResultSuccesed;
        bool MayToHint;

        Balance.MakeOrder(Hint1Price, out PurchaseResultSuccesed);
        if (PurchaseResultSuccesed == true)
        {
            WordsLine.HideOtherChars(out MayToHint);
            if (MayToHint == false)
            {
                Balance.ReturnBalance(Hint1Price);
                SoundController.Play(SoundController.statMistake);
            }else
            {
                SoundController.Play(SoundController.statPurchasingSound);
            }
        }
    }
}

public enum Hint{
    Hint1 = 0,
    Hint2 = 1,
};
