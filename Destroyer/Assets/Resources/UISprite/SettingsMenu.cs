using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private Text VolumeTextUI = null;

    private void Start()
    {
        LoadValues();
    }
    public void VolumeSlider(float volume)
    {
        VolumeTextUI.text = volume.ToString("0.0");
        AudioListener.volume = volume;
    }
    public void SaveVolumeButton()
    {
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);
        PlayerPrefs.SetInt("QualityIndex", QualitySettings.GetQualityLevel());
        LoadValues();
    }
    void LoadValues()
    {
        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        volumeSlider.value = volumeValue;
        AudioListener.volume = volumeValue;
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityIndex"));
    }
    public void TimeStop()
    {
        Time.timeScale = 0.0f;
    }
    public void TimeStart()
    {
        Time.timeScale = 1.0f;
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
