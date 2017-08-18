using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //So the winbox victory can read data
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    public GameObject pauseMenu;

    private float startTime;
    public float silverTime;
    public float goldTime;

    private void Start()
    {
        //access this anywhere
        instance = this;
        pauseMenu.SetActive(false);
        startTime = Time.time;
    }

    public void TogglePauseMenu()
    {
        //tells if pauseMenu is active or not
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //winbox victory method
    public void Victory()
    {
        float duration = Time.time - startTime;
        if (duration < goldTime)
        {
            GameManager.Instance.currency += 50;
        }
        else if (duration < silverTime)
        {
            GameManager.Instance.currency += 25;
        }
        else
        {
            GameManager.Instance.currency += 10;
        }
        GameManager.Instance.Save();

        string saveString = "";
        // "30&60&45"
        saveString += duration.ToString();
        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);

        SceneManager.LoadScene("MainMenu");
    }
}
