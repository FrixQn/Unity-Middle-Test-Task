using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
#region Alphabet
    private List<char> Alphabet = new List<char>(){'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З','И','Й','К','Л','М','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я'}; 
#endregion

#region SerializedFields
    [SerializeField] private LevelConfiguration[] Levels;
#endregion

#region LevelConfiguration
    private LevelConfiguration Level;
#endregion

#region Text
    private Text CurrentLevelText { get => FindObjectOfType<LevelIDText>().GetComponent<Text>();}
#endregion

#region Keyboard
    private Keyboard Keyboard {get {return FindObjectOfType<Keyboard>();}}
#endregion

#region WordsLine
    private WordsLine WordsLine {get => FindObjectOfType<WordsLine>();}
#endregion

#region Lists
    private List<ImagePiece> PieceOfImage;

    private List<char> GeneratedKeys
    {
        get {
        List<char> chars = new List<char>();
        int i = 0;
        int k = 0;

        while (i < Level.Answer.Length){
            chars.Add(Level.Answer.ToUpper()[i]);
            i++;
        }

        while (k < Keyboard.KeysCount - Level.Answer.Length)
        {
            chars.Add(Alphabet[Random.Range(0, Keyboard.KeysCount)]);
            k++;
        }

        return RandomizeList(chars);
        }
    }

    private List<char> RandomizeList(List<char> charArray)
    {   
        int Count = charArray.Count;
        List<char> InputArray = charArray;
        List<char> OutputArray = new List<char>();

        int i = 0;
        while(i < Count)
        {
            int RandomIndex = Random.Range(0, InputArray.Count);
            OutputArray.Add(charArray[RandomIndex]);
            InputArray.Remove(charArray[RandomIndex]);

            i++;
        }

        return OutputArray;
    }

    private List<ImagePiece> GetImages()
    {
        List<ImagePiece> array = new List<ImagePiece>();
        ImagePiece[] pieces = FindObjectsOfType<ImagePiece>();
        int i = 0;

        while(i < pieces.Length)
        {
            array.Add(pieces[i]);
            i++;
        }

        return array;
    }
#endregion

    private void Start()
    {
        PieceOfImage = GetImages();

        if (LevelComplete.CompletedLevels < Levels.Length){

            Level = Levels[LevelComplete.CompletedLevels];
            CurrentLevelText.text = Level.LevelID.ToString();
            Keyboard.SetKeys(GeneratedKeys);

            WordsLine.CreateWordFieldAndSetAnswer(Level.Answer);
            SetImagesAndPrice(PieceOfImage);
        }else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void GoToMenu()
    {
        SoundController.Play(SoundController.statClickOnTheButton);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    

    private void SetImagesAndPrice(List<ImagePiece> list)
    {
        for (int i = 0; i < Level.ImagesCount; i++)
        {
            list[i].ImageSprite = Level.GetImage(i);
            list[i].Price = Level.GetImgaePrice(i);
        }
    }
}
