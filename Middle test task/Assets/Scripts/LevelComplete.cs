using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelComplete : MonoBehaviour
{
    private Canvas WinningHUD {get => GetComponent<Canvas>();}
    [SerializeField] private Transform Field;
    [SerializeField] private Sprite EmptyCellSprite;
    [SerializeField] private Font DefaultFont;

    private const string CompleteLevelsStatsPrefs = "TotalLevelsComplete";
    private static int LevelsCompleted;

    private void Start()
    {
        if (PlayerPrefs.HasKey(CompleteLevelsStatsPrefs))
        {
            LevelsCompleted = PlayerPrefs.GetInt(CompleteLevelsStatsPrefs);
        }
        else
        {
            PlayerPrefs.SetInt(CompleteLevelsStatsPrefs, 0);
            LevelsCompleted = 0;
        }

        PlayerPrefs.Save();
    }

    public static string GetCompletedLevelsPrefs()
    {
        return CompleteLevelsStatsPrefs;
    }
    
    public void ShowWinningCanvas(bool state)
    {
        WinningHUD.enabled = state;
    }

    public static void Complete()
    {
        LevelsCompleted++;
        PlayerPrefs.SetInt(CompleteLevelsStatsPrefs, LevelsCompleted);

        PlayerPrefs.Save();
    }

    public static int GetCompletedLevels()
    {
        return LevelsCompleted;
    }

    public void CreateWinnigWord(string Answer, Color Default)
    {
        if (Answer.Length <= WordsLine.MAX_WORD_LENGTH && Answer.Length >= WordsLine.MIN_WORD_LENGTH){
            for (int i = 0; i < Answer.Length; i++)
            {
                GameObject CharCell = new GameObject("Word char " + i, new Type[] {typeof(RectTransform), typeof(CanvasRenderer), typeof(Image)});
                RectTransform CharCellRectTransform = CharCell.GetComponent<RectTransform>();

                CharCell.transform.SetParent(Field);

                CharCellRectTransform.sizeDelta = new Vector2(90, 90);
                CharCellRectTransform.localPosition = new Vector3(WordsLine.CenterOffset(CharCellRectTransform.sizeDelta.x, i, Answer.Length), 0f, 0f);
                CharCellRectTransform.localScale = Vector3.one;
                CharCellRectTransform.pivot = new Vector2(0.5f, 0.5f);

                CharCell.GetComponent<Image>().sprite = EmptyCellSprite;
                CharCell.GetComponent<Image>().color = Default;

                GameObject Symbol = new GameObject(Answer[i].ToString());
                Symbol.transform.SetParent(CharCell.transform);
                Text SymbolText = Symbol.AddComponent<Text>();

                SymbolText.rectTransform.localPosition = Vector3.zero;
                SymbolText.rectTransform.localScale = Vector3.one;
                SymbolText.font = DefaultFont;
                SymbolText.fontSize = (int)(SymbolText.rectTransform.sizeDelta.x * 0.75f);
                SymbolText.fontStyle = FontStyle.Bold;
                SymbolText.alignment = TextAnchor.MiddleCenter;
                SymbolText.color = Color.white;
                SymbolText.text = Answer[i].ToString().ToUpper();
            }
        }
        else
        {
            throw new System.Exception("The length of the word can not be higer than MAX_WORD_LENGTH");
        }
    }

    public void NextLevel()
    {
        SoundController.Play(SoundController.statClickOnTheButton);
        SceneManager.LoadScene(1);
    }

}
