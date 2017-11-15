using UnityEngine;
using System;
using System.Collections;
using System.Threading;

namespace Tile
{
    enum TILE_TYPE
    {
        UNIT = 1,
        ENAMY = 2,
        BOSS = 3,
        OTHERS = 4
    }

    enum CLASS_TYPE
    {
        WARRIOR = 1,
        MAGICIAN = 2,
        ARCHER = 3,
    }

    public class TileObj 
    {
        // 타일 타입
        private TILE_TYPE tile_type;

        int value;
        float maxhp;
        float hp;
        float atk;
        int floor;
        int Class;

        public bool upgradedThisTurn;
        public bool fightedThisTurn;

        private GameObject Bosshp;
        private GameObject Boss;
        private UnityEngine.UI.Text Level;
        private Transform infowindow;
        private Transform info;

        public GameObject obj;
        public bool isMoving = false;
        public AnimationHandler ani_hdr;

        static int[] ATKArray = { 1, 2, 3, 4, 5 };//무기 뎀지 배열
        static int[] DEFArray = { 1, 2, 3, 4, 5 };//방어구 방어 배열

        static public Sprite[] warrior_sprite;

        public int[,] pos_sorting = {
            {6, 3, 1, 0},
            {10, 7, 4, 2 },
            {13, 11, 8, 5 },
            {15, 14, 12, 9 } };

        public GameObject getBoss()
        {
            return Boss;
        }
        public UnityEngine.UI.Text getLevel()
        {
            return Level;
        }
        public GameObject getBosshp()
        {
            return Bosshp;
        }

        public int getpoz(int i, int j)
        {
            return pos_sorting[i, j];
        }
        public void ResetTileObj()
        {
            setValue(0, 1);
            upgradedThisTurn = false;
            fightedThisTurn = false;
        }
        public void ShowBosshp(GameObject boss, UnityEngine.UI.Text level, GameObject bosshp, int v)
        {
            Boss = boss;
            Level = level;
            Bosshp = bosshp;
            boss.SetActive(true);
            level.text = v.ToString();
        }
        public void HideBosshp()
        {
            Boss.SetActive(false);
        }
        public void SetFloor(int F)
        {
            floor = F;
        }

        // 생성자
        public TileObj()
        {

        }

        public void Damage(float damage)
        {
            this.hp -= damage;
            UpdateInfo();
        }
        public void Heal(int i)
        {
            this.hp += this.maxhp * ((float)i / 100);
            if (this.hp >= this.maxhp)
            {
                this.hp = this.maxhp;
            }
            UpdateInfo();
        }
        public void Attack(TileObj enemy)
        {
            this.hp -= enemy.getATK();
            UpdateInfo();
        }
        // 죽었으면 true 살았으면 false
        public bool isDie()
        {
            //return false;
            if (this.hp <= 0)
                return true;
            return false;
        }
        public float getHP()
        {
            return hp;
        }
        public float Damaged()//닳은체력
        {
            return maxhp - hp;
        }
        public float getATK()
        {
            return atk;
        }

        public void setValue(int value, int t)
        {
            switch ((TILE_TYPE)t)
            {
                case TILE_TYPE.UNIT:
                    this.value = value;
                    if (Class == (int)CLASS_TYPE.WARRIOR)
                    {
                        this.maxhp = (float)Math.Pow(2, this.value) * SkillSript.Warriorpassive() + FileData.GetArmor();
                        this.hp = this.maxhp;
                    }
                    else
                    {
                        this.maxhp = (float)Math.Pow(2, this.value) + FileData.GetArmor();
                        this.hp = this.maxhp;
                    }
                    this.atk = (float)Math.Pow(2, this.value) + FileData.GetWeapon();
                    break;
                case TILE_TYPE.ENAMY:
                    this.value = value;
                    this.maxhp = (float)Math.Pow(2, this.value) + floor / 10;
                    this.hp = this.maxhp;
                    this.atk = (float)Math.Pow(2, this.value) + floor / 10;
                    break;
                case TILE_TYPE.BOSS:
                    this.value = value;
                    this.maxhp = this.value * 3f;
                    this.hp = this.value * 3f;
                    this.atk = this.value * 2;
                    break;
            }
            Debug.Log("hp :" + this.hp);
            CloseInfo();
        }
        public int getValue()
        {
            return value;
        }

