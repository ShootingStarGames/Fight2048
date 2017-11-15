using UnityEngine;
using System.Collections;
using System;

public class FileData : MonoBehaviour {

    
    //private static int currentscore,highscore, floor, weapown, armor, gold,dia;
    //private static string array;
    //static int language_type;
    public static string text;
    public static void SetText(string s)
    {
        text = s;
    }

    public static string GetText()
    {
        return text;
    }

    public static void SetRevive(int i)
    {
        PlayerPrefs.SetInt("Revive",i);
        SaveData();
    }
    public static int GetRevive()
    {
        return PlayerPrefs.GetInt("Revive");
    }
    public static void SetArrayvalue(string s)
    {
        PlayerPrefs.SetString("Array_value", s);
        SaveData();
    }
    public static void SetArraydamage(string s)
    {
        PlayerPrefs.SetString("Array_damage", s);
        SaveData();
    }
    public static void SetArraytype(string s)
    {
        PlayerPrefs.SetString("Array_type", s);
        SaveData();
    }
    public static void SetSkillremain(string s)
    {
        PlayerPrefs.SetString("Skill_remain", s);
        SaveData();
    }

    public static string GetArrayvalue()
    {
        return PlayerPrefs.GetString("Array_value");
    }
    public static string GetArraydamage()
    {
        return PlayerPrefs.GetString("Array_damage");
    }
    public static string GetArraytype()
    {
        return PlayerPrefs.GetString("Array_type");
    }
    public static string GetSkillremain()
    {
        return PlayerPrefs.GetString("Skill_remain");
    }

    //최고 도달지점.
    public static void SetMaxKill()
    {
        PlayerPrefs.SetInt("MaxKill", PlayerPrefs.GetInt("MaxKill") + 1);
    }
    public static int GetMaxKill()
    {
        return PlayerPrefs.GetInt("MaxKill");
    }
    public static void SetMaxLev(int i)
    {
        if(PlayerPrefs.GetInt("MaxLev")< i)
            PlayerPrefs.SetInt("MaxLev", i);
    }
    public static int GetMaxLev()
    {
        return PlayerPrefs.GetInt("MaxLev");
    }
    public static void SetMaxArmor(int i)
    {
        if (PlayerPrefs.GetInt("MaxArmor") < i)
            PlayerPrefs.SetInt("MaxArmor", i);
    }
    public static int GetMaxArmor()
    {
        return PlayerPrefs.GetInt("MaxArmor");
    }
    public static void SetMaxWeapon(int i)
    {
        if (PlayerPrefs.GetInt("MaxWeapon") < i)
            PlayerPrefs.SetInt("MaxWeapon", i);
    }
    public static int GetMaxWeapon()
    {
        return PlayerPrefs.GetInt("MaxWeapon");
    }
    public static void SetMaxFloor(int i)
    {
        if (PlayerPrefs.GetInt("MaxFloor") < i)
            PlayerPrefs.SetInt("MaxFloor", i);
    }
    public static int GetMaxFloor()
    {
        return PlayerPrefs.GetInt("MaxFloor");
    }

    public static int GetSkillUpgrade(int ch,int op)
    {
        if (ch == 1)
        {
            switch (op)
            {
                case 1:
                    return PlayerPrefs.GetInt("W_Skill_01");
                case 2:
                    return PlayerPrefs.GetInt("W_Skill_02");
                case 3:
                    return PlayerPrefs.GetInt("W_Skill_03");
                case 4:
                    return PlayerPrefs.GetInt("W_Skill_04");
                default:
                    return 0;
            }
        }
        else if(ch == 2)
        {
            switch (op)
            {
                case 1:
                    return PlayerPrefs.GetInt("M_Skill_01");
                case 2:
                    return PlayerPrefs.GetInt("M_Skill_02");
                case 3:
                    return PlayerPrefs.GetInt("M_Skill_03");
                case 4:
                    return PlayerPrefs.GetInt("M_Skill_04");
                default:
                    return 0;
            }
        }
        else
        {
            switch (op)
            {
                case 1:
                    return PlayerPrefs.GetInt("A_Skill_01");
                case 2:
                    return PlayerPrefs.GetInt("A_Skill_02");
                case 3:
                    return PlayerPrefs.GetInt("A_Skill_03");
                case 4:
                    return PlayerPrefs.GetInt("A_Skill_04");
                default:
                    return 0;
            }
        }
    }

