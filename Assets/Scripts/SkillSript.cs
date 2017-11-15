using UnityEngine;
using System.Collections;
public class SkillSript : MonoBehaviour {

    private static Animator anim;
    private static Transform[] selectbutton;
    private static GameObject selectpanel;

    private static int[] w_upgrade = { 0, 0, 0, 0 };
    private static int[] m_upgrade = { 0, 0, 0, 0 };

    public static float GetUpgrade(int ch,int i)
    {
        if (ch == 1)
        {
            switch (i)
            {
                case 1:
                    return 2 * w_upgrade[0];
                case 2:
                    return 10+5 * w_upgrade[1];
                case 3:
                    return 30 + 3 * w_upgrade[2];
                case 4:
                    return 80 + 1 * w_upgrade[3];
                default:
                    return 0;
            }
        }
        else if (ch == 2)
        {
            switch (i)
            {
                case 1:
                    return  m_upgrade[0];
                case 2:
                    return m_upgrade[1];
                case 3:
                    return m_upgrade[2];
                case 4:
                    return m_upgrade[3];
                default:
                    return 0;
            }
        }
        else
        {
            switch (i)
            {
                case 1:
                    return 2 * w_upgrade[0];
                case 2:
                    return 5 * w_upgrade[1];
                case 3:
                    return 50 + 1f * w_upgrade[2];
                case 4:
                    return 100 + 0.5f * w_upgrade[3];
                default:
                    return 0;
            }
        }
    }

    public static void SetUpgrade(int ch,int i)
    {
        if (ch == 1)
        {
            switch (i)
            {
                case 1:
                    w_upgrade[0] = FileData.GetSkillUpgrade(ch, 1);
                    break;
                case 2:
                    w_upgrade[1] = FileData.GetSkillUpgrade(ch, 2);
                    break;
                case 3:
                    w_upgrade[2] = FileData.GetSkillUpgrade(ch, 3);
                    break;
                case 4:
                    w_upgrade[3] = FileData.GetSkillUpgrade(ch, 4);
                    break;
            }
        }
        else if (ch == 2)
        {
            switch (i)
            {
                case 1:
                    m_upgrade[0] = FileData.GetSkillUpgrade(ch, 1);
                    break;
                case 2:
                    m_upgrade[1] = FileData.GetSkillUpgrade(ch, 2);
                    break;
                case 3:
                    m_upgrade[2] = FileData.GetSkillUpgrade(ch, 3);
                    break;
                case 4:
                    m_upgrade[3] = FileData.GetSkillUpgrade(ch, 4);
                    break;
            }
        }
        else
        {

        }
    }


    public static void TheEyeOfTheWizard(Tile.TileMap tilemap, Tile.TileObj[,] mapArray, int i, int j)
    {
        if (tilemap.isMoving)
            return;
        TouchTest.setTouch(false);
        tilemap.UserSkill(0);
        mapArray[i, j].ShowInfo();

        CloseSelect();
        tilemap.Turnover();
        tilemap.saveArray();
        TouchTest.setTouch(true);
    }

    public static float Warriorpassive()
    {
        float Sheild = 1 + 0.02f * FileData.GetSkillUpgrade(1, 1);
        return Sheild;
    }

