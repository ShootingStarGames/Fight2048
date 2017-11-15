using UnityEngine;
using System.Collections;
using System;

namespace Tile
{
    public class AnimationHandler : MonoBehaviour
    {
        private Transform _transform;
        public TileMap tilemap;
        private Animator anim;
        Material sprite_m, white_m;

        public TextMesh damage_t;
        
        private int[,] pos_sorting = {
            {6, 3, 1, 0},
            {10, 7, 4, 2 },
            {13, 11, 8, 5 },
            {15, 14, 12, 9 } };

        // 움직임 애니메이션 시킴
        public void AnimateMove(int i, int j, int x, int y, bool isUpgrade, int type)
        {
            tilemap.animateNum++;
            StartCoroutine(AnimationMove(i, j, x, y, isUpgrade, type));
        }

        // 업글 애니메이션 시킴
        public void AnimateUpgrade(int type, int value, int order)
        {
            tilemap.animateNum++;
            StartCoroutine(AnimationUpgrade(type, value, order));
        }

        // 싸우는 애니메이션
        public void AnimateFightBeforeFight(int i, int j, int x, int y, int type, bool p_isDie, bool e_isDie, GameObject e_obj, int dir)
        {
            tilemap.animateNum++;
            StartCoroutine(AnimationMoveBeforeFight(i, j, x, y, type, p_isDie, e_isDie, e_obj, dir));
        }

        public void AnimateDamageShow(int damage) {
            StartCoroutine(AnimationDamageShow(damage));
        }
        public void AnimateDamage()
        {
            anim.Play("slash_ani");
            StartCoroutine(AnimationDamage());
        }

        // 애니메이션 데미지 표시
        public IEnumerator AnimationDamageShow(int damage) {
            damage_t.text = damage.ToString();
            damage_t.transform.localPosition = new Vector3(0.004f, 0.02f, -1.5f);
            yield return new WaitForSeconds(0.05f);
            for (int i = 0; i < 11; i+=2)
            {
                damage_t.transform.localPosition = new Vector3(0.004f, 0.02f + 0.01f * i, -1.5f);
                damage_t.color = new Vector4(1, 0, 0, 1 - 0.1f * i);
                Debug.Log(1 - 0.1f*i);
                yield return new WaitForSeconds(0.05f);
            }
        }

