using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MenuGroup;
    public GameObject OptionsMenu;

    public Toggle[] resolutionToggles;
    public Toggle fullscreenToggle;
    public Slider volume;
    public int[] screenWidths;

    private int _activeResIndex;

    private const string RES_INDEX = "screen res index";
    private const string FULLSCREEN = "fullscreen";

    private void Start()
    {
        _activeResIndex = PlayerPrefs.GetInt(RES_INDEX);
        bool isFullscreen = (PlayerPrefs.GetInt(FULLSCREEN) == 1) ? true : false;
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == _activeResIndex;
        }
        volume.value = AudioManager.instance.GetVolume();
        fullscreenToggle.isOn = isFullscreen;
        SetFullScreen(isFullscreen);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        MenuGroup.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            _activeResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
            PlayerPrefs.SetInt(RES_INDEX, _activeResIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }
        if (isFullscreen)
        {
            Resolution[] resolutions = Screen.resolutions;
            Resolution maxRes = resolutions[resolutions.Length - 1];
            Screen.SetResolution(maxRes.width, maxRes.height, true);
        }
        else
        {
            SetResolution(_activeResIndex);
        }

        PlayerPrefs.SetInt(FULLSCREEN, isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void Volume(float value)
    {
        AudioManager.instance.SetVolume(value);
    }

    public void Back()
    {
        MenuGroup.SetActive(true);
        OptionsMenu.SetActive(false);
    }
}
