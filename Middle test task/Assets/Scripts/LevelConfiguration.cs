using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create new level", order = 51)]

public class LevelConfiguration : ScriptableObject
{
#region  SerializableFields
    [SerializeField] private int LevelId;
    [SerializeField] private string LevelAnswer;
    [SerializeField] private Sprite[] Images = new Sprite[4];
    [SerializeField] private int[] Price = new int[4];
#endregion

#region Integer
    public int LevelID {get => LevelId;}
    public int ImagesCount {get => Images.Length;}
    public int GetImagePrice(int imgId){return Price[imgId];}
#endregion    

#region String
    public string Answer {get { return LevelAnswer;}}
#endregion

#region Sprite
    public Sprite GetImage(int imgId)
    {
        return Images[imgId];
    }
#endregion


}



