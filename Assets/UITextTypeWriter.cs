using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITextTypeWriter : MonoBehaviour {

    private Text txt;
    private string story;
    private bool isTxtEmpty = true;

    static string[] storyText = new string[]{
        "좋은 물건 있으니까 한번 둘러봐",
        "열심히 개발했어요~ 재밌게 플레이 해주세요 ^^",
        "업적 보상도 잘 챙겨가~",
        "버튼 눌러봐.\n벤자민버튼\n쿠쿸ㅋ",
        "공격력이 너무 약하다고? 날 눌러!",
        "고블린이 너무 세? 날 눌러!",
        "죽으면 탑의 높이에 따라 보석을 준다구!",
        "스킬을 업그레이드 하고 싶지 않아?",
        "돈 쓸데가 없어? 좀 쓰고가",
        "광고도 보고가~",
        "그 돈은 다 저승길 노잣돈인가?",
    };
    string GetStory() {
        int r = Random.Range(0, storyText.Length);
        return storyText[r];
    }
    void Awake()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        
    }
    void Update() {
        if (isTxtEmpty)
        {
            isTxtEmpty = false;
            // TODO: add optional delay when to start
            StartCoroutine("PlayText");
        }
    }
    IEnumerator PlayText()
    {
        txt.text = "";
        story = GetStory();
        foreach (char c in story)
        {
            txt.text += c;
            yield return new WaitForSeconds(0.125f);
        }
        yield return new WaitForSeconds(1.0f);
        isTxtEmpty = true;
    }
}
