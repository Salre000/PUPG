using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGMLoop : MonoBehaviour
{
    AudioSource music;
    const float START_TIME = 15.692f;
    const float END_TIME = 207.692f;
    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }
private void Update()
    {
        if (music.time >= END_TIME)
            music.time = START_TIME;
    }
}
