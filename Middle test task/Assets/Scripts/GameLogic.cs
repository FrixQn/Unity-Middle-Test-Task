using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
#region Alphabet
    private List<char> Alphabet = new List<char>(){'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З','И','Й','К','Л','М','О','П','Р','С','Т','У','Ф','Х','Ц','Ч','Ч','Ш','Щ','Ъ','Ы','Ь','Э','Ю','Я'}; 
#endregion
    [SerializeField] private LevelConfiguration[] Levels;
    private LevelConfiguration Level;
    private Text CurrentLevelNumber { get => FindObjectOfType<LevelIDText>().GetComponent<Text>();}
    private Keyboard Keyboard {get => FindObjectOfType<Keyboard>();}
    private WordsLine WordsLine {get => FindObjectOfType<WordsLine>();}
    private List<ImagePiece> PieceOfImage {get => GetImages();}


    private void Start()
    {
        if (LevelComplete.GetCompletedLevels() < Levels.Length){
            Level = Levels[LevelComplete.GetCompletedLevels()];
            CurrentLevelNumber.text = Level.LevelID.ToString();
            Keyboard.SetKeys(GenerateKeys());

            WordsLine.CreateWordFieldAndSetAnswer(Level.GetAnswer());
            SetImagesAndPrice(new List<ImagePiece>{PieceOfImage[0], PieceOfImage[1], PieceOfImage[2], PieceOfImage[3]});
        }else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        
    }

    private void Update()
    {
        //CurrentLevelNumber.text = Level.LevelID.ToString();
        // if (PieceOfImage[0].ImageSprite == null){
        //     PieceOfImage[0].ImageSprite = Level.GetImage(0);
        //     PieceOfImage[1].ImageSprite = Level.GetImage(1);
        //     PieceOfImage[2].ImageSprite = Level.GetImage(2);
        //     PieceOfImage[3].ImageSprite = Level.GetImage(3);
        // }
    }

    public void GoToMenu()
    {
        SoundController.Play(SoundController.statClickOnTheButton);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private List<char> GenerateKeys()
    {
        List<char> chars = new List<char>();
        for (int i = 0; i < Level.GetAnswer().Length; i++)
        {
            chars.Add(Level.GetAnswer().ToUpper()[i]);
        }


        for (int k = 0; k < Keyboard.KeysCount - Level.GetAnswer().Length; k++)
        {
            chars.Add(Alphabet[Random.Range(0, Keyboard.KeysCount)]);
        }

        return RandomizeList(chars);
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

        for (int i = 0; i < pieces.Length; i++)
        {
            array.Add(pieces[i]);
        }

        return array;
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
