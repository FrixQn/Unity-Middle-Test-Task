using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text LevelsInfo;
    void Start()
    {
        if (PlayerPrefs.HasKey(LevelComplete.GetCompletedLevelsPrefs()))
        {
            LevelsInfo.text = PlayerPrefs.GetInt(LevelComplete.GetCompletedLevelsPrefs()).ToString();
        }else
        {
            PlayerPrefs.SetInt(LevelComplete.GetCompletedLevelsPrefs(), 0);
            LevelsInfo.text = PlayerPrefs.GetInt(LevelComplete.GetCompletedLevelsPrefs()).ToString();
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
        LevelsInfo.text = PlayerPrefs.GetInt(LevelComplete.GetCompletedLevelsPrefs()).ToString();
    }
}
