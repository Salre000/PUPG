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
        audioMixer.SetFloat("Master_Volume", OptionManager.Instance.GetMasterVolume() - 80);
        audioMixer.SetFloat("BGM_Volume", OptionManager.Instance.GetBGMVolume() - 80);
        audioMixer.SetFloat("SE_Volume", OptionManager.Instance.GetSEVolume() - 80);

    }

    public void SetBGM(float volume)
    {

        
        volume -= 80;




        audioMixer.SetFloat("BGM_Volume", volume);
    }

    public void SetSE(float volume)
    {
        volume -= 80;


        audioMixer.SetFloat("SE_Volume", volume);
    }
    public void SetMaster(float volume)
    {
        volume -= 80;


        audioMixer.SetFloat("Master_Volume", volume);
    }
}
