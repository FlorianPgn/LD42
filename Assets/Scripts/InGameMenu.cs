using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    public GameObject MenuPanel;
    public Slider Volume;
    public Button ExitMenuBtn;
    public Button ExitGameBtn;
    public bool DisplayMenu;

    private void Start()
    {
        if (AudioManager.instance != null)
        {
            Volume.value = AudioManager.instance.GetVolume();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || DisplayMenu)
        {
            MenuPanel.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Escape) && !DisplayMenu)
        {
            MenuPanel.SetActive(false);
        }
    }

    public void ExitToMenu()
    {
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayMenuTheme();
        }
        SceneManager.LoadScene("Menu");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    public void SetVolume(float value)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetVolume(value);
        }
    }
}
