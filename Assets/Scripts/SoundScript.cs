using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundScript : MonoBehaviour {



    static new AudioSource audio;
    static AudioClip s_boss;
    static AudioClip s_map1;
    static AudioClip s_map2;
    static AudioClip s_map3;
    // Use this for initialization

    public static void BossMusic()
    {
        audio.clip = s_boss;
        audio.Play();
    }
    public static void MapMusic()
    {
        int i = Random.Range(0, 2);
        switch (i)
        {
            case 0:
                audio.clip = s_map1;
                break;
            case 1:
                audio.clip = s_map2;
                break;
            case 2:
                audio.clip = s_map3;
                break;
        }
        audio.Play();
    }

    static void muteaudio()
    {
        if (audio != null)
        {
            if (FileData.GetSound() == 0)
            {
                audio.mute = true;
            }
            else
            {
                audio.mute = false;
            }
        }
    }

    void Awake () {
        audio = this.GetComponent<AudioSource>();
        s_boss = Resources.Load("Audio/boss") as AudioClip;
        s_map1 = Resources.Load("Audio/map1") as AudioClip;
        s_map2 = Resources.Load("Audio/map2") as AudioClip;
        s_map3 = Resources.Load("Audio/map3") as AudioClip;
    }

    // Update is called once per frame
    void Update () {
        muteaudio();
    }
}
