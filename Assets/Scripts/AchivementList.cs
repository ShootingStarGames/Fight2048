using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Achivement
{
    class AchivementList
    {
        static string[] achivementName = new string[] {
            "몬스터 사냥 {0}마리",
            "100층 이상 환생 {0}회",
            "{0}층 달성",
            "무기 {0} 획득하기",
            "방어구 {0} 획득하기",
            "플레이어 레벨 {0} 달성"
        };

        // 몬스터 사냥 업적 {목표, 보상}
        static int[][] monster = new int[][]{
            new int[]{ 10, 1},
            new int[]{ 30, 1},
            new int[]{ 50, 1},
            new int[]{ 100, 1},
            new int[]{ 200, 1},
            new int[]{ 500, 1},
            new int[]{ 1000, 1},
            new int[]{ 1500, 3},
            new int[]{ 2500, 5},
            new int[]{ 3500, 10},
            new int[]{ 10000, 10}
        };
        // 환생 업적 {목표레벨, 보상}
        static int[][] rebirth = new int[][]{
            new int[]{ 1, 1},
            new int[]{ 3, 1},
            new int[]{ 5, 1},
            new int[]{ 10, 1},
            new int[]{ 20, 1},
            new int[]{ 30, 1},
            new int[]{ 50, 1},
        };
        static int[] rebirthFloor = new int[] {
            100, 200, 300, 500, 1000
        };

        // 층수 업적 {목표층수, 보상}
        static int[][] floor = new int[][]{
            new int[]{ 50, 1},
            new int[]{ 100, 1},
            new int[]{ 200, 2},
            new int[]{ 300, 3},
            new int[]{ 400, 4},
            new int[]{ 500, 6},
            new int[]{ 600, 8},
            new int[]{ 700, 10},
        };
        // 무기 레벨 업적 {레벨, 보상}
        static int[][] weapon = new int[][]{
            new int[]{ 10, 1},
            new int[]{ 20, 1},
            new int[]{ 30, 1},
            new int[]{ 40, 1},
            new int[]{ 50, 2},
            new int[]{ 60, 3},
            new int[]{ 70, 4},
            new int[]{ 80, 5},
        };
        // 무기 네임
        static string[] weaponName = new string[] {
            "단검",
            "장검",
            "시미터",
            "그레이트 소드",
            "플랑베르쥬"
        };
        // 방어구 레벨 업적 {레벨, 보상}
        static int[][] armor = new int[][]{
            new int[]{ 10, 1},
            new int[]{ 20, 1},
            new int[]{ 30, 1},
            new int[]{ 40, 1},
            new int[]{ 50, 2},
            new int[]{ 60, 3},
            new int[]{ 70, 4},
            new int[]{ 80, 5},
        };
        // 방어구 네임
        static string[] armorName = new string[] {
            "원형방패",
            "사각방패",
            "군용방패",
            "카이트 실드",
            "타워실드"
        };
        // 플레이어 레벨 업적 {레벨, 보상}
        static int[][] p_level = new int[][] {
            new int[] {3, 1 },
            new int[] {4, 1 },
            new int[] {5, 1 },
            new int[] {6, 1 },
            new int[] {7, 1 },
            new int[] {8, 1 },
            new int[] {9, 1 },
            new int[] {10, 1 },
            new int[] {11, 1 },
            new int[] {12, 1 },
            new int[] {13, 1 },
            new int[] {14, 1 },
            new int[] {15, 1 },
            new int[] {16, 1 },
            new int[] {17, 1 },
        };

        static public string GetAchivementName(int type) {
            return achivementName[type];
        }
        static int[][] GetAchivementArray(Type type) {
            switch (type) {
                case Type.ARMOR:
                    return armor;
                case Type.FLOOR:
                    return floor;
                case Type.MONSTER:
                    return monster;
                case Type.REBIRTH:
                    return rebirth;
                case Type.WEAPON:
                    return weapon;
                case Type.PLAYERLEVEL:
                    return p_level;
            }
            return null;
        }
        static public bool isMaxLevel(Type type, int level) {
            int[][] array = GetAchivementArray(type);
            return (array.Length<=level)?true:false;
        }
        static public int GetGoal(Type type, int level) {
            int[][] array = GetAchivementArray(type);
            return array[level][0];
        }
        static public int GetReward(Type type, int level)
        {
            int[][] array = GetAchivementArray(type);
            return array[level][1];
        }
        static public string GetWeaponName(int level) {
            return weaponName[(level-1) / 10];
        }
        static public string GetArmorName(int level) {
            return armorName[(level-1) / 10];
        }
    }
}