    public static void SetSkillUpgrade(int ch,int op)
    {
        if (ch == 1)
        {
            switch (op)
            {
                case 1:
                    PlayerPrefs.SetInt("W_Skill_01", PlayerPrefs.GetInt("W_Skill_01") + 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("W_Skill_02", PlayerPrefs.GetInt("W_Skill_02") + 1);
                    break;
                case 3:
                    PlayerPrefs.SetInt("W_Skill_03", PlayerPrefs.GetInt("W_Skill_03") + 1);
                    break;
                case 4:
                    PlayerPrefs.SetInt("W_Skill_04", PlayerPrefs.GetInt("W_Skill_04") + 1);
                    break;
            }
        }
        else if(ch == 2)
        {
            switch (op)
            {
                case 1:
                    PlayerPrefs.SetInt("M_Skill_01", PlayerPrefs.GetInt("M_Skill_01") + 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("M_Skill_02", PlayerPrefs.GetInt("M_Skill_02") + 1);
                    break;
                case 3:
                    PlayerPrefs.SetInt("M_Skill_03", PlayerPrefs.GetInt("M_Skill_03") + 1);
                    break;
                case 4:
                    PlayerPrefs.SetInt("M_Skill_04", PlayerPrefs.GetInt("M_Skill_04") + 1);
                    break;
            }
        }
        else if(ch == 3)
        {
            switch (op)
            {
                case 1:
                    PlayerPrefs.SetInt("A_Skill_01", PlayerPrefs.GetInt("A_Skill_01") + 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("A_Skill_02", PlayerPrefs.GetInt("A_Skill_02") + 1);
                    break;
                case 3:
                    PlayerPrefs.SetInt("A_Skill_03", PlayerPrefs.GetInt("A_Skill_03") + 1);
                    break;
                case 4:
                    PlayerPrefs.SetInt("A_Skill_04", PlayerPrefs.GetInt("A_Skill_04") + 1);
                    break;
            }
        }
    }
    public static void Setmonsterkill(int i)
    {
        PlayerPrefs.SetInt("Monster",i);
        SaveData();
    }
    public static int Getmonsterkill()
    {
        return PlayerPrefs.GetInt("Monster");
    }
    public static void SetCurrentScore(int i)
    {
        PlayerPrefs.SetInt("Score", i);
        SaveData();
    }
    public static void SetHighScore(int i)
    {
        PlayerPrefs.SetInt("HighScore", i);
        SaveData();
    }
    public static void SetFloor(int i)
    {
        PlayerPrefs.SetInt("Floor", i);
        SaveData();
    }
    public static void Setweapon(int i)
    {
        PlayerPrefs.SetInt("Weapon", i);
        SaveData();
    }
    public static void SetArmor(int i)
    {
        PlayerPrefs.SetInt("Armor", i);
        SaveData();
    }
    public static void SetGold(int i)
    {
        PlayerPrefs.SetInt("Gold", i);
        SaveData();
    }
    public static void SetSound(int i)
    {
        PlayerPrefs.SetInt("Sound", i);
        SaveData();
    }
    public static void SetScore(int i)
    {
        PlayerPrefs.SetInt("Score", i);
    }

    public static int GetScore()
    {
        return PlayerPrefs.GetInt("Score");
    }
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore");
    }
    public static int GetFloor()
    {
        return PlayerPrefs.GetInt("Floor");
    }
    public static int GetGold()
    {
        return PlayerPrefs.GetInt("Gold");
    }

    public static int GetWeapon()
    {
        return PlayerPrefs.GetInt("Weapon");
    }
    public static int GetArmor()
    {
        return PlayerPrefs.GetInt("Armor");
    }
    public static int GetSound()
    {
        return PlayerPrefs.GetInt("Sound");
    }

    public static void SaveData()
    {
        PlayerPrefs.Save();
    }

    /**
     * 업적 관련 프리팹
     * 
     * */
    public static void SetDiamond(int reward)
    {
        PlayerPrefs.SetInt("Diamond", PlayerPrefs.GetInt("Diamond") + reward);
    }
    public static int GetDiamond()
    {
        return PlayerPrefs.GetInt("Diamond");
    }
    public static void SetAchivement_MonsterKillLevel()
    {
        PlayerPrefs.SetInt("Achivement_MonsterKill_level", PlayerPrefs.GetInt("Achivement_MonsterKill_level")+1);
    }
    public static int GetAchivement_MonsterKillLevel()
    {
        return PlayerPrefs.GetInt("Achivement_MonsterKill_level");
    }
    public static void SetAchivement_ReviveLevel()
    {
        PlayerPrefs.SetInt("Achivement_Revive_level", PlayerPrefs.GetInt("Achivement_Revive_level") + 1);
    }
    public static int GetAchivement_ReviveLevel()
    {
        return PlayerPrefs.GetInt("Achivement_Revive_level");
    }
    public static void SetAchivement_FloorLevel()
    {
        PlayerPrefs.SetInt("Achivement_Floor_level", PlayerPrefs.GetInt("Achivement_Floor_level") + 1);
    }
    public static int GetAchivement_FloorLevel()
    {
        return PlayerPrefs.GetInt("Achivement_Floor_level");
    }
    public static void SetAchivement_WeaponLevel()
    {
        PlayerPrefs.SetInt("Achivement_Weapon_level", PlayerPrefs.GetInt("Achivement_Weapon_level") + 1);
    }
    public static int GetAchivement_WeaponLevel()
    {
        return PlayerPrefs.GetInt("Achivement_Weapon_level");
    }
    public static void SetAchivement_ArmorLevel()
    {
        PlayerPrefs.SetInt("Achivement_Armor_level", PlayerPrefs.GetInt("Achivement_Armor_level") + 1);
    }
    public static int GetAchivement_ArmorLevel()
    {
        return PlayerPrefs.GetInt("Achivement_Armor_level");
    }
    public static void SetAchivement_PlayerLevel()
    {
        PlayerPrefs.SetInt("Achivement_Player_level", PlayerPrefs.GetInt("Achivement_Player_level") + 1);
    }
    public static int GetAchivement_PlayerLevel()
    {
        return PlayerPrefs.GetInt("Achivement_Player_level");
    }

    public static void SetOwn_Char(int i)
    {
        PlayerPrefs.SetInt("OwnChar", i);
    }
    public static int GetOwn_Char()
    {
        return PlayerPrefs.GetInt("OwnChar");
    }
    public static void SetCurrent_Char(int i)
    {
        PlayerPrefs.SetInt("CurrentChar", i);
    }
    public static int GetCurrent_Char()
    {
        return PlayerPrefs.GetInt("CurrentChar");
    }
    //public static string DatatoString()
    //{
    //    string s = "";
    //    s += "Score" + " " + PlayerPrefs.GetInt("Score") + " ";
    //    s += "HighScore" + " " + PlayerPrefs.GetInt("HighScore") + " ";
    //    s += "Floor" + " " + PlayerPrefs.GetInt("Floor") + " ";
    //    s += "Weapown" + " " + PlayerPrefs.GetInt("Weapon") + " ";
    //    s += "Armor" + " " + PlayerPrefs.GetInt("Armor") + " ";
    //    s += "Array" + " " + PlayerPrefs.GetString("Array") + " ";
    //    s += "Gold" + " " + PlayerPrefs.GetInt("Gold") + " ";
    //    return s;
    //}

    public static void SetFirstGame(int i)
    {
        PlayerPrefs.SetInt("FirstGame", i);
    }
    public static int GetFirstGame()
    {
        return PlayerPrefs.GetInt("FirstGame");
    }

    // 골드 광고 쿨타임 off!(광고 봤을 때)
    public static void OffAdGoldCooldown() {
        // AdGoldCooldown = false
        PlayerPrefs.SetInt("IsAdGoldCooldown", 0);
        // 타임스탬프값 저장
        DateTime time = DateTime.Now;
        PlayerPrefs.SetString("AdGoldCooldownTime", time.Ticks.ToString());
    }
    // 골드 광고 쿨타임 on!(광고 볼 수 있을 때)
    public static void OnAdGoldCooldown() {
        // AdGoldCooldown = true
        PlayerPrefs.SetInt("IsAdGoldCooldown", 1);
    }
    // 타임 스탬프값 얼마인지 확인~~ 만약 쿨타임이 다 지났다면 -1을 출력해주자~~
    public static string GetAdGoldCooldown() {
        DateTime origin = new DateTime(Convert.ToInt64(PlayerPrefs.GetString("AdGoldCooldownTime")));
        DateTime time = DateTime.Now;

        TimeSpan diff = time - origin; // 15분*60초
        int sec = (int)(15 * 60 - diff.TotalSeconds);

        // 다 지났다면?
        if (sec <= 0) {
            return "-1";
        }
        // 분과 초를 세줍시다앙
        int min = sec / 60;
        sec = sec % 60;
        return min+"분 "+sec+"초";
    }
    // 골드 광고 볼수있는지 확인
    public static int GetIsAdGoldCooldown()
    {
        // AdGoldCooldown = true
        return PlayerPrefs.GetInt("IsAdGoldCooldown");
    }

    // 다이아 광고 쿨타임 off!(광고 봤을 때)
    public static void OffAdDiaCooldown()
    {
        // AdGoldCooldown = false
        PlayerPrefs.SetInt("IsAdDiaCooldown", 0);
        // 타임스탬프값 저장
        DateTime time = DateTime.Now;
        PlayerPrefs.SetString("AdDiaCooldownTime", time.Ticks.ToString());
    }
    // 다이아 광고 쿨타임 on!(광고 볼 수 있을 때)
    public static void OnAdDiaCooldown()
    {
        // AdGoldCooldown = true
        PlayerPrefs.SetInt("IsAdDiaCooldown", 1);
    }
    // 타임 스탬프값 얼마인지 확인~~ 만약 쿨타임이 다 지났다면 -1을 출력해주자~~
    public static string GetAdDiaCooldown()
    {
        DateTime origin = new DateTime(Convert.ToInt64(PlayerPrefs.GetString("AdDiaCooldownTime")));
        DateTime time = DateTime.Now;
        TimeSpan diff = time - origin; // 15분*60초
        int sec = (int)(15 * 60 - diff.TotalSeconds);
        // 다 지났다면?
        if (sec <= 0)
        {
            return "-1";
        }
        // 분과 초를 세줍시다앙
        int min = sec / 60;
        sec = sec % 60;
        return min + "분 " + sec + "초";
    }
    // 골드 광고 볼수있는지 확인
    public static int GetIsAdDiaCooldown()
    {
        // AdGoldCooldown = true
        return PlayerPrefs.GetInt("IsAdDiaCooldown");
    }
    public static void LoadData()
    {
        Debug.Log("LoadData");
   
        if (!PlayerPrefs.HasKey("FirstGame"))
        {
            ResetData();
        }

    }

    public static void ResetData() {
        PlayerPrefs.SetInt("FirstGame", 1);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("Floor", 1);
        PlayerPrefs.SetInt("Weapon", 0);
        PlayerPrefs.SetInt("Armor", 0);
        PlayerPrefs.SetString("Array_value", "0 0 0 0 0 0 0 0 0 0 0 0");
        PlayerPrefs.SetString("Array_damage", "0 0 0 0 0 0 0 0 0 0 0 0");
        PlayerPrefs.SetString("Array_type", "0 0 0 0 0 0 0 0 0 0 0 0");
        PlayerPrefs.SetString("Skill_remain", "0 0 0 0");
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("Sound", 1);
        PlayerPrefs.SetInt("Monster", 0);
        PlayerPrefs.SetInt("MaxKill", 0);
        PlayerPrefs.SetInt("MaxLev", 0);
        PlayerPrefs.SetInt("MaxArmor", 0);
        PlayerPrefs.SetInt("MaxWeapon", 0);
        PlayerPrefs.SetInt("MaxFloor", 1);

        PlayerPrefs.SetInt("CurrentChar", 1);
        PlayerPrefs.SetInt("OwnChar", 3);

        PlayerPrefs.SetInt("Diamond", 0);
        PlayerPrefs.SetInt("Achivement_MonsterKill_level", 0);
        PlayerPrefs.SetInt("Achivement_Revive_level", 0);
        PlayerPrefs.SetInt("Achivement_Floor_level", 0);
        PlayerPrefs.SetInt("Achivement_Weapon_level", 0);
        PlayerPrefs.SetInt("Achivement_Aromor_level", 0);
        PlayerPrefs.SetInt("Achivement_Player_level", 0);

        PlayerPrefs.SetInt("W_Skill_01", 0);
        PlayerPrefs.SetInt("W_Skill_02", 0);
        PlayerPrefs.SetInt("W_Skill_03", 0);
        PlayerPrefs.SetInt("W_Skill_04", 0);

        PlayerPrefs.SetInt("M_Skill_01", 0);
        PlayerPrefs.SetInt("M_Skill_02", 0);
        PlayerPrefs.SetInt("M_Skill_03", 0);
        PlayerPrefs.SetInt("M_Skill_04", 0);

        PlayerPrefs.SetInt("A_Skill_01", 0);
        PlayerPrefs.SetInt("A_Skill_02", 0);
        PlayerPrefs.SetInt("A_Skill_03", 0);
        PlayerPrefs.SetInt("A_Skill_04", 0);

        PlayerPrefs.SetInt("IsAdGoldCooldown", 1);
        PlayerPrefs.SetInt("IsAdDiaCooldown", 1);
    }
    // Use this for initialization
    void Start()
    {
        LoadData();
        //HttpScript.GetDia(CGoogleplayGameServiceManager.GetNameGPGS());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