        public IEnumerator AnimationDamage()
        {
            SpriteRenderer sr = _transform.GetComponentInChildren<SpriteRenderer>();
            sr.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            sr.color = Color.white;
        }
        // dir = 0(왼쪽, DownLeft), 1(위, UpLeft), 2(오른쪽, UpRight), 3(아래, DownRight)
        public IEnumerator AnimationMoveBeforeFight(int i, int j, int x, int y, int type, bool p_isDie, bool e_isDie, GameObject e_obj, int dir)
        {
            Debug.Log("min Log: " + i + " " + j + " " + x + " " + y + " " + type);
            SpriteRenderer sr;
            if (_transform == null)
                sr = transform.GetComponentInChildren<SpriteRenderer>();
            else
                sr = _transform.GetComponentInChildren<SpriteRenderer>();
            anim.Play("slash_ani");
            sr.color = Color.red;
            AudioManager.Instance().playFight();
            while (_transform == null)
                yield return null;



            Vector3 ori_pos = _transform.localPosition;

            float dx = 0.066f * 1f;
            float dy = 0.033f * 1f;

            if (x != 0)
            {
                dy = -dy;
            }

            float abs_xy = Math.Abs(x + y);
            int c;
            for (c = 0; c < 10 * abs_xy; c += 2)
            {
                if (!p_isDie && !e_isDie)
                {
                    if (x > 0)
                        _transform.localPosition = new Vector3(ori_pos.x + ((c + 1) * ((float)((x) + y) / abs_xy * dx)), ori_pos.y + ((c + 1) * (((float)((x) + y) / abs_xy * dy))), -pos_sorting[i + x, j + y]);
                    else if (x < 0)
                        _transform.localPosition = new Vector3(ori_pos.x + ((c + 1) * ((float)((x) + y) / abs_xy * dx)), ori_pos.y + ((c + 1) * (((float)((x) + y) / abs_xy * dy))), -pos_sorting[i + x, j + y]);
                    else if (y > 0)
                        _transform.localPosition = new Vector3(ori_pos.x + ((c + 1) * ((float)(x + (y)) / abs_xy * dx)), ori_pos.y + ((c + 1) * (((float)(x + (y)) / abs_xy * dy))), -pos_sorting[i + x, j + y]);
                    else if (y < 0)
                        _transform.localPosition = new Vector3(ori_pos.x + ((c + 1) * ((float)(x + (y)) / abs_xy * dx)), ori_pos.y + ((c + 1) * (((float)(x + (y)) / abs_xy * dy))), -pos_sorting[i + x, j + y]);
                }
                else
                    _transform.localPosition = new Vector3(ori_pos.x + ((c + 1) * ((float)(x + y) / abs_xy * dx)), ori_pos.y + ((c + 1) * (((float)(x + y) / abs_xy * dy))), -pos_sorting[i + x, j + y]);

                // 레이어 맞춰줌
                if ((c + 1) % 10 == 0)
                {
                 
                    Vector3 pos = this.transform.localPosition;
                    pos.z = -pos_sorting[i + ((x*c)/10), j + ((y*c)/10)];
                    this.transform.localPosition = pos;
                }
                yield return null;  // new WaitForSeconds(0.05f);
            }
            sr.color = Color.white;

            _transform.localPosition = new Vector3((float)((((float)(i + x) * 0.66) - 1.98) + ((float)(j + y) * 0.66)), (float)(((-(float)(i + x) * 0.33) - 0.51) + ((float)(j + y) * 0.33)), -pos_sorting[i + x, j + y]);
            Debug.Log((float)((((float)(i + x) * 0.66) - 1.98) + ((float)(j + y) * 0.66)));// 0.66 *(i+x));
            Debug.Log((float)(((-(float)(i + x) * 0.33) - 0.51) + ((float)(j + y) * 0.33)));
            if (!p_isDie && !e_isDie)
            {
                //                tilemap.GetTileObj(i, j).ShowHP();
                tilemap.GetTileObj(i + x, j + y).ShowHP();

                if (x > 0)
                {
                    tilemap.GetTileObj(i + x + 1, j).ShowHP();
                }
                else if (x < 0)
                {
                    tilemap.GetTileObj(i + x - 1, j).ShowHP();
                }
                else if (y > 0)
                {
                    tilemap.GetTileObj(i, j + y + 1).ShowHP();
                }
                else if (y < 0)
                {
                    tilemap.GetTileObj(i, j + y - 1).ShowHP();
                }else {
                    switch (dir) {
                        case 0: // 왼쪽(DownLeft)
                            tilemap.GetTileObj(i, j-1).ShowHP();
                            break;
                        case 1: // 위(UpLeft)
                            tilemap.GetTileObj(i-1, j).ShowHP();
                            break;
                        case 2: // 오른쪽(UpRight)
                            tilemap.GetTileObj(i, j+1).ShowHP();
                            break;
                        case 3: // 아래(DownRight)
                            tilemap.GetTileObj(i+1, j).ShowHP();
                            break;
                    }
                }
            }
            else
            {
                // 자기 자신이 죽는거라면
                if (p_isDie)
                    Destroy(this.gameObject);
                else
                    tilemap.GetTileObj(i, j).ShowHP();

                if (e_isDie)
                    Destroy(e_obj);
                else
                    tilemap.GetTileObj(i + x, j + y).ShowHP();

                tilemap.GetTileObj(i, j).ShowHP();
                tilemap.GetTileObj(i + x, j + y).ShowHP();
            }

            tilemap.animateNum--;

            if (tilemap.animateNum == 0)
            {
                tilemap.isMoving = false;
                tilemap.setRandomValue(0);
                tilemap.resetUpgradedThisTurn();
            }
        }

