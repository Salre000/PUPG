using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bGMSlider;
    public Slider sESlider;
    public Slider masterSlider;

    private void Start()
    {

        audioMixer.GetFloat("BGM_Volume", out float bgmVolume);
        bgmVolume += 100;
        bgmVolume *= 0.8f;
        bGMSlider.value = bgmVolume;
        audioMixer.GetFloat("SE_Volume", out float seVolume);
        seVolume += 100;
        seVolume *= 0.8f;

        sESlider.value = seVolume;
        audioMixer.GetFloat("Master_Volume", out float masterVolume);
        masterVolume += 100;
        masterVolume *= 0.8f;

        masterSlider.value = masterVolume;
    }

    public void SetBGM(float volume)
    {
        //bGMSlider.value = volume;

        volume -= 80;




        audioMixer.SetFloat("BGM_Volume", volume);
    }

    public void SetSE(float volume)
    {
        sESlider.value = volume;

        volume -= 80;
        volume *= 0.8f;


        audioMixer.SetFloat("SE_Volume", volume);
    }
    public void SetMaster(float volume)
    {
        masterSlider.value = volume;

        volume -= 80;
        volume *= 0.8f;


        audioMixer.SetFloat("Master_Volume", volume);
    }
}
