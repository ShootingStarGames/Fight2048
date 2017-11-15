using UnityEngine;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Tile
{
    public class TileMap : MonoBehaviour
    {
        public GameObject revivepanel;
        public Text reward;

        public GameObject boss;
        public GameObject bosshp;
        public Text bosslv;

        public GameObject clearpanel;
        public Text cleartext;
        private Vector2 clearvector = Vector2.zero;

        public GameObject restart;
        public GameObject score_obj, highscore_obj;
        public Text Floortext;
        public Slider Killgage;
        public Text gold_t, dia_t;
        private bool spawnboss;
        private int gold, dia;
        private int score;
        private int floor;
        private int monster_kill = 0;
        private int mons_count = 10;
        private int reward_dia;
        // 외부에서 받아올 타일 부모 오브젝트입니다.
        public GameObject tiles;
        // 맵 넣는 배열
        private TileObj[,] mapArray = new TileObj[4, 4];
        private Assets.Scripts.SpriteManager sm;
        public bool isMoving = false;
        public int animateNum = 0;

        private int character = 1;
        Material sprite_m, white_m;
        public Image Skill_icon0, Skill_icon1, Skill_icon2, Skill_icon3;
        private SkillSript Skill = new SkillSript();
        private int[] Skill_arr = new int[4] { 0, 0, 0, 0 };
        Text score_t, highscore_t;

        bool isUpgrade = false;
        bool isFight = false;

        bool[] skill_on = new bool[4];

        public AdmobManager admobManager;

        private int Interaction_state(int my_i, int my_j, int your_i, int your_j, bool flag) //my 내꺼 your 니꺼 
        {

            int cnt = 0;

            //업그레이드 되는 블록인지 체크
            if ((flag) && (mapArray[my_i, my_j].getValue() == mapArray[your_i, your_j].getValue()) && !mapArray[your_i, your_j].upgradedThisTurn && (mapArray[my_i, my_j].getType() == mapArray[your_i, your_j].getType()))
            {
                cnt++;

                // upgrade_queue.Enqueue(ut);
                // 해당 업그레이드 되는 타일을 이미 업그레이드가 되었다고 표시해준다.
                mapArray[your_i, your_j].upgradedThisTurn = true;
                mapArray[your_i, your_j].setValue(mapArray[my_i, my_j].getValue() + 1, mapArray[my_i, my_j].getType());
                isUpgrade = true;
                //레벨 확인.
                FileData.SetMaxLev((mapArray[my_i, my_j].getValue() + 1));
            }
            // 싸워야 하는 블록인지 체크
            if ((flag) && !mapArray[your_i, your_j].fightedThisTurn && !mapArray[your_i, your_j].upgradedThisTurn && (mapArray[my_i, my_j].getType() != mapArray[your_i, your_j].getType()))
            {
                //if( (mapArray[i, j].getHP() <= mapArray[i, j-cnt-1].getATK()))
                //if (mapArray[i, j].isDie() && !mapArray[i, j - cnt - 1].isDie())
                {
                    if (mapArray[your_i, your_j].getType() == 1 || mapArray[my_i, my_j].getType() == 1)
                    {
                        // 해당 파이트 되는 타일을 이미 파이트되었다고 표시해준다.
                        mapArray[my_i, my_j].fightedThisTurn = true;
                        mapArray[your_i, your_j].fightedThisTurn = true;

                        // 상대방의 ATK만큼 자신의 체력을 깎자!...
                        isFight = true;
                        mapArray[my_i, my_j].Attack(mapArray[your_i, your_j]);
                        mapArray[your_i, your_j].Attack(mapArray[my_i, my_j]);

                        cnt++;
                    }
                }
            }
            return cnt;
        }

        private void Interaction_move(int my_i, int my_j, int your_i, int your_j, int not_dead_i, int not_dead_j, int x, int y, int alive_x, int alive_y, int cnt, int dir)
        {
            // 그냥 이동만 하는거라면?! 바꿔줍시당
            if ((!isUpgrade) && (!isFight))
            {
                mapArray[your_i, your_j] = mapArray[my_i, my_j];
                mapArray[your_i, your_j].setPosZ(your_i, your_j);
                mapArray[my_i, my_j] = new TileObj();
                mapArray[my_i, my_j].ResetTileObj();
                mapArray[your_i, your_j].ani_hdr.AnimateMove(my_i, my_j, x, y, isUpgrade, mapArray[your_i, your_j].getType());
            }
            else if (isUpgrade)
            {
                mapArray[my_i, my_j].ani_hdr.AnimateMove(my_i, my_j, x, y, isUpgrade, mapArray[my_i, my_j].getType());
                mapArray[my_i, my_j] = new TileObj();
                mapArray[my_i, my_j].ResetTileObj();
            }
            else if (isFight)
            {
                mapArray[your_i, your_j].ani_hdr.AnimateDamage();

                if (!mapArray[my_i, my_j].isDie())
                    mapArray[my_i, my_j].ani_hdr.AnimateDamageShow((int)mapArray[your_i, your_j].getATK());
                if (!mapArray[your_i, your_j].isDie())
                    mapArray[your_i, your_j].ani_hdr.AnimateDamageShow((int)mapArray[my_i, my_j].getATK());

                // 만약 나만 죽는거라면? cnt 전까지만 움직이구 죽어줘야댐
                Debug.Log(mapArray[my_i, my_j].isDie().ToString() + mapArray[your_i, your_j].isDie().ToString());
                if (mapArray[my_i, my_j].isDie() && !mapArray[your_i, your_j].isDie())
                {
                    if (mapArray[my_i, my_j].getType() != 1)
                    {
                        if (mapArray[my_i, my_j].getType() == 3)
                        {

                            KillMonster(mapArray[my_i, my_j].getValue(), 3);
                            mapArray[my_i, my_j].HideBosshp();
                        }
                        else
                            KillMonster(mapArray[my_i, my_j].getValue(), 2);
                    }
                    Debug.Log("나만 주겅");
                    // 이동 애니메이션합시다..
                    mapArray[my_i, my_j].ani_hdr.AnimateFightBeforeFight(my_i, my_j, x, y, mapArray[my_i, my_j].getType(), mapArray[my_i, my_j].isDie(), mapArray[your_i, your_j].isDie(), mapArray[your_i, your_j].getObj(), dir);
                    mapArray[my_i, my_j] = new TileObj();
                    mapArray[my_i, my_j].ResetTileObj();
                }

                // 만약 둘다 죽는거라면? 역시 cnt 전까지만 움직이구 죽어줘야댐..!
                else if (mapArray[my_i, my_j].isDie() && mapArray[your_i, your_j].isDie())
                {
                    if (mapArray[my_i, my_j].getType() == 1)
                    {
                        if (mapArray[your_i, your_j].getType() == 3)
                        {

                            KillMonster(mapArray[your_i, your_j].getValue(), 3);
                            mapArray[your_i, your_j].HideBosshp();
                        }
                        else
                            KillMonster(mapArray[your_i, your_j].getValue(), 2);
                    }
                    else
                    {
                        if (mapArray[my_i, my_j].getType() == 3)
                        {

                            KillMonster(mapArray[my_i, my_j].getValue(), 3);
                            mapArray[my_i, my_j].HideBosshp();
                        }
                        else
                            KillMonster(mapArray[my_i, my_j].getValue(), 2);
                    }

                    mapArray[my_i, my_j].ani_hdr.AnimateFightBeforeFight(my_i, my_j, x, y, mapArray[my_i, my_j].getType(), mapArray[my_i, my_j].isDie(), mapArray[your_i, your_j].isDie(), mapArray[your_i, your_j].getObj(), dir);
                    mapArray[your_i, your_j] = new TileObj();
                    mapArray[your_i, your_j].ResetTileObj();
                    mapArray[my_i, my_j] = new TileObj();
                    mapArray[my_i, my_j].ResetTileObj();
                }

                // 만약 적만 죽는거라면? cnt까지 움직여주자..!
                else if (!mapArray[my_i, my_j].isDie() && mapArray[your_i, your_j].isDie())
                {
                    if (mapArray[your_i, your_j].getType() != 1)
                    {
                        if (mapArray[your_i, your_j].getType() == 3)
                        {

                            KillMonster(mapArray[your_i, your_j].getValue(), 3);
                            mapArray[your_i, your_j].HideBosshp();
                        }
                        else
                            KillMonster(mapArray[your_i, your_j].getValue(), 2);
                    }

                    mapArray[my_i, my_j].ani_hdr.AnimateFightBeforeFight(my_i, my_j, x, y, mapArray[my_i, my_j].getType(), mapArray[my_i, my_j].isDie(), mapArray[your_i, your_j].isDie(), mapArray[your_i, your_j].getObj(), dir);
                    mapArray[your_i, your_j] = mapArray[my_i, my_j];
                    mapArray[your_i, your_j].setPosZ(your_i, your_j);
                    mapArray[my_i, my_j] = new TileObj();
                    mapArray[my_i, my_j].ResetTileObj();
                }

                // 만약 적과 나 둘다 안죽는다면? cnt 전까지만 움직여줘야댐
                else if (!mapArray[my_i, my_j].isDie() && !mapArray[your_i, your_j].isDie())
                {
                    // 만약 움직일일이 없다면?? 초기화 시켜주면 안댐!!
                    if (cnt <= 1)
                    {
                        mapArray[my_i, my_j].ani_hdr.AnimateFightBeforeFight(my_i, my_j, alive_x, alive_y, mapArray[my_i, my_j].getType(), mapArray[my_i, my_j].isDie(), mapArray[your_i, your_j].isDie(), mapArray[your_i, your_j].getObj(), dir);
                    }
                    else
                    {
                        mapArray[my_i, my_j].ani_hdr.AnimateFightBeforeFight(my_i, my_j, alive_x, alive_y, mapArray[my_i, my_j].getType(), mapArray[my_i, my_j].isDie(), mapArray[your_i, your_j].isDie(), mapArray[your_i, your_j].getObj(), dir);
                        mapArray[not_dead_i, not_dead_j] = mapArray[my_i, my_j];
                        mapArray[not_dead_i, not_dead_j].setPosZ(not_dead_i, not_dead_j);
                        mapArray[my_i, my_j] = new TileObj();
                        mapArray[my_i, my_j].ResetTileObj();
                    }
                }
            }
        } //my 내꺼 your 니꺼 not_dead 안죽었을때, xy 죽었을때 움직일거리,alivexy 안죽었을때 움직일거리,dir 방향

        private void OpenRevive()
        {
            new AdmobManager().ShowInterstitialAdFunc();
            revivepanel.SetActive(true);
            reward_dia = 0;
            if (floor >= 100)
                reward_dia = (floor - 100) / 10;

            reward.text = "현재 층수 : " + floor + "\n보상 다이아 : " + reward_dia;
        }

        public void saveArray()
        {
            string array_damage = "";
            string array_value = "";
            string array_type = "";
            string skill_remain = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    array_damage += mapArray[i, j].Damaged().ToString() + " ";
                    array_value += mapArray[i, j].getValue().ToString() + " ";
                    array_type += mapArray[i, j].getType().ToString() + " ";
                }
            }
            skill_remain += Skill_arr[0].ToString() + " " + Skill_arr[1].ToString() + " " + Skill_arr[2].ToString() + " " + Skill_arr[3].ToString();
            FileData.SetArraydamage(array_damage);
            FileData.SetArraytype(array_type);
            FileData.SetArrayvalue(array_value);
            FileData.SetSkillremain(skill_remain);
            FileData.Setmonsterkill(monster_kill);

        }

        public int getSkillgage(int i)
        {
            return Skill_arr[i - 1];
        }

        private void OnSkill(int i)
        {
            switch (i)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }

        private void SetCoolTime()
        {
            Skill_icon0.fillAmount = (float)Skill_arr[0] / 8;
            Skill_icon1.fillAmount = (float)Skill_arr[1] / 10;
            Skill_icon2.fillAmount = (float)Skill_arr[2] / 15;
            Skill_icon3.fillAmount = (float)Skill_arr[3] / 30;
            if (Skill_icon0.fillAmount == 1 && skill_on[0])
            {
                skill_on[0] = false;
                StartCoroutine(SkillOn(0));
            }
            if (Skill_icon1.fillAmount == 1 && skill_on[1])
            {
                skill_on[1] = false;
                StartCoroutine(SkillOn(1));
            }
            if (Skill_icon2.fillAmount == 1 && skill_on[2])
            {
                skill_on[2] = false;
                StartCoroutine(SkillOn(2));
            }
            if (Skill_icon3.fillAmount == 1 && skill_on[3])
            {
                skill_on[3] = false;
                StartCoroutine(SkillOn(3));
            }
        }

        IEnumerator SkillOn(int i)
        {
            switch (i)
            {
                case 0:
                    Skill_icon0.material = white_m;
                    break;
                case 1:
                    Skill_icon1.material = white_m;
                    break;
                case 2:
                    Skill_icon2.material = white_m;
                    break;
                case 3:
                    Skill_icon3.material = white_m;
                    break;
            }
            yield return new WaitForSeconds(0.3f);

            switch (i)
            {
                case 0:
                    Skill_icon0.material = sprite_m;
                    break;
                case 1:
                    Skill_icon1.material = sprite_m;
                    break;
                case 2:
                    Skill_icon2.material = sprite_m;
                    break;
                case 3:
                    Skill_icon3.material = sprite_m;
                    break;
            }
        }

        public void UserSkill(int i)
        {
            Skill_arr[i] = 0;
            SetCoolTime();
            skill_on[i] = true;
        }

        public void Turnover()
        {
            FileData.SetGold(gold);
            FileData.SetScore(score);
            SetCoolTime();
            SetScore();
            gold_t.text = gold.ToString();

            if (spawnboss)
            {
                monster_kill = 0;
            }
            if (monster_kill >= mons_count)
            {
                Upfloor();
            }
            Killgage.value = monster_kill;
        }

        private void SetScore()
        {
            score_t.text = "SCORE : " + score.ToString();
            if (FileData.GetHighScore() < score)
            {
                FileData.SetHighScore(score);
                CGoogleplayGameServiceManager.SumbitScore(score);
            }
            highscore_t.text = "HIGHSCORE : " + FileData.GetHighScore();
        }

        public int GetMonsterkill(int i)
        {
            return Skill_arr[i];
        }

        public int GetFloor()
        {
            return floor;
        }

        public TileObj[,] GetTileMap()
        {
            return mapArray;
        }

        public TileObj GetTileObj(int i, int j)
        {
            return mapArray[i, j];
        }
        //몬스터 킬
        public void KillMonster(int value, int s)
        {
            FileData.SetMaxKill();
            Skill_arr[0]++;
            Skill_arr[1]++;
            Skill_arr[2]++;
            Skill_arr[3]++;
            monster_kill++;
            if (s == 3)
            {
                //보스 잡았을 때
                spawnboss = false;
                value = value * 10;
                Upfloor();
                SoundScript.MapMusic();
                AudioManager.Instance().playClear();
            }
            gold += value;
            score += value;
            //dia_t.text = dia.ToString();

            AudioManager.Instance().playCoin();
        }
        // 게임 오버 확인
        private bool CheckGameOver()
        {
            int playernum = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (mapArray[i, j].getType() == 1)
                    {
                        playernum++;
                    }
                }
            }
            if (playernum <= 0 || playernum >= 16)
            {
                StopCoroutine(ShowClear());
                StopCoroutine(ShowBoss());
                return true;
            }
            return false;
        }
        // mapArray 리셋 해줌
        // reset MapArray
        public void resetMapArray()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mapArray[i, j] = new TileObj();
                    mapArray[i, j].ResetTileObj();
                }
            }
        }

        public void resetUpgradedThisTurn()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mapArray[i, j].upgradedThisTurn = false;
                    mapArray[i, j].fightedThisTurn = false;
                }
            }
        }
        // 처음에 랜덤값 세팅해줍니당
        // mapArray 랜덤으로 띄워주는 함수

        private void LoadValue()
        {

            if (FileData.GetFirstGame() == 1 || FileData.GetFirstGame() == 3)
            {
                if (character == 1)
                    Skill_arr[0] = 8;
                setRandomValue(1);
                FileData.SetFirstGame(2);
                return;
            }
            string[] type = FileData.GetArraytype().Split(' ');
            string[] value = FileData.GetArrayvalue().Split(' ');
            string[] damage = FileData.GetArraydamage().Split(' ');
            string[] skill = FileData.GetSkillremain().Split(' ');
            int[] t = new int[16];
            int[] v = new int[16];
            float[] d = new float[16];
            Skill_arr[0] = System.Int32.Parse(skill[0]);
            Skill_arr[1] = System.Int32.Parse(skill[1]);
            Skill_arr[2] = System.Int32.Parse(skill[2]);
            Skill_arr[3] = System.Int32.Parse(skill[3]);
            if (character == 1)
                Skill_arr[0] = 8;
            monster_kill = FileData.Getmonsterkill();
            Killgage.value = monster_kill;
            int i, j;
            int count = 0;
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    t[i * 4 + j] = System.Int32.Parse(type[i * 4 + j]);
                    v[i * 4 + j] = System.Int32.Parse(value[i * 4 + j]);
                    d[i * 4 + j] = float.Parse(damage[i * 4 + j]);
                    if (t[i * 4 + j] == 1)
                        count++;
                }
            }

            SoundScript.MapMusic();
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (v[i * 4 + j] == 0 || t[i * 4 + j] == 0)
                        continue;
                    if (t[i * 4 + j] == 3)
                    {
                        mapArray[i, j].ShowBosshp(boss, bosslv, bosshp, v[i * 4 + j]);
                        spawnboss = true;
                        SoundScript.BossMusic();
                    }
                    mapArray[i, j].SetFloor(FileData.GetFloor());
                    mapArray[i, j].setValue(v[i * 4 + j], t[i * 4 + j]);
                    mapArray[i, j].createObj(tiles, i, j, this, t[i * 4 + j]);
                    mapArray[i, j].setTileValue(mapArray[i, j].pos_sorting[i, j]);
                    mapArray[i, j].Damage(d[i * 4 + j]);
                    mapArray[i, j].ShowHP();

                }
            }
            SetCoolTime();
        }

        public void setRandomValue(int f)
        {

            // 처음에 몇 개를 위에 띄울까?..
            int cnt = 0;

            // 하지만 먼저 랜덤으로 띄울 애들이 1개밖에 없다면... 한개만 띄워줘야겠졍?
            for (int i = 0; i < 16; i++)
            {
                if (mapArray[i / 4, i % 4].getValue() == 0)
                {
                    cnt++;
                    if (cnt > 1) break;
                }
            }

            if (cnt > 1)
            {
                cnt = (int)Random.Range(1, 2);
            }
            else if (cnt == 0)
            {
                // 일어날일이 없음!.. 하지만 혹시 모르니 넣어두는 코드임
                Debug.Log("랜덤값 넣을 공간이 없네염;;");
                return;
            }
            else if (cnt == 1)
            {
                cnt = 1;
            }

            for (int i = 0; i < cnt; i++)
            {
                // 어디 부분에 넣을까요?
                int r = (int)Random.Range(0, 16);

                // 만약 이 곳의 값이 0이 아니라면 다시 랜덤값을 돌려야합니다.
                if (mapArray[r / 4, r % 4].getValue() != 0)
                {
                    i--;
                }

                // 이 곳의 값이 0이라면 랜덤 값을 넣어줘야겠죠?
                else
                {
                    // 1 또는 2를 넣어주면 돼요!
                    int v = Random.Range(0, 5); //v는 레벨
                    v = (v == 0) ? 2 : 1;
                    int t = Random.Range(1, 6); //t는 타입

                    //처음 소환했을때
                    if (t >= 3)
                    {
                        t = 2;
                        if (floor >= 30)//올라갈수록 몬스터 레벨 상승.
                        {
                            v = Random.Range(0, 5);
                            v = (v == 0) ? 3 : 2;
                        }
                    }// t >=3 일때  60% 확률로 enemy
                    else
                    {
                        t = 1;
                    }// 40% 확률로 unit

                    if (f == 1)//처음 시작할때
                    {
                        v = 2;
                        t = 1;
                    }
                    if (floor % 10 == 0 && spawnboss == false)
                    {
                        v = floor;
                        t = 3;
                    }// 10층일때 마다 보스 한번 소환

                    mapArray[r / 4, r % 4].SetFloor(floor);
                    mapArray[r / 4, r % 4].setValue(v, t);
                    mapArray[r / 4, r % 4].createObj(tiles, r / 4, r % 4, this, t);
                    mapArray[r / 4, r % 4].setTileValue(mapArray[r / 4, r % 4].pos_sorting[r / 4, r % 4]);

                    if (floor % 10 == 0 && spawnboss == false)
                    {
                        bosshp.GetComponent<Image>().fillAmount = 1f;
                        mapArray[r / 4, r % 4].ShowBosshp(boss, bosslv, bosshp, v);
                        spawnboss = true;
                        SoundScript.BossMusic();

                    }//보스 소환시 보스체력바 소환및 보스 체력 셋팅

                }
            }
            saveArray();

            if (CheckGameOver())
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (mapArray[i, j].getType() == 3)
                        {
                            mapArray[i, j].HideBosshp();
                        }
                    }
                }
                OpenRevive();
                TouchTest.setTouch(false);
            }
        }

        // 왼쪽으로 갑니당
        public void DownLeft()
        {
            animateNum = 0;
            isMoving = true;
            bool moved = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    isUpgrade = false;
                    isFight = false;

                    if (mapArray[i, j].getValue() == 0)
                        continue;

                    int cnt = 0;
                    for (int k = j - 1; k > -1; k--)
                    {
                        if (mapArray[i, k].getValue() == 0)
                        {
                            cnt++;
                        }
                        else { break; }
                    }

                    cnt += Interaction_state(i, j, i, j - cnt - 1, (j - cnt - 1 >= 0)); // 업그레이드, 싸움 체크

                    // 이동해야하는 오브젝트들을 큐에 넣어준다.
                    // movequeue.Enqueue(어떤 블록 i, j, 얼마나 이동? x, y)
                    if (cnt > 0)
                    {
                        moved = true;

                        Interaction_move(i, j, i, j - cnt, i, j - cnt + 1, 0, -cnt, 0, -cnt + 1, cnt, 0);//원래 위치ij,옮길 위치ij,아무도 안죽었을때 ij,무빙 xy,아무도 안죽었을때 xy,cnt

                    }
                }
            }
            if (moved)
            {
                AudioManager.Instance().playMove();

                if (animateNum == 0)
                {
                    isMoving = false;
                    setRandomValue(0);
                    resetUpgradedThisTurn();
                }
            }
            else
            {
                isMoving = false;
            }
            Turnover();
        }
        // 오른쪽으로 갑니당
        public void UpRight()
        {
            animateNum = 0;
            isMoving = true;
            bool moved = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j > -1; j--)
                {
                    isUpgrade = false;
                    isFight = false;

                    if (mapArray[i, j].getValue() == 0)
                        continue;

                    int cnt = 0;
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (mapArray[i, k].getValue() == 0)
                        {
                            cnt++;
                        }
                        else { break; }
                    }
                    cnt += Interaction_state(i, j, i, j + cnt + 1, (j + cnt + 1 < 4)); // 업그레이드, 싸움 체크

                    // 이동해야하는 오브젝트들을 큐에 넣어준다.
                    // movequeue.Enqueue(어떤 블록 i, j, 얼마나 이동? x, y)
                    if (cnt > 0)
                    {
                        moved = true;

                        Interaction_move(i, j, i, j + cnt, i, j + cnt - 1, 0, cnt, 0, cnt - 1, cnt, 2);//원래 위치ij,옮길 위치ij,아무도 안죽었을때 ij,무빙 xy,아무도 안죽었을때 xy,cnt

                    }
                }
            }
            if (moved)
            {
                AudioManager.Instance().playMove();

                if (animateNum == 0)
                {
                    isMoving = false;
                    setRandomValue(0);
                    resetUpgradedThisTurn();
                }
            }
            else
            {
                isMoving = false;
            }
            Turnover();
        }
        // 위
        public void UpLeft()
        {
            animateNum = 0;
            bool moved = false;
            isMoving = true;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 1; i < 4; i++)
                {
                    isUpgrade = false;
                    isFight = false;

                    if (mapArray[i, j].getValue() == 0)
                        continue;

                    int cnt = 0;
                    for (int k = i - 1; k > -1; k--)
                    {
                        if (mapArray[k, j].getValue() == 0)
                        {
                            cnt++;
                        }
                        else { break; }
                    }

                    cnt += Interaction_state(i, j, i - cnt - 1, j, (i - cnt - 1 >= 0)); // 업그레이드, 싸움 체크

                    // 이동해야하는 오브젝트들을 큐에 넣어준다.
                    // movequeue.Enqueue(어떤 블록 i, j, 얼마나 이동? x, y)
                    if (cnt > 0)
                    {
                        moved = true;

                        Interaction_move(i, j, i - cnt, j, i - cnt + 1, j, -cnt, 0, -cnt + 1, 0, cnt, 1);//원래 위치ij,옮길 위치ij,아무도 안죽었을때 ij,무빙 xy,아무도 안죽었을때 xy,cnt

                    }
                }
            }
            if (moved)
            {
                AudioManager.Instance().playMove();

                if (animateNum == 0)
                {
                    isMoving = false;
                    setRandomValue(0);
                    resetUpgradedThisTurn();
                }
            }
            else
            {
                isMoving = false;
            }
            Turnover();
        }

        public void DownRight()
        {
            animateNum = 0;
            isMoving = true;
            bool moved = false;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 2; i > -1; i--)
                {
                    isUpgrade = false;
                    isFight = false;

                    if (mapArray[i, j].getValue() == 0)
                        continue;

                    int cnt = 0;
                    for (int k = i + 1; k < 4; k++)
                    {
                        if (mapArray[k, j].getValue() == 0)
                        {
                            cnt++;
                        }
                        else { break; }
                    }

                    cnt += Interaction_state(i, j, i + cnt + 1, j, (i + cnt + 1 <= 3)); // 업그레이드, 싸움 체크

                    if (cnt > 0)
                    {
                        moved = true;

                        Interaction_move(i, j, i + cnt, j, i + cnt - 1, j, cnt, 0, cnt - 1, 0, cnt, 3);//원래 위치ij,옮길 위치ij,아무도 안죽었을때 ij,무빙 xy,아무도 안죽었을때 xy,cnt

                    }
                }
            }

            if (moved)
            {
                AudioManager.Instance().playMove();

                if (animateNum == 0)
                {
                    isMoving = false;
                    setRandomValue(0);
                    resetUpgradedThisTurn();
                }
            }
            else
            {
                isMoving = false;
            }
            Turnover();
        }

        public void RestartGame()
        {
            foreach (Transform t in tiles.GetComponent<Transform>())
            {
                Destroy(t.gameObject);
            }
            TouchTest.setTouch(true);

            resetMapArray();
            FileData.SetScore(0);
            FileData.SetHighScore((int)Mathf.Max(FileData.GetHighScore(), score));
            FileData.SetGold(0);
            FileData.Setmonsterkill(0);
            FileData.SetFloor(1);
            FileData.Setweapon(0);
            FileData.SetArmor(0);
            setRandomValue(1);
            spawnboss = false;
            Skill_arr[0] = 0;
            Skill_arr[1] = 0;
            Skill_arr[2] = 0;
            Skill_arr[3] = 0;
            SetCoolTime();
            saveArray();
            FileData.SetFirstGame(3);
            FileData.SetDiamond(reward_dia);
            SceneManager.LoadScene("select");
        }

        // Use this for initialization
        void Start()
        {
            character = FileData.GetCurrent_Char();
            spawnboss = false;
            score_t = score_obj.GetComponent<Text>();
            highscore_t = highscore_obj.GetComponent<Text>();
            sprite_m = Resources.Load("Material/Sprite", typeof(Material)) as Material;
            white_m = Resources.Load("Material/White", typeof(Material)) as Material;
            sm = new Assets.Scripts.SpriteManager((int)Assets.Scripts.UNIT_TYPE.WARRIOR, (int)Assets.Scripts.ENEMY_TYPE.GOBLIN);
            resetMapArray();
            score = FileData.GetScore();
            gold = FileData.GetGold();
            dia = FileData.GetDiamond();
            floor = FileData.GetFloor();
            Killgage.value = monster_kill;
            //PlayerPrefs.DeleteAll();
            //맵 생성 처음.
            LoadValue();
            SetCoolTime();
            score_t.text = "SCORE : " + score.ToString();
            highscore_t.text = "HIGHSCORE : " + FileData.GetHighScore();
            Floortext.text = floor.ToString();
            gold_t.text = gold.ToString();
            dia_t.text = dia.ToString();
            reward_dia = 0;
        }
        private void Upfloor()
        {
            monster_kill = 0;
            floor++;
            FileData.SetFloor(floor);
            FileData.SetMaxFloor(floor);
            Floortext.text = floor.ToString();
            Killgage.value = monster_kill;
            if (floor % 10 == 0)
                StartCoroutine(ShowBoss());
            else
                StartCoroutine(ShowClear());

        }
        private IEnumerator ShowClear()
        {
            cleartext.GetComponent<Transform>().localPosition = new Vector2(-460, 0);
            clearpanel.SetActive(true);
            cleartext.text = "CLEAR";
            cleartext.color = Color.white;
            for (int i = 0; i < 460; i += 20)
            {
                cleartext.GetComponent<Transform>().localPosition = new Vector2(-460 + i, 0);
                yield return new WaitForSeconds(0.005f);
            }
            yield return new WaitForSeconds(2f);
            clearpanel.SetActive(false);
        }
        private IEnumerator ShowBoss()
        {
            cleartext.GetComponent<Transform>().localPosition = new Vector2(-460, 0);
            clearpanel.SetActive(true);
            cleartext.text = "BOSS";
            cleartext.color = Color.red;
            for (int i = 0; i < 460; i += 20)
            {
                cleartext.GetComponent<Transform>().localPosition = new Vector2(-460 + i, 0);
                yield return new WaitForSeconds(0.005f);
            }
            yield return new WaitForSeconds(2f);
            clearpanel.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}