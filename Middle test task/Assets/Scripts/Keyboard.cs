using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
#region List
    private List<KeyAction> Actions = new List<KeyAction>();
    private static List<KeyAction> statActions = new List<KeyAction>();
    public static List<KeyAction> GetAllActions(){return statActions;}
#endregion

#region Button
    private Button[] Buttons {get {return GetComponentsInChildren<Button>();}}
#endregion
    
#region Integer
    public int KeysCount {get => Buttons.Length;}
#endregion
    
#region WordLine
    public static WordsLine WordLine {get => FindObjectOfType<WordsLine>();}
#endregion

#region KeyAction 
    public static KeyAction FindKeyActionByCharValue(char value)
    {
        int i = 0;
        KeyAction current = null;

        while (i < statActions.Count)
        {
            if (statActions[i].GetKey() == value && statActions[i].isClickable())
            {
                current = statActions[i];
                break;
            }
            i++;
        }

        return current;
    }
#endregion

    public void SetKeys(List<char> keysArray)
    {
        if (keysArray.Count < Buttons.Length)
        {
            throw new System.Exception("The array of char keys does not equals the keyboard buttons count");
        }
        else
        {
            for (int i = 0; i < Buttons.Length; i++)
            {
                Text buttonSymbol = Buttons[i].GetComponentInChildren<Text>();
                buttonSymbol.text = keysArray[i].ToString();

                Actions.Add(new KeyAction(keysArray[i], Buttons[i]));

                Buttons[i].onClick.AddListener(Actions[i].CallAction);
                Buttons[i].onClick.AddListener(PlayTap);
            }
            statActions = Actions;
        }
        
    }

    private void PlayTap()
    {
        SoundController.Play(SoundController.statClickOnTheButton);
    }

    public static void SetRightKeyActions(List<KeyAction> newKeyActions)
    {
        statActions = newKeyActions;
    }

}

public class KeyAction
{
    private ActionKeyIdentificator Key;
    private WordsLine WordsLine {get => Keyboard.WordLine;}
    private Button KeyButton;
    private GameObject FilledCell;

    public char GetKey()
    {
        return (char)Key;
    }

    public KeyAction(char key, Button keyButton)
    {
        Key = (ActionKeyIdentificator)key;
        KeyButton = keyButton;
    }

    public void CallAction()
    {
        bool isPlacingWasSuccesed;
        WordsLine.PlaceCell((char)Key, this, false, out isPlacingWasSuccesed);
        if(isPlacingWasSuccesed){
            KeyButton.interactable = false;
        }
    }

    public void CallHintAction()
    {
        bool isPlacingWasSuccesed;
        WordsLine.PlaceCell((char)Key, this, true, out isPlacingWasSuccesed);
        if(isPlacingWasSuccesed){
            KeyButton.interactable = false;
        }
    }

    public void CleanCell()
    {
        KeyButton.interactable = true;
        if (WordsLine.IsAnswerWrong){
            WordsLine.ClearLine();
        }else
        {
            WordsLine.ClearObject(FilledCell);
        }
        
    }

    public GameObject GetCellChar()
    {
        return FilledCell;
    }

    public void SetClickable(bool state)
    {
        KeyButton.interactable = state;
    }

    public void ClearButtonText()
    {
        Keyboard.Destroy(KeyButton.GetComponentInChildren<Text>());
    }

    public bool isClickable()
    {
        return KeyButton.interactable;
    }

    public void SetFilledCell(GameObject gameObject)
    {
        FilledCell = gameObject;
    }
}

public enum ActionKeyIdentificator
{
    Action1 = '??',
    Action2 = '??',
    Action3 = '??',
    Action4 = '??',
    Action5 = '??',
    Action6 = '??',
    Action7 = '??',
    Action8 = '??',
    Action9 = '??',
    Action10 = '??',
    Action11 = '??',
    Action12 = '??',
    Action13 = '??',
    Action14 = '??',
    Action15 = '??',
    Action16 = '??',
    Action17 = '??',
    Action18 = '??',
    Action19 = '??',
    Action20 = '??',
    Action21 = '??',
    Action22 = '??',
    Action23 = '??',
    Action24 = '??',
    Action25 = '??',
    Action26 = '??',
    Action27 = '??',
    Action28 = '??',
    Action29 = '??',
    Action30 = '??',
    Action31 = '??',
    Action32 = '??',
    Action33 = '??',

}



