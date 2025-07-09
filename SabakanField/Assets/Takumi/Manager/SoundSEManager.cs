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

    [SerializeField, Header("タイトル画面と選択画面のBGM")]
    private AudioClip titleBGM;
    [SerializeField, Header("リザルト画面のBGM")]
    private AudioClip resultBGM;
    [SerializeField, Header("足音のSE")]
    private AudioClip footstep;
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



    public void Awake()
    {
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

    /// <summary>
    /// タイトル画面と選択画面で使うBGMを再生させる関数
    /// </summary>
    public void PlayTitleBGM()
    {

        AudioSource audioSource = GetUsableAudioSource();

        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[1];

        audioSource.spatialBlend = 0;

        audioSource.loop = true;

        audioSource.clip = titleBGM;

        audioSource.Play();

    }
    /// <summary>
    /// リザルト画面で使うBGMを再生させる関数
    /// </summary>
    public void PlayResultBGM()
    {

        AudioSource audioSource = GetUsableAudioSource();

        audioSource.spatialBlend = 0;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[1];

        audioSource.loop = true;

        audioSource.clip = resultBGM;

        audioSource.Play();

    }
    public void PlayFootstep(Vector3 pos)
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 1;

        audioSource.minDistance = 1;
        audioSource.maxDistance = 5;

        audioSource.transform.position = pos;

        audioSource.loop = false;

        audioSource.clip = footstep;

        audioSource.Play();

    }
    public void PlayEnemyHit()
    {

        AudioSource audioSource = GetUsableAudioSource();

        audioSource.spatialBlend = 0;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.loop = false;

        audioSource.clip = enemyHit;

        audioSource.Play();

    }
    public void PlayArmorBreak()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = armorBreak;

        audioSource.Play();

    }
    public void PlayEnemykilled()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = enemykilled;

        audioSource.Play();

    }
    public void PlayplayerHit()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = playerHit;

        audioSource.Play();

    }
    public void PlayGameEnd()
    {

        AudioSource audioSource = GetUsableAudioSource();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[2];

        audioSource.spatialBlend = 0;

        audioSource.loop = false;

        audioSource.clip = gameEnd;

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
