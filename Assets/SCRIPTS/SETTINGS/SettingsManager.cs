using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider master;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfxs;


    private void Start()
    {

        music.value = AudioManager.Instance.musicVolume;
        sfxs.value = AudioManager.Instance.sfxsVolume;
        master.value = AudioManager.Instance.masterVolume;
    }

    public void SetMusicValue()
    {

        audioMixer.SetFloat("Music", MathF.Log10(music.value) * 20);
        AudioManager.Instance.musicVolume = music.value;
    }

    public void SetSfxsValue()
    {
        audioMixer.SetFloat("SFXs", MathF.Log10(sfxs.value) * 20);
        AudioManager.Instance.sfxsVolume = sfxs.value;
    }

    public void SetMasterValue()
    {
        audioMixer.SetFloat("Master", MathF.Log10(master.value) * 20);
        AudioManager.Instance.masterVolume = master.value;

    }


    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }

}
