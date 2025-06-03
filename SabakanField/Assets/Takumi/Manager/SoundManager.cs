using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{
    private static SoundList ShotList;
    public static AudioClip GetShotSound(GanObject.ConstancyGanType type) { return ShotList.SoundLists[(int)type]; }
    private static SoundList ShotAddList;

    private static SoundList InGameSoundList;
    public static AudioClip GetShotAddSound(int ID) { return ShotAddList.SoundLists[ID]; }

    private static List<AudioSource> SoundSource=new List<AudioSource>(50);

    public static AudioClip GetInGameSoundList(SoundEnum.SoundEnumType type) { return InGameSoundList.SoundLists[(int)type]; }

    public static void StartSound(Vector3 pos,AudioClip clip,System.Action addAction=null) 
    {
        AudioSource audioSource = GetAudioSource();

        audioSource.gameObject.transform.position = pos;
        audioSource.gameObject.GetComponent<DisappearInTime>().SetEndFancs(() => !audioSource.isPlaying);
        audioSource.PlayOneShot(clip);

        if (addAction == null) return;

        audioSource.gameObject.GetComponent<DisappearInTime>().SetEndActions(() => addAction());

    }
    private static AudioSource GetAudioSource() 
    {
        for(int i = 0; i < SoundSource.Count; i++) 
        {
            if (SoundSource[i].gameObject.activeSelf) continue;
            SoundSource[i].gameObject.SetActive(true);
            return SoundSource[i];
        }

        return null;
    }
    public static void Initialize() 
    {
        ShotList = Resources.Load<SoundList>("GanShot");
        ShotAddList = Resources.Load<SoundList>("GanShotAddition");
        InGameSoundList = Resources.Load<SoundList>("SoundList");

        AudioMixer audioMixer = Resources.Load<AudioMixer>("AudioMixer");

        GameObject parentObject = new GameObject("SoundParent");

        for(int i = 0; i < SoundSource.Capacity; i++) 
        {
            GameObject soundSource = new GameObject("SoundObject");
            soundSource.transform.parent = parentObject.transform;

            AudioSource sound = soundSource.AddComponent<AudioSource>();

            sound.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

            sound.spatialBlend = 1;

            SoundSource.Add(sound);

            soundSource.AddComponent<DisappearInTime>();

            soundSource.SetActive(false);

        }


    }


}