        public void setObj(GameObject prefab, String name, Transform parent, Vector2 localPosition, TileMap tilemap, int tile_type)
        {
            this.obj = MonoBehaviour.Instantiate(prefab) as GameObject;
            this.obj.name = name;
            this.obj.transform.parent = parent;
            this.obj.transform.localPosition = localPosition;
            this.ani_hdr = obj.transform.GetComponent<AnimationHandler>();
            this.ani_hdr.tilemap = tilemap.GetComponent<TileMap>();
            this.tile_type = (TILE_TYPE)tile_type;
        }
        public GameObject getObj()
        {
            return this.obj;
        }

        public int getType()
        {
            return (int)tile_type;
        }
        // obj 생성해줌
        public void createObj(GameObject parent, int i, int j, TileMap tilemap, int tile_type)
        {
            GameObject prefab = Resources.Load("Prefabs/tile") as GameObject;
            if (tile_type == (int)TILE_TYPE.BOSS)
            {
                prefab = Resources.Load("Prefabs/bosstile") as GameObject;
            }
            this.setObj(
                        prefab,
                        "tile",
                        parent.transform,
                        new Vector2((float)((((float)i * 0.66) - 1.98) + ((float)j * 0.66)), (float)(((-(float)i * 0.33) - 0.51) + ((float)j * 0.33))),
                        tilemap,
                        tile_type
            );

            this.setTileValue(pos_sorting[i, j]);
        }

        public void setTile()
        {

        }
        public void setPosZ(int i, int j)
        {
            Vector3 pos = obj.transform.localPosition;
            pos.z = -pos_sorting[i, j];
            obj.transform.localPosition = pos;
        }
        public void setTileValue(int order)
        {
            TextMesh text = obj.transform.GetComponentInChildren<TextMesh>();
            text.text = value.ToString();
            SpriteRenderer sr = obj.transform.GetComponentInChildren<SpriteRenderer>();
            Class = FileData.GetCurrent_Char();
            switch (tile_type)
            {
                case TILE_TYPE.UNIT:
                    sr.sprite = Assets.Scripts.SpriteManager.unit_sprites[value - 1];
                    sr.color = new Color(0xff, 0xff, 0xff, 0xff);
                    break;
                case TILE_TYPE.ENAMY:
                    sr.sprite = Assets.Scripts.SpriteManager.enemy_sprites[value - 1];
                    sr.color = new Color(0xff, 0xff, 0xff, 0xff);
                    break;
                case TILE_TYPE.BOSS:
                    sr.sprite = Assets.Scripts.SpriteManager.boss_sprites[UnityEngine.Random.Range(0, 2)];
                    // Color hsvcolor = Color.HSVToRGB((float)UnityEngine.Random.Range(0, 360) / (float)360, (float)55 / (float)255, 1);
                    sr.color = Color.white;
                    break;
            }

            Vector3 pos = sr.transform.parent.transform.localPosition;
            pos.z = -order;
            sr.transform.parent.transform.localPosition = pos;
        }
        public void ShowHP()
        {
            if (hp < 0)
            {
                return;
            }
            if (obj != null && tile_type != TILE_TYPE.BOSS)
            {
                Transform tr = obj.transform.FindChild("hpbar");
                Vector3 pos = tr.transform.localPosition;
                pos.x = 0.024f - (1 - (float)hp / maxhp) * 0.07f;
                tr.localScale = new Vector3((float)hp / maxhp, 1, 1);
                tr.localPosition = pos;
            }
            else if (obj != null && tile_type == TILE_TYPE.BOSS)
            {
                Bosshp.GetComponent<UnityEngine.UI.Image>().fillAmount = (float)hp / maxhp;
            }
        }
        public void ShowInfo()
        {
            if (obj != null)
            {
                infowindow = obj.transform.FindChild("infowindow");
                info = obj.transform.FindChild("info");
                if (info == null || infowindow == null)
                    return;
                infowindow.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.5f);
                info.gameObject.GetComponent<TextMesh>().text = "ATK : " + atk + "\nHP : " + hp + "/" + maxhp;
            }
        }
        public void UpdateInfo()
        {
            if (obj == null)
                return;
            info = obj.transform.FindChild("info");
            if (info == null)
                return;
            if (info.gameObject.GetComponent<TextMesh>().text == "")
            {
                return;
            }
            info.gameObject.GetComponent<TextMesh>().text = "ATK : " + atk + "\nHP : " + hp + "/" + maxhp;
        }
        public void CloseInfo()
        {
            if (obj != null)
            {
                infowindow = obj.transform.FindChild("infowindow");
                info = obj.transform.FindChild("info");
                if (info == null || infowindow == null)
                    return;
                infowindow.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
                info.gameObject.GetComponent<TextMesh>().text = "";
            }
        }
    }
}
