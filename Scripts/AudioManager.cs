using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgSound;
    //public AudioSource sfxSound;
    //public AudioClip buttonClickSound;
    private EndingUI ending;

    public List<AudioClip> bgClips = new List<AudioClip>();

    private void Awake()
    {
        if (Instance == null)
        {
            bgSound = GetComponent<AudioSource>();
            ending = GetComponent<EndingUI>();
            //sfxSound = gameObject.AddComponent<AudioSource>();
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
            bgClips.Add(Resources.Load<AudioClip>("BGMClip"));
            bgClips.Add(Resources.Load<AudioClip>("SchoolBGM"));
            bgClips.Add(Resources.Load<AudioClip>("Ending1Another"));
            bgClips.Add(Resources.Load<AudioClip>("Ending2Happy"));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���� ���ؼ� ���� �´� ����� �ҽ��� ������ �÷���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        AudioClip clipToPlay = null;

        switch (sceneName) //���� ���ϰ� ������ ����
        {
            case "StartScene":
                clipToPlay = bgClips.Find(clip => clip.name == "BGMClip");
                break;
            case "MainScene":
                clipToPlay = bgClips.Find(clip => clip.name == "SchoolBGM");
                break;
            case "EndingScene":
                if (GameManager.Instance.npc.GetRelationship() <= 1) clipToPlay = bgClips.Find(clip => clip.name == "Ending1Another");
                if (GameManager.Instance.npc.GetRelationship() >= 2) clipToPlay = bgClips.Find(clip => clip.name == "Ending2Happy");
                break;
        }

        if (clipToPlay != null) // �� �÷���
        {
            BgSoundPlay(clipToPlay);
        }
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.Play();
    }

    public void SetBgSoundVolume(float volume)
    {
        if (bgSound == null) return;

        bgSound.volume = volume;
    }

    //public void PlaySfx()
    //{
    //    sfxSound.PlayOneShot(buttonClickSound);
    //}

    //public void SetPlaySfxVolume(float volume)
    //{
    //    sfxSound.volume = volume;
    //
}
