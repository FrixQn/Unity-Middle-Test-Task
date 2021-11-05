using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
#region List
    private List<KeyAction> Actions = new List<KeyAction>();
    private static List<KeyAction> statActions = new List<KeyAction>();
#endregion

#region Button
    private Button[] Buttons {get {return GetComponentsInChildren<Button>();}}
#endregion
    public int KeysCount {get => Buttons.Length;}
    public static WordsLine WordLine {get => FindObjectOfType<WordsLine>();}

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

    public static void SetRightKeyActions(List<KeyAction> newKeyActions)
    {
        statActions = newKeyActions;
    }

    public static List<KeyAction> GetAllActions()
    {
        return statActions;
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
        if (WordsLine.IsAnswerWrong()){
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
    Action1 = 'А',
    Action2 = 'Б',
    Action3 = 'В',
    Action4 = 'Г',
    Action5 = 'Д',
    Action6 = 'Е',
    Action7 = 'Ё',
    Action8 = 'Ж',
    Action9 = 'З',
    Action10 = 'И',
    Action11 = 'Й',
    Action12 = 'К',
    Action13 = 'Л',
    Action14 = 'М',
    Action15 = 'Н',
    Action16 = 'О',
    Action17 = 'П',
    Action18 = 'Р',
    Action19 = 'С',
    Action20 = 'Т',
    Action21 = 'У',
    Action22 = 'Ф',
    Action23 = 'Х',
    Action24 = 'Ц',
    Action25 = 'Ч',
    Action26 = 'Ш',
    Action27 = 'Щ',
    Action28 = 'Ъ',
    Action29 = 'Ы',
    Action30 = 'Ь',
    Action31 = 'Э',
    Action32 = 'Ю',
    Action33 = 'Я',

}



