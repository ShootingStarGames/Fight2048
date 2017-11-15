using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StoreScript : MonoBehaviour
{

    public Button[] weapon; //무기 버튼 배열
    public Button[] armor; //방어구 버튼 배열
    public Button[] s_button;
    public Text[] w_text; //가격 text 배열
    public Text[] a_text; //가격 text 배열
    public Text[] s_text; // 가격 text 배열
    public Text[] s_info; // 스킬 text 배열
    public Text[] w_atk; //데미지 text 배열
    public Text[] a_def; //방어력 text 배열

    private int character;
    private int Weapon, Armor;
    public Image pade;
    public Text gold_t, dia_t; // 골드 다이아 text
    private int W_money, A_money;
    private int S_dia;
    private int[] Moneyarry = { 10, 50, 500, 1000, 5000, 10000 };//가격 배열
    private int[] Diaarry = { 1, 2, 3, 4, 5, 10 };
    private int[] Upgradearry = { 1, 2, 3, 4, 5 };//무기 방어구 업그레이드 배열
    private string[] WarriorSkillArry = { "","전사의 기본 체력의 ","%를 올려 주는 패시브 스킬. ", "전체 체력의 ","%를 치유해주는 마법의 물약.", "공격력의 ","%로 범위 공격하는 강력한 기술.", "단일대상으로 공격력의 ","%를 데미지로 주는 최강 기술." };
    private string[] MagicianSkillArry = { "", "법사의 기본 스킬 ", "%를 올려 주는 패시브 스킬. ", "전체 체력의 ", "%를 치유해주는 마법의 물약.", "공격력의 ", "%로 범위 공격하는 강력한 기술.", "단일대상으로 공격력의 ", "%를 데미지로 주는 최강 기술." };

    enum CLASS_TYPE
    {
        WARRIOR = 1,
        MAGICIAN = 2,
        ARCHER = 3,
    }

    IEnumerator padeout()
    {
        for (int i = 0; i < 10; i++)
        {
            pade.color = new Vector4(1, 1, 1, (float)i / 10);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(padein());
    }
    IEnumerator padein()
    {
        for (int i = 10; i >= 0; i--)
        {
            pade.color = new Vector4(1, 1, 1, (float)i / 10);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void ShowSkillinfo(int i)//스킬 정보 표시
    {
        switch (character)
        {
            case (int)CLASS_TYPE.WARRIOR:
                s_info[i - 1].text = WarriorSkillArry[2 * i - 1] + SkillSript.GetUpgrade(1,i) + WarriorSkillArry[2 * i];
                break;
            case (int)CLASS_TYPE.MAGICIAN:
                s_info[i - 1].text = MagicianSkillArry[2 * i - 1] + SkillSript.GetUpgrade(2,i) + MagicianSkillArry[2 * i];
                break;
            case (int)CLASS_TYPE.ARCHER:
                break;
        }

    }

    public void BuySkill(int i)
    {
        setitem();
        S_dia = FileData.GetSkillUpgrade(character, i);
        if (S_dia > 5)
            S_dia = 5;

        if (FileData.GetDiamond() - Diaarry[S_dia] >= 0)
        {
            FileData.SetDiamond(-Diaarry[S_dia]);
            FileData.SetSkillUpgrade(character, i);
            //AudioManager.Instance().playWeapon();
            dia_t.text = FileData.GetDiamond().ToString();
            StartCoroutine(padeout());
            S_dia = FileData.GetSkillUpgrade(character, i);
            if (S_dia > 5)
                S_dia = 5;
            s_text[i - 1].text = "x" + Diaarry[S_dia];
            SkillSript.SetUpgrade(character,i);
            ShowSkillinfo(i);
        }
        else
        {
            AudioManager.Instance().playUnclick();
        }
    }
    public void BuyWeapon()
    {
        if (FileData.GetGold() - Moneyarry[W_money] * (Weapon + 1) >= 0)
        {
            Weapon += 1;
            FileData.SetGold(FileData.GetGold() - Moneyarry[W_money] * Weapon);
            FileData.Setweapon(Weapon);
            FileData.SetMaxWeapon(Weapon);
            AudioManager.Instance().playWeapon();
            gold_t.text = FileData.GetGold().ToString();
            StartCoroutine(padeout());
            Setmoney();
        }
        else
        {
            AudioManager.Instance().playUnclick();
        }
        setitem();
    }
    public void BuyArmor()
    {
        if (FileData.GetGold() - Moneyarry[A_money] * (Armor + 1) >= 0)
        {
            Armor += 1;
            FileData.SetGold(FileData.GetGold() - Moneyarry[A_money] * Armor);
            FileData.SetArmor(Armor);
            FileData.SetMaxArmor(Armor);
            AudioManager.Instance().playArmor();
            gold_t.text = FileData.GetGold().ToString();
            StartCoroutine(padeout());
            Setmoney();
        }
        else
        {
            AudioManager.Instance().playUnclick();
        }
        setitem();
    }

    private void Setmoney()
    {
        W_money = Weapon / 10;
        A_money = Armor / 10;
        if (W_money > 5)
        {
            W_money = 5;
        }
        if (A_money > 5)
        {
            A_money = 5;
        }
    }

    // Use this for initialization
    void Start()
    {
        character = FileData.GetCurrent_Char();
        for (int i = 0; i < 4; i++)
        {
            w_atk[i].text = "";
            a_def[i].text = "";
        }
        Weapon = FileData.GetWeapon();
        Armor = FileData.GetArmor();
        Setmoney();
        setitem();
        setfirstskill();
        ShowSkillinfo(1);
        ShowSkillinfo(2);
        ShowSkillinfo(3);
        ShowSkillinfo(4);

    }

    private void setfirstskill()
    {
        for(int i = 1; i < 5; i++)
        {
            S_dia = FileData.GetSkillUpgrade(character, i);
            if (S_dia > 5)
                S_dia = 5;
            s_text[i-1].text = "x" + Diaarry[S_dia];
        }
      
    }

    private void setitem()
    {
        if (Weapon >= 0 && Weapon <= 10)
        {
            weapon[0].gameObject.SetActive(true);
            w_text[0].text = "x" + Moneyarry[W_money] * (Weapon + 1);
            w_atk[0].text = Weapon.ToString();
        }
        else if (Weapon > 10 && Weapon <= 20)
        {
            w_text[0].text = "";
            weapon[0].gameObject.SetActive(false);
            weapon[1].gameObject.SetActive(true);
            w_text[1].text = "x" + Moneyarry[W_money] * (Weapon + 1);
            w_atk[0].text = 10.ToString();
            w_atk[1].text = Weapon.ToString();
        }
        else if (Weapon > 20 && Weapon <= 30)
        {
            w_text[1].text = "";
            weapon[1].gameObject.SetActive(false);
            weapon[2].gameObject.SetActive(true);
            w_text[2].text = "x" + Moneyarry[W_money] * (Weapon + 1);
            w_atk[0].text = 10.ToString();
            w_atk[1].text = 20.ToString();
            w_atk[2].text = Weapon.ToString();
        }
        else if (Weapon > 30 && Weapon <= 40)
        {
            w_text[2].text = "";
            weapon[2].gameObject.SetActive(false);
            weapon[3].gameObject.SetActive(true);
            w_text[3].text = "x" + Moneyarry[W_money] * (Weapon + 1);
            w_atk[0].text = 10.ToString();
            w_atk[1].text = 20.ToString();
            w_atk[2].text = 30.ToString();
            w_atk[3].text = Weapon.ToString();
        }
        else if (Weapon > 40)
        {
            w_text[3].text = "";
            weapon[3].gameObject.SetActive(false);
            weapon[4].gameObject.SetActive(true);
            w_text[4].text = "x" + Moneyarry[W_money] * (Weapon + 1);
            w_atk[0].text = 10.ToString();
            w_atk[1].text = 20.ToString();
            w_atk[2].text = 30.ToString();
            w_atk[3].text = 40.ToString();
            w_atk[4].text = Weapon.ToString();
        }

        if (Armor >= 0 && Armor <= 10)
        {
            armor[0].gameObject.SetActive(true);
            a_text[0].text = "x" + Moneyarry[A_money] * (Armor + 1);
            a_def[0].text = Armor.ToString();
        }
        else if (Armor > 10 && Armor <= 20)
        {
            a_text[0].text = "";
            armor[0].gameObject.SetActive(false);
            armor[1].gameObject.SetActive(true);
            a_text[1].text = "x" + Moneyarry[A_money] * (Armor + 1);
            a_def[0].text = 10.ToString();
            a_def[1].text = Armor.ToString();
        }
        else if (Armor > 20 && Armor <= 30)
        {
            a_text[1].text = "";
            armor[1].gameObject.SetActive(false);
            armor[2].gameObject.SetActive(true);
            a_text[2].text = "x" + Moneyarry[A_money] * (Armor + 1);
            a_def[0].text = 10.ToString();
            a_def[1].text = 20.ToString();
            a_def[2].text = Armor.ToString();
        }
        else if (Armor > 30 && Armor <= 40)
        {
            a_text[2].text = "";
            armor[2].gameObject.SetActive(false);
            armor[3].gameObject.SetActive(true);
            a_text[3].text = "x" + Moneyarry[A_money] * (Armor + 1);
            a_def[0].text = 10.ToString();
            a_def[1].text = 20.ToString();
            a_def[2].text = 30.ToString();
            a_def[3].text = Armor.ToString();
        }
        else if (Armor > 40)
        {
            a_text[3].text = "";
            armor[3].gameObject.SetActive(false);
            armor[4].gameObject.SetActive(true);
            a_text[4].text = "x" + Moneyarry[A_money] * (Armor + 1);
            a_def[0].text = 10.ToString();
            a_def[1].text = 20.ToString();
            a_def[2].text = 30.ToString();
            a_def[3].text = 40.ToString();
            a_def[4].text = Armor.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