    public static void EatPotion(Tile.TileMap tilemap, Tile.TileObj[,] mapArray)
    {
        if (tilemap.isMoving)
            return;
        TouchTest.setTouch(false);
        AudioManager.Instance().playWarriorskil(1);
        anim.Play("warrior_skill01");
        tilemap.UserSkill(1);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (mapArray[i, j].getType() == 1)
                {
                    mapArray[i, j].Heal(10+5*FileData.GetSkillUpgrade(1, 2));
                    mapArray[i, j].ShowHP();
                }
            }
        }
        tilemap.saveArray();
        TouchTest.setTouch(true);
    }

    public static void DropSword(Tile.TileMap tilemap, Tile.TileObj[,] mapArray)
    {
        if (tilemap.isMoving)
            return;
        TouchTest.setTouch(false);
        AudioManager.Instance().playWarriorskil(2);
        anim.Play("warrior_skill02");
        tilemap.UserSkill(2);
        float high = FindHighLevel(mapArray);

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (mapArray[i, j].getType() != 1 && mapArray[i, j].getType() != 0)
                {
                    mapArray[i, j].Damage(high * (30 + 3 * FileData.GetSkillUpgrade(1, 3))/100f);
                    mapArray[i, j].ShowHP();
                    if (mapArray[i, j].isDie())
                    {
                        if (mapArray[i, j].getType() == 3)
                        {
                            mapArray[i, j].HideBosshp();
                            tilemap.KillMonster(mapArray[i, j].getValue(), 3);
                        }
                        else
                            tilemap.KillMonster(mapArray[i, j].getValue(), 2);
                        Destroy(mapArray[i, j].getObj());
                        mapArray[i, j] = new Tile.TileObj();
                        mapArray[i, j].ResetTileObj();
                    }
                }
            }
        }
        tilemap.Turnover();
        tilemap.saveArray();
        TouchTest.setTouch(true);
    }

    public static void OneShot(Tile.TileMap tilemap, Tile.TileObj[,] mapArray, int i, int j)
    {
        if (tilemap.isMoving)
            return;
        TouchTest.setTouch(false);
        AudioManager.Instance().playWarriorskil(3);
        anim.Play("warrior_skill03");
        tilemap.UserSkill(3);
        float high = FindHighLevel(mapArray);

        mapArray[i, j].Damage(high * (80 + 1 * FileData.GetSkillUpgrade(1, 4))/100);
        mapArray[i, j].ShowHP();
        if (mapArray[i, j].isDie())
        {
            if (mapArray[i, j].getType() == 3)
            {
                mapArray[i, j].HideBosshp();
                tilemap.KillMonster(mapArray[i, j].getValue(), 3);
            }
            else
                tilemap.KillMonster(mapArray[i, j].getValue(), 2);
            Destroy(mapArray[i, j].getObj());
            mapArray[i, j] = new Tile.TileObj();
            mapArray[i, j].ResetTileObj();
        }
        tilemap.Turnover();
        tilemap.saveArray();
        CloseSelect();
        TouchTest.setTouch(true);
    }
    


    public static float FindHighLevel(Tile.TileObj[,] mapArray)
    {
        float high = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (mapArray[i, j].getType() == 1)
                {
                    high = Mathf.Max(high, mapArray[i, j].getATK());
                }
            }
        }
        return high;
    }

    public static void OpenSelect(Tile.TileMap tilemap, Tile.TileObj[,] mapArray, int k)//
    {
        if (tilemap.isMoving)
            return;

        selectbutton[1].gameObject.SetActive(true);
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (mapArray[i, j].getType() != 1 && mapArray[i, j].getType() != 0 && k == 1)
                {
                    count++;
                    selectbutton[mapArray[i, j].getpoz(i, j) + 2].gameObject.SetActive(true);
                }
                else if (k == 2 && mapArray[i, j].getType() == 1)
                {
                    count++;
                    selectbutton[mapArray[i, j].getpoz(i, j) + 2].gameObject.SetActive(true);
                }
                else if(mapArray[i,j].getType() == 1 || mapArray[i, j].getType() == 2 || mapArray[i, j].getType() == 3)
                {
                    count++;
                    selectbutton[mapArray[i, j].getpoz(i, j) + 2].gameObject.SetActive(true);
                }
            }
        }

        if (count == 0)
        {
            selectbutton[1].gameObject.SetActive(false);
        }
    }

    public static void CloseSelect()
    {
        for (int i = 1; i <= 17; i++)
        {
            selectbutton[i].gameObject.SetActive(false);
        }
    }

    // Use this for initialization
    void Awake()
    {
        int character = FileData.GetCurrent_Char();
        
        anim = GetComponent<Animator>();
        selectpanel = GameObject.FindWithTag("skill");
        selectbutton = selectpanel.transform.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < 4; i++)
        {
            SetUpgrade(character, i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
