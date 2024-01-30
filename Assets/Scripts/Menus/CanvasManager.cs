using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] AudioMixer audioMixer;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void SliderMaster(float masterVolume)
    {
        audioMixer.SetFloat("master", Mathf.Log10(masterVolume) * 20);
    }
    public void SliderMusic(float musicVolume)
    {
        audioMixer.SetFloat("volumeMusic", Mathf.Log10(musicVolume) * 20);
    }

    public void SliderFx(float fxVolume)
    {
        audioMixer.SetFloat("volumeFx", Mathf.Log10(fxVolume) * 20);
    }

    public void OptionsBackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
