using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    static AudioManager _instance = null;

    AudioClip s_fight;
    AudioClip s_upgrade;
    AudioClip s_move;
    AudioClip s_coin;
    AudioClip s_weapon, s_armor, s_unclick;
    AudioClip s_Wskill1,s_Wskill2, s_Wskill3;
    AudioClip s_Clear;
    public static AudioManager Instance() {
        return _instance;
    }
	// Use this for initialization
	void Start () {
        s_upgrade = Resources.Load("Audio/Effect/upgrade") as AudioClip;
        s_move = Resources.Load("Audio/Effect/move") as AudioClip;
        s_fight = Resources.Load("Audio/Effect/slash") as AudioClip;
        s_weapon = Resources.Load("Audio/Effect/getweapon") as AudioClip;
        s_armor = Resources.Load("Audio/Effect/getarmor") as AudioClip;
        s_coin = Resources.Load("Audio/Effect/coin") as AudioClip;
        s_unclick = Resources.Load("Audio/Effect/unclick") as AudioClip;
        s_Wskill1 = Resources.Load("Audio/Effect/warriorskill1") as AudioClip;
        s_Wskill2 = Resources.Load("Audio/Effect/warriorskill2") as AudioClip;
        s_Wskill3 = Resources.Load("Audio/Effect/warriorskill3") as AudioClip;
        s_Clear = Resources.Load("Audio/Effect/clear") as AudioClip;
        if (_instance == null)
            _instance = this;
	}

    public void playClear()
    {
        GetComponent<AudioSource>().volume = 0.7f;
        GetComponent<AudioSource>().PlayOneShot(s_Clear);
        GetComponent<AudioSource>().volume = 1.0f;
    }

    public void playWarriorskil(int i)
    {
        GetComponent<AudioSource>().volume = 1.0f;
        switch (i)
        {
            case 1:
                GetComponent<AudioSource>().PlayOneShot(s_Wskill1);
                break;
            case 2:
                GetComponent<AudioSource>().PlayOneShot(s_Wskill2);
                break;
            case 3:
                GetComponent<AudioSource>().PlayOneShot(s_Wskill3);
                break;
        }
    }
    public void playUnclick()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(s_unclick);
    }

    public void playCoin()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(s_coin);
    }

    public void playWeapon()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(s_weapon);
    }

    public void playArmor()
    {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(s_armor);
    }

    public void playFight() {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(s_fight);
    }

    public void playUpgrade() {
        GetComponent<AudioSource>().volume = 1.0f;
        GetComponent<AudioSource>().PlayOneShot(s_upgrade);
    }

    public void playMove() {
        GetComponent<AudioSource>().volume = 0.5f;
        GetComponent<AudioSource>().PlayOneShot(s_move);
    }
}
