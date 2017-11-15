using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    private int character;
    public Toggle soundon,soundoff;
    public Tile.TileMap tile;
    public GameObject fadepanel;
    public GameObject UI;
    private int[,] pos_sorting = {
        {0,3 }, {0,2 }, {1,3 }, {0,1 },
        {1,2 }, {2,3 }, {0,0 }, {1,1 },
        {2,2 }, {3,3 }, {1,0 }, {2,1 },
        {3,2 }, {2,0 }, {3,1 }, {3,0 }};
    enum CLASS_TYPE
    {
        WARRIOR = 1,
        MAGICIAN = 2,
        ARCHER = 3,
    }

    public void CloseSkill()
    {
        SkillSript.CloseSelect();
        TouchTest.setTouch(true);
    }

    public void UseSelectSkill(int k)
    {
        int i, j;
        
        switch (character)
        {
            case (int)CLASS_TYPE.WARRIOR:
                UI.SetActive(false);
                fadepanel.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
                SkillSript.OneShot(tile, tile.GetTileMap(), pos_sorting[k, 0], pos_sorting[k, 1]);
                StartCoroutine(Fadein());
                break;
            case (int)CLASS_TYPE.MAGICIAN:
                SkillSript.TheEyeOfTheWizard(tile, tile.GetTileMap(), pos_sorting[k, 0], pos_sorting[k, 1]);
                break;
            case (int)CLASS_TYPE.ARCHER:
                break;
        }


    }

    private IEnumerator Fadein()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 10; i >=0; i--)
        {
            fadepanel.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, (float)i / 10);
            yield return new WaitForSeconds(0.001f);
        }
        UI.SetActive(true);
    }

    public void Skill(int i)
    {
        switch (character)
        {
            case (int)CLASS_TYPE.WARRIOR:
                WarriorSkill(i);
                break;
            case (int)CLASS_TYPE.MAGICIAN:
                MagicianSkill(i);
                break;
            case (int)CLASS_TYPE.ARCHER:
                break;
        }
    }

    public void WarriorSkill(int i)
    {
        switch (i)
        {
            case 1:
                if (tile.GetMonsterkill(1) >= 10)
                {
                    SkillSript.EatPotion(tile, tile.GetTileMap());
                }      
                break;
            case 2:
                if (tile.GetMonsterkill(2) >= 15)
                {
                    UI.SetActive(false);
                    fadepanel.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
                    SkillSript.DropSword(tile, tile.GetTileMap());
                    StartCoroutine(Fadein());
                }
                break;
            case 3:
                if (tile.GetMonsterkill(3) >= 30)
                {
                    SkillSript.OpenSelect(tile, tile.GetTileMap(),1);
                }
                break;
        }
          
    }

    public void MagicianSkill(int i)
    {

        switch (i)
        {
            case 0:
                if (tile.GetMonsterkill(0) >= 8)
                {
                    SkillSript.OpenSelect(tile, tile.GetTileMap(), 3);
                }              
                break;
            case 1:
                if (tile.GetMonsterkill(1) >= 10)
                {
                }
                break;
            case 2:
                if (tile.GetMonsterkill(2) >= 15)
                {
                }
                break;
            case 3:
                if (tile.GetMonsterkill(3) >= 30)
                {
                }
                break;
        }

    }

    public void OnTouch()
    {
        TouchTest.setTouch(true);
    }
    public void OffTouch()
    {
        TouchTest.setTouch(false);
    }

    public void SoundOnOff(int i)
    {
        FileData.SetSound(i);
    }

    public void OpenSetting()
    {
        Time.timeScale = 0.0f;
    }
    public void CloseSetting()
    {
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        FileData.SaveData();
        Application.Quit();
    }
	// Use this for initialization
	void Start () {
        character = FileData.GetCurrent_Char();

        if (FileData.GetSound() == 1)
        {
            soundon.isOn = true;
        }
        else
        {
            soundoff.isOn = true;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
