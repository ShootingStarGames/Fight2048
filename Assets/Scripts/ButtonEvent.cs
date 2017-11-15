using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject skillWindow;
    public Text skillName;
    public Text skillInfo;
    public int skillNum;

    private string[] skillNameArray = { "체력증진(PASSIVE)", "회복(ACTIVE)", "야압!(ACTIVE)","이건뭐지(ACTIVE)" };
    private string[] skillInfoArray = { "전체 체력을 퍼센트로 올려주는 스킬", "체력 비례 회복 스킬", "범위 공격", "단일 집중 공격" };

    public ButtonScript bs;
    private bool pressed = false;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        TouchTest.setTouch(false);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TouchTest.setTouch(true);
    }
    public void OnPointerDown(PointerEventData eventData) {
        pressed = true;
        Invoke("OnLongPress", 0.5f);
        Debug.Log("dd");
    }
    public void OnPointerUp(PointerEventData eventData) {
        pressed = false;
        Debug.Log("ee");
        skillWindow.SetActive(false);
    }
    public void OnLongPress() {
        Debug.Log("OnLongPress");
        if (pressed)
        {
            Debug.Log("pressed!");
            skillName.text = skillNameArray[skillNum];
            skillInfo.text = skillInfoArray[skillNum];
            skillWindow.SetActive(true);
        }
        else {
            bs.Skill(skillNum);
            pressed = false;
        }
    }
}
