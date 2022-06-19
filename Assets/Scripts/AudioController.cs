using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource warriorSound;
    [SerializeField] private AudioSource peasantSound;
    [SerializeField] private AudioSource invasionSound;
    
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;
    
    private float _musicVolume = 1f;
    private float _soundVolume = 1f;
    
    public void SetMusicVolume(float vol)
    {
        _musicVolume = musicVolumeSlider.value;
    }

    public void SetSoundVolume(float vol)
    {
        _soundVolume = soundVolumeSlider.value;
    }
    
    private void Update()
    {
        musicAudioSource.volume = _musicVolume;
        warriorSound.volume = _soundVolume;
        peasantSound.volume = _soundVolume;
        invasionSound.volume = _soundVolume;
    }
}
