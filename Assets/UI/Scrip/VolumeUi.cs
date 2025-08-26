using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider; 
    [SerializeField] private Slider sfxSlider;   

    private void Start()
    {
        if (AudioManager.Instance != null)
        {
            if (musicSlider)
            {
                musicSlider.value = AudioManager.Instance.GetMusicVolume();
                musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            }
            if (sfxSlider)
            {
                sfxSlider.value = AudioManager.Instance.GetSFXVolume();
                sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
            }
        }
    }

    private void OnDestroy()
    {
        if (AudioManager.Instance != null)
        {
            if (musicSlider) musicSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMusicVolume);
            if (sfxSlider) sfxSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetSFXVolume);
        }
    }
}
