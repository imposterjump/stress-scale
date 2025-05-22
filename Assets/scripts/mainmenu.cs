using UnityEngine;
using UnityEngine.SceneManagement;    // for loading scenes
using UnityEngine.UI;                 // for UI Button/Text
using TMPro;

public class mainmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void playgame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void quitgame()
    {
        Application.Quit();
    }
    public void homepage()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
