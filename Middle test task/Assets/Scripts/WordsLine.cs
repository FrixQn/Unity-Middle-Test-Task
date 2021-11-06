using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WordsLine : MonoBehaviour
{
#region Constants
    public const int MAX_WORD_LENGTH = 9;
    public const int MIN_WORD_LENGTH = 3;
    public const int ConstantPixelWidth = 840;
#endregion

#region Color
    private Color DefaultColor = Color.white;
#endregion

#region SerializableFields
    [SerializeField] private Color UnchekedKeyColor;
    [SerializeField] private Color RightKeyColor;
    [SerializeField] private Color WrongKeyColor;
    [SerializeField] private Font DefaultFont;
    [SerializeField] private Sprite EmptyCell;
#endregion

#region Transform
    private Transform Field {get => gameObject.transform;}
#endregion

#region KeyAction
    KeyAction[] CellsOnTheLine;
#endregion

#region String
    //private string Answer {get; set;}
#endregion

#region LevelComplete
    private LevelComplete Complete {get => FindObjectOfType<LevelComplete>();}
#endregion
    
#region Boolean
    private bool isWrongAnswer;
    public bool IsAnswerWrong {get {return isWrongAnswer;}}
    private bool isCleanedKeys = false;
#endregion

    public void CreateWordField()
    {
        if (GameLogic.Answer.Length <= MAX_WORD_LENGTH && GameLogic.Answer.Length >= MIN_WORD_LENGTH){

            CellsOnTheLine = new KeyAction[GameLogic.Answer.Length];
            for (int i = 0; i < GameLogic.Answer.Length; i++)
            {
                GameObject CharCell = new GameObject("Word char " + i, new Type[] {typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)});
                RectTransform CharCellRectTransform = CharCell.GetComponent<RectTransform>();

                CharCell.transform.SetParent(Field);

                CharCellRectTransform.sizeDelta = new Vector2(85, 85);
                CharCellRectTransform.localPosition = new Vector3(CenterOffset(CharCellRectTransform.sizeDelta.x, i, GameLogic.Answer.Length), 65f, 0f);
                CharCellRectTransform.localScale = Vector3.one;
                CharCellRectTransform.pivot = new Vector2(0.5f, 0.5f);

                CharCell.GetComponent<Image>().sprite = EmptyCell;
            }
        }
        else
        {
            throw new System.Exception("The length of the word can not be higer than MAX_WORD_LENGTH");
        }
        
    }

    public static int CenterOffset(float cellSizeX, int index, int total)
    {
        
        if (total % 2 == 0)
        {
            int Middle = total/2;
            int Interval = (int)(ConstantPixelWidth - (cellSizeX * total))/total;

            
            if (index < Middle)
            {
                float Pose = -cellSizeX/2f - Interval * (total/10f)/2f - ((cellSizeX + Interval * (total/10f)) *(Middle - (index + 1))) ;
                return (int)Pose;
            }
            else
            {
                float Pose = cellSizeX/2f + Interval * (total/10f)/2f + ((cellSizeX + Interval * (total/10f)) *(index - Middle)) ; 
                return (int)Pose;
            }

        }else
        {
            int Middle = (total/2);
            int Interval = (int)(ConstantPixelWidth - (cellSizeX * total))/total;

            if(index < Middle)
            {
                float Pose = ((Middle - index)*-(cellSizeX)) - Interval * (total/10f) * (Middle - index);
                return (int)Pose;
            }
            else
            {
                float Pose = ((Middle - index)*-(cellSizeX)) + Interval * (total/10f) * (index - Middle);
                return (int)Pose;
            }   
        }
    }
    
    public void PlaceCell(char symbol, KeyAction keyAction, bool isHint, out bool placingResult)
    {
        placingResult = false;
        int i = 0;
        if (isHint == false){
            while (i < gameObject.transform.childCount)
            {
                GameObject Cell = gameObject.transform.GetChild(i).gameObject;
                if (Cell.transform.childCount == 0)
                {
                    CellsOnTheLine[i] = keyAction;
                //
                    keyAction.SetFilledCell(Cell);
                //
                    Cell.GetComponent<Image>().color = UnchekedKeyColor;
                    Cell.AddComponent<Button>();
                    Cell.GetComponent<Button>().onClick.AddListener(keyAction.CleanCell);
                //
                    GameObject wordChar = new GameObject(symbol.ToString());
                    wordChar.transform.parent = Cell.transform;
                    Text symbolText = wordChar.AddComponent<Text>();
                    symbolText.rectTransform.localPosition = Vector3.zero;
                    symbolText.rectTransform.localScale = Vector3.one;
                    symbolText.font = DefaultFont;
                    symbolText.fontSize = (int)(symbolText.rectTransform.sizeDelta.x * 0.75f);
                    symbolText.fontStyle = FontStyle.Bold;
                    symbolText.alignment = TextAnchor.MiddleCenter;
                    symbolText.color = Color.white;
                    symbolText.text = symbol.ToString();

                    placingResult = true;
                    break;
                }
            i++;
            }
        }
        else
        {
            for (int x = 0; x < GameLogic.Answer.Length; x++){
                if (GameLogic.Answer.ToUpper()[x] == symbol)
                {
                    GameObject Cell = gameObject.transform.GetChild(x).gameObject;
                    if (Cell.transform.childCount == 0)
                    {
                        CellsOnTheLine[x] = keyAction;
                    //
                        keyAction.SetFilledCell(Cell);
                    //
                        Cell.GetComponent<Image>().color = RightKeyColor;
                    //
                        GameObject wordChar = new GameObject(symbol.ToString());
                        wordChar.transform.parent = Cell.transform;
                        Text symbolText = wordChar.AddComponent<Text>();
                        symbolText.rectTransform.localPosition = Vector3.zero;
                        symbolText.rectTransform.localScale = Vector3.one;
                        symbolText.font = DefaultFont;
                        symbolText.fontSize = (int)(symbolText.rectTransform.sizeDelta.x * 0.75f);
                        symbolText.fontStyle = FontStyle.Bold;
                        symbolText.alignment = TextAnchor.MiddleCenter;
                        symbolText.color = Color.white;
                        symbolText.text = symbol.ToString();

                        placingResult = true;
                        break;
                    }
                }
            }
        }
        CheckResult();
    }
    
    private void Update()
    {
        ReturnColorAndRemoveButton();
    }

    private void ReturnColorAndRemoveButton()
    {
        int i = 0;
        while(i < gameObject.transform.childCount)
        {
            if (gameObject.transform.GetChild(i).gameObject.transform.childCount == 0)
            {
                gameObject.transform.GetChild(i).GetComponent<Image>().color = DefaultColor;
                Destroy(gameObject.transform.GetChild(i).GetComponent<Button>());
            }
            i++;
        }
    }
    
    public void ClearObject(GameObject _gameObject)
    {
        if (_gameObject.GetComponent<Image>().color != RightKeyColor){
            Destroy(_gameObject.transform.GetChild(0).gameObject);
        }
    }
    
    private void CheckResult()
    {
        string answer = "";
        int i = 0;
        while (i < GameLogic.Answer.Length)
        {
            GameObject cell = gameObject.transform.GetChild(i).gameObject;
            if (cell.transform.childCount == 0)
            {
                break;
            }
            answer += gameObject.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text[0];
            i++;
            
        }
        if (i == GameLogic.Answer.Length)
        {
            if (GameLogic.Answer.ToUpper() != answer)
            {
                SoundController.Play(SoundController.statMistake);
                StartCoroutine(ShowMistake());
                isWrongAnswer = true;
            }else
            {
                Complete.ShowWinningCanvas(true);
                Complete.CreateWinnigWord(GameLogic.Answer, UnchekedKeyColor);
                LevelComplete.Complete();
            }
        }
    }

    public void ClearLine()
    {
        int i = 0;
        while (i < CellsOnTheLine.Length)
        {
            if (gameObject.transform.GetChild(i)?.gameObject.GetComponent<Image>().color != RightKeyColor && gameObject.transform.GetChild(i).gameObject.GetComponent<Image>().color != DefaultColor)
            {
                CellsOnTheLine[i]?.SetClickable(true);
                CellsOnTheLine[i]?.SetFilledCell(null);
                Destroy(gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject);
                
            }
            i++;
        }
        isWrongAnswer = false;
    }
    
    private IEnumerator ShowMistake()
    {
        List<Image> RightKeys = FindRightKeys();

        int x = 0;
        while (x < GameLogic.Answer.Length)
        {
            gameObject.transform.GetChild(x).GetComponent<Image>().color = WrongKeyColor;
            x++;
        }
        yield return new WaitForSeconds(0.5f);
        int i = 0;
        while (i < GameLogic.Answer.Length)
        {
            Image CellImage = transform.GetChild(i).GetComponent<Image>();
            CellImage.color = UnchekedKeyColor;
            i++;
        }
        int k = 0;
        while (k < RightKeys.Count)
        {
            RightKeys[k].color = RightKeyColor;
            k++;
        }
    }

    public void ShowRandomChar(out bool isMayToPlace)
    {
        
        isMayToPlace = false;
        ClearLine();
        char[] RightKeys = GameLogic.Answer.ToUpper().ToCharArray();
        List<int> EnabledSymbolIndex = new List<int>();
        int i = 0;

        while(i < gameObject.transform.childCount)
        {
            GameObject Cell = gameObject.transform.GetChild(i).gameObject;
            if (Cell.transform.childCount == 0)
            {
                EnabledSymbolIndex.Add(i);
            }
            i++;
        }

        if (EnabledSymbolIndex.Count > 0)
        {
            isMayToPlace = true;
            int RndSymbol = UnityEngine.Random.Range(0, EnabledSymbolIndex.Count);

            Keyboard.FindKeyActionByCharValue(RightKeys[EnabledSymbolIndex[RndSymbol]])?.CallHintAction();
        }
    }

    public void HideOtherChars(out bool isMayToHide)
    {
        
        if (isCleanedKeys == false){
        char[] RightCells = GameLogic.Answer.ToUpper().ToCharArray();
        List<KeyAction> AllActions = Keyboard.GetAllActions();
        List<KeyAction> RightActions = new List<KeyAction>();

        for (int i = 0; i < GameLogic.Answer.Length; i++)
        {
            for (int x = 0; x < AllActions.Count; x++)
            {
                if (AllActions[x].GetKey() == RightCells[i])
                {
                    RightActions.Add(AllActions[x]);
                    AllActions.Remove(AllActions[x]);
                    break;
                }
            }
        }
        int j = 0;

        while(j < AllActions.Count)
        {
            if (AllActions[j].GetCellChar() != null){
                AllActions[j].CleanCell();
            }
            AllActions[j].SetClickable(false);
            AllActions[j].ClearButtonText();
            j++;
        }

        Keyboard.SetRightKeyActions(RightActions);
        isMayToHide = true;
        isCleanedKeys = true;
        }else
        {
            isMayToHide = false;
        }
            
    }

    private List<Image> FindRightKeys()
    {
        List<Image> CurrentList = new List<Image>();

        for (int i = 0; i < GameLogic.Answer.Length; i++)
        {
            Image Cell = transform.GetChild(i).GetComponent<Image>();
            if (Cell.color == RightKeyColor)
            {
                CurrentList.Add(Cell);
            }
        }

        return CurrentList;
    }
}