        public IEnumerator AnimationMove(int i, int j, int x, int y, bool isUpgrade, int type)
        {
            //SpriteRenderer sr = _transform.GetComponentInChildren<SpriteRenderer>();
            while (_transform == null)
                yield return null;

            Vector3 ori_pos = _transform.localPosition;

            float dx = 0.066f * 1f;
            float dy = 0.033f * 1f;

            if (x != 0)
            {
                dy = -dy;
            }

            float abs_xy = Math.Abs(x + y);
            
            for (int c = 0; c < 10 * abs_xy; c += 2)
            {
                _transform.localPosition = new Vector3(ori_pos.x + ((c + 1) * ((float)(x + y) / abs_xy * dx)), ori_pos.y + ((c + 1) * (((float)(x + y) / abs_xy * dy))), -pos_sorting[i + x, j + y]);

                // 레이어 맞춰줌
                if ((c + 1) % 10 == 0)
                {
                    Vector3 pos = this.transform.localPosition;
                    pos.z = -pos_sorting[i + x, j + y];
                    this.transform.localPosition = pos;
                }
                yield return null;  // new WaitForSeconds(0.05f);
            }

            _transform.localPosition = new Vector3((float)((((float)(i+x) * 0.66) - 1.98) + ((float)(j+y) * 0.66)), (float)(((-(float)(i + x) * 0.33) - 0.51) + ((float)(j + y) * 0.33)), -pos_sorting[i + x, j + y]);
            Debug.Log((float)((((float)(i+x) * 0.66) - 1.98) + ((float)(j+y) * 0.66)));// 0.66 *(i+x));
            Debug.Log((float)(((-(float)(i+x) * 0.33) - 0.51) + ((float)(j+y) * 0.33)));
            

            if (isUpgrade)
            {
                // 업그레이드 되는거면 오브젝트 삭제를 해줘용
                Destroy(this.gameObject);
                if (tilemap.GetTileObj(i + x, j + y).ani_hdr != null)
                {
                    // 2016/09/19 타입 전달 변경/타입을 받아서 하는 것이 아니라 동적으로 받아옴!
                    tilemap.GetTileObj(i + x, j + y).ani_hdr.AnimateUpgrade(tilemap.GetTileObj(i + x, j + y).getType(), tilemap.GetTileObj(i + x, j + y).getValue(), pos_sorting[i + x, j + y]);
                    tilemap.GetTileObj(i + x, j + y).ShowHP();
                }
       
            }

            tilemap.animateNum--;

            if (tilemap.animateNum == 0)
            {
                tilemap.isMoving = false;
                tilemap.setRandomValue(0);
                tilemap.resetUpgradedThisTurn();
            }
        }

        public IEnumerator AnimationUpgrade(int type, int value, int order)
        {
            anim.Play("upgrade_ani");
            AudioManager.Instance().playUpgrade();
            SpriteRenderer sr = _transform.GetComponentInChildren<SpriteRenderer>();


            while (_transform == null)
                yield return null;

            this.setTileValue(type, value, order);

            tilemap.animateNum--;
            yield return new WaitForSeconds(0.2f);
            sr.material = sprite_m;
            if (tilemap.animateNum == 0)
            {
                tilemap.isMoving = false;
                tilemap.setRandomValue(0);
                tilemap.resetUpgradedThisTurn();
            }
        }

        public void setTileValue(int type, int value, int order)
        {
            TextMesh text = _transform.GetComponentInChildren<TextMesh>();
            text.text = value.ToString();

            SpriteRenderer sr = _transform.GetComponentInChildren<SpriteRenderer>();
            sr.material = white_m;
            // 색깔 맞춰줌

            sr.color = new Color(0xff, 0xff, 0xff, 0xff);
            
            switch ((Tile.TILE_TYPE)type)
            {
                case Tile.TILE_TYPE.UNIT:
                    sr.sprite = Assets.Scripts.SpriteManager.unit_sprites[value - 1];
                    break;
                case Tile.TILE_TYPE.ENAMY:
                    sr.sprite = Assets.Scripts.SpriteManager.enemy_sprites[value - 1];
                    break;
                case Tile.TILE_TYPE.BOSS:
                    value = UnityEngine.Random.Range(0, 2);
                    sr.sprite = Assets.Scripts.SpriteManager.boss_sprites[value];
                    break;
            }
            Vector3 pos = this.transform.localPosition;
            pos.z = -order;
            this.transform.localPosition = pos;
      
        }
        
        // Use this for initialization
        void Start()
        {
            _transform = transform;
            anim = GetComponentInChildren<Animator>();
            sprite_m = Resources.Load("Material/Sprite", typeof(Material)) as Material;
            white_m = Resources.Load("Material/White", typeof(Material)) as Material;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}