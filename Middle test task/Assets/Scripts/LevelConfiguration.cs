using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create new level", order = 51)]

public class LevelConfiguration : ScriptableObject
{
    [SerializeField] private int LevelId;
    public int LevelID {get => LevelId;}
    [SerializeField] private string LevelAnswer;
    public string Answer {get { return LevelAnswer;}}
    [SerializeField] private Sprite[] Images = new Sprite[4];
    public Sprite GetImage(int imgId)
    {
        return Images[imgId];
    }
    public int ImagesCount {get => Images.Length;}
    [SerializeField] private int[] Price = new int[4];
    public int GetImgaePrice(int imgId)
    {
        return Price[imgId];
    }

}



