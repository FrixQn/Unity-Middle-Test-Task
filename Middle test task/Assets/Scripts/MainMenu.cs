using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
#region SerializeField
    [SerializeField] private Text LevelsInfo;
#endregion

    void Start()
    {
        if (PlayerPrefs.HasKey(LevelComplete.CompletedLevelsPrefs))
        {
            LevelsInfo.text = PlayerPrefs.GetInt(LevelComplete.CompletedLevelsPrefs).ToString();
        }else
        {
            PlayerPrefs.SetInt(LevelComplete.CompletedLevelsPrefs, 0);
            LevelsInfo.text = PlayerPrefs.GetInt(LevelComplete.CompletedLevelsPrefs).ToString();
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
        SoundController.Play(SoundController.statClickOnTheButton);
    }

    public void ClearProgres()
    {
        PlayerPrefs.DeleteAll();
        LevelsInfo.text = PlayerPrefs.GetInt(LevelComplete.CompletedLevelsPrefs).ToString();
    }
}
