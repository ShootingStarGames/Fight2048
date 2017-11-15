using UnityEngine;
using System.Collections;

public class AchivementScript : MonoBehaviour {
    // 업적스크립트 배열
    public AchivementItemScript[] achivementItem;
    public GameObject exclamationMark;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < achivementItem.Length; i++)
        {
            bool result = achivementItem[i].SetAchivement();
            // result = true 이면 느낌표 띄워주어야함~
            if (result)
            {
                exclamationMark.SetActive(true);
                return;
            }
            else
            {
                exclamationMark.SetActive(false);
            }
        }
    }
}
