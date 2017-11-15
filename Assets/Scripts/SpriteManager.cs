using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    enum UNIT_TYPE
    {
        WARRIOR = 0,
    }

    enum ENEMY_TYPE
    {
        GOBLIN = 0,SLIME =1,
    }

    class SpriteManager
    {
        static public Sprite[] unit_sprites;
        static public Sprite[] Archor;
        static public Sprite[] enemy_sprites;
        static public Sprite[] boss_sprites;

        public SpriteManager(int unit, int enemy) {
            switch ((UNIT_TYPE)unit) {
                case UNIT_TYPE.WARRIOR:
                    create_warrior();
                    break;
            }
            switch ((ENEMY_TYPE)enemy) {
                case ENEMY_TYPE.GOBLIN:
                    create_goblin();
                    break;
                case ENEMY_TYPE.SLIME:
                    create_slime();
                    break;
            }

            create_boss();
        }

        private void create_warrior() {
            unit_sprites = new Sprite[14];
            for (int i = 0; i < 10; i++)
            {
                String s = "Images/Units/Fighter/warrior" + (i + 1).ToString("D2");

                unit_sprites[i] = Resources.Load(s, typeof(Sprite)) as Sprite;
            }
        }
        private void create_goblin() {
            enemy_sprites = new Sprite[14];
            for (int i = 0; i < 10; i++)
            {
                String s = "Images/Enemy/Goblin/goblin" + (i + 1).ToString("D2");

                enemy_sprites[i] = Resources.Load(s, typeof(Sprite)) as Sprite;
            }
        }

        private void create_slime()
        {
            enemy_sprites = new Sprite[14];
            for (int i = 0; i < 10; i++)
            {
                String s = "Images/Enemy/Slime/slime" + (i + 1).ToString("D2");

                enemy_sprites[i] = Resources.Load(s, typeof(Sprite)) as Sprite;
            }
        }

        private void create_boss()
        {
            boss_sprites = new Sprite[3];
            for (int i = 0; i < 2; i++)
            {
                String s = "Images/Enemy/Boss/boss" + (i + 1).ToString("D2");

                boss_sprites[i] = Resources.Load(s, typeof(Sprite)) as Sprite;
            }
        }
    }
}
