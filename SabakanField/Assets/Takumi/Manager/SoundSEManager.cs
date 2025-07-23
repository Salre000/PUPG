using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSEManager : MonoBehaviour
{

    public static SoundSEManager instance;

    private const int MAXSOURECES = 200;
    private List<AudioSource> audioSources = new List<AudioSource>(MAXSOURECES);
    [SerializeField,Header("オーディオミキサー")]
    private AudioMixer audioMixer;

    [SerializeField, Header("足音のSE")]
    private AudioClip []footstep;
    [SerializeField, Header("敵に弾を当てたときの音")]
    private AudioClip enemyHit;
    [SerializeField, Header("アーマーが割られたときの音")]
    private AudioClip armorBreak;
    [SerializeField, Header("敵を倒したときの音")]
    private AudioClip enemykilled;
    [SerializeField, Header("撃たれたときの音")]
    private AudioClip playerHit;
    [SerializeField, Header("ゲームが終了する時の音")]
    private AudioClip gameEnd;
    [SerializeField, Header("弾の補給をするときの音")]
    private AudioClip ReChege;



    public void Awake()
    {

        OptionDataClass.GetOptionData();


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

            return;
        }
        Initialize();
    }
    private void Start()
    {
        Debug.Log(OptionDataClass.GetMasterVolume()+"SS");

        audioMixer.SetFloat("Master_Volume", OptionDataClass.GetMasterVolume() - 80);
        audioMixer.SetFloat("BGM_Volume", OptionDataClass.GetBGMVolume() - 80);
        audioMixer.SetFloat("SE_Volume", OptionDataClass.GetSEVolume() - 80);


    }

    private void Initialize()
    {



        for (int i = 0; i < MAXSOURECES; i++)
        {
            GameObject soundObject = new GameObject("soundObject" + i.ToString());

            audioSources.Add(soundObject.AddComponent<AudioSource>());
            audioSources[i].outputAudioMixerGroup= audioMixer.FindMatchingGroups("Master")[2];
            soundObject.transform.parent = transform;

        }
    }


    public void PlayFootstep(Vector3 pos)
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 1;

        audioSource.minDistance = 1;
        audioSource.maxDistance = 10;

        audioSource.transform.position = pos;

        audioSource.loop = false;

        audioSource.clip = footstep[Random.Range(0,3)];

        audioSource.volume = 1;

        audioSource.Play();

    }
    public void PlayEnemyHit()
    {

        AudioSource audioSource = GetUsableAudioSource();

        audioSource.spatialBlend = 0;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.loop = false;

        audioSource.clip = enemyHit;
        audioSource.volume = 1;

        audioSource.Play();

    }
    public void PlayArmorBreak()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = armorBreak;
        audioSource.volume = 1;

        audioSource.Play();

    }
    public void PlayEnemykilled()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = enemykilled;
        audioSource.volume = 1;

        audioSource.Play();

    }
    public void PlayplayerHit()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = playerHit;
        audioSource.volume = 1;

        audioSource.Play();

    }
    public void PlayGameEnd()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = gameEnd;
        audioSource.volume = 1;

        audioSource.Play();

    }
    public void PlayReChege()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = ReChege;
        audioSource.volume = 1;

        audioSource.Play();

    }

    private AudioSource GetUsableAudioSource()
    {
        for (int i = 0; i < MAXSOURECES; i++)
        {
            if (audioSources[i].isPlaying) continue;

            return audioSources[i];
        }


        Debug.Log("音をならすオブジェクトが余っていない");
        return null;
    }



}
