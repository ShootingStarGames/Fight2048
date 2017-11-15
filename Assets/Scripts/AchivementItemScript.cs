using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchivementItemScript : MonoBehaviour {
    // 업적 종류
    public int achivementType;

    // view start
    public Image achivementImage;
    public Text achivementName;
    public GameObject achivementSlider;
    public Text achivementSliderText;
    public GameObject achivementRewardButton;
    public Image achivementRewardImage;
    public Text achivementRewardText;
    public Text diaText;
    // view end

    // 업적
    private int level = 0;
    private int cur_state = 0;
    private int goal_state;
    private int reward = 0;
    
    // 업적 확인 // 보상받을 것이 있는지도 확인함
    public bool SetAchivement() {
        bool result = false;

        // 파일에서 level불러오기
        // 파일에서 cur_state를 불러온다
        switch (achivementType)
        {
            // 몬스터 킬수
            case 0:
                level = FileData.GetAchivement_MonsterKillLevel();
                cur_state = FileData.GetMaxKill();
                break;
            // 환생 수
            case 1:
                level = FileData.GetAchivement_ReviveLevel();
                cur_state = FileData.GetRevive();
                break;
            // 층 수
            case 2:
                level = FileData.GetAchivement_FloorLevel();
                cur_state = FileData.GetMaxFloor();
                break;
            // 무기 레벨
            case 3:
                level = FileData.GetAchivement_WeaponLevel();
			cur_state = FileData.GetMaxWeapon();
                break;
            // 방어구 레벨
		case 4:
			level = FileData.GetAchivement_ArmorLevel ();
			cur_state = FileData.GetMaxArmor ();
                break;
            // 캐릭터 레벨
            case 5:
                level = FileData.GetAchivement_PlayerLevel();
                cur_state = FileData.GetMaxLev();
                break;
        }

        // maxlevel인지 확인
        bool isMaxLevel = Achivement.AchivementList.isMaxLevel((Achivement.Type)achivementType, level);
        if (isMaxLevel) {
            // max레벨이면...뭐 완료라고 써야댐..!
            switch (achivementType) {
                case 0:
                    achivementName.text = "몬스터 사냥 MAX";
                    break;
                case 1:
                    achivementName.text = "환생 MAX";
                    break;
                case 2:
                    achivementName.text = "탑 올라가기 MAX";
                    break;
                case 3:
                    achivementName.text = "무기 업그레이드 MAX";
                    break;
                case 4:
                    achivementName.text = "방어구 업그레이드 MAX";
                    break;
                case 5:
                    achivementName.text = "플레이어 레벨 MAX";
                    break;

            }
            achivementRewardButton.SetActive(false); ;
            achivementSlider.SetActive(false);

            return false;
        }

        // level 에 따른 goal 찾기
        goal_state = Achivement.AchivementList.GetGoal((Achivement.Type)achivementType, level);

        // 업적 테스트 넣어주기
        achivementName.text = GetAchivementName(achivementType);

		/*
        // 무기 레벨이랑 방어구 레벨 업적은 1 or 0이다.
        if (achivementType == 3 || achivementType == 4) {
            if (cur_state >= goal_state)
            {
                cur_state = 1;
                goal_state = 1;
            }
            else
            {
                cur_state = 0;
                goal_state = 1;
            }
        }
		*/
        achivementSlider.GetComponent<Slider>().value = (float)cur_state / goal_state;
        
        if (achivementSlider.GetComponent<Slider>().value < 1)
        {
            achivementRewardImage.color = new Color((float)0x8C/0xff, (float)0x8C/0xff, (float)0x8C/0xff);
            achivementRewardButton.GetComponent<Button>().interactable = false;
            result = false;
        }
        else
        {
            achivementRewardImage.color = new Color(1, 1, 1);
            achivementRewardButton.GetComponent<Button>().interactable = true;
            result = true;
        }
        achivementSliderText.text = cur_state.ToString() + "/" + goal_state.ToString();

        reward = Achivement.AchivementList.GetReward((Achivement.Type)achivementType, level);
        achivementRewardText.text = reward.ToString();

        return result;
    }

    string GetAchivementName(int achivementType) {
        // 업적 테스트 넣어주기
        switch (achivementType)
        {
            case 0:
            case 1:
                return string.Format(Achivement.AchivementList.GetAchivementName(achivementType), goal_state);
                case 2:
                return string.Format(Achivement.AchivementList.GetAchivementName(achivementType), goal_state);
            case 3:
                return string.Format(Achivement.AchivementList.GetAchivementName(achivementType), Achivement.AchivementList.GetWeaponName(goal_state));
            case 4:
                return string.Format(Achivement.AchivementList.GetAchivementName(achivementType), Achivement.AchivementList.GetArmorName(goal_state));
            case 5:
                return string.Format(Achivement.AchivementList.GetAchivementName(achivementType), goal_state);
            default:
                return null;
        }
    }

    // 업적 보상 받는 함슈
    void BtnClick() {
        Debug.Log("ButtonClick");
        achivementRewardButton.GetComponent<Button>().interactable = false;
        FileData.SetDiamond(reward);
        diaText.text = FileData.GetDiamond().ToString();

        AudioManager am = AudioManager.Instance();
        am.playCoin();

        switch (achivementType)
        {
            // 몬스터 킬수
            case 0:
                FileData.SetAchivement_MonsterKillLevel();
                SetAchivement();
                break;
            // 환생 수
            case 1:
                FileData.SetAchivement_ReviveLevel();
                SetAchivement();
                break;
            // 층수
            case 2:
                FileData.SetAchivement_FloorLevel();
                SetAchivement();
                break;
            // 무기레벨
            case 3:
                FileData.SetAchivement_WeaponLevel();
                SetAchivement();
                break;
            // 방어구 레벨
            case 4:
                FileData.SetAchivement_ArmorLevel();
                SetAchivement();
                break;
            // 플레이어 레벨
            case 5:
                FileData.SetAchivement_PlayerLevel();
                SetAchivement();
                break;
        }
    }
	// Use this for initialization
	void Start () {
        achivementRewardButton.GetComponent<Button>().onClick.AddListener(BtnClick);
        // 업적 확인
        SetAchivement();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
