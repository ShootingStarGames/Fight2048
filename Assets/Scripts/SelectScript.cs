using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SelectScript : MonoBehaviour {

    private enum Character_name {none, warrior ,archer, magician};
    private bool [] Char_array = { false, false, false };
    public Image fadepanel;
    public GameObject story,story_text;
    public GameObject[] character;
    

    // Use this for initialization
    private int character_num = 1;
    private GameObject currentcharacter;
    private Vector3 centerpos = new Vector3(0, -1, 0);
    private Vector3 rightpos = new Vector3(1.5f, 0, 10f);
    private Vector3 leftpos = new Vector3(-1.5f, 0, 10f);
    private bool sliding;

    public void OwnCharacter()
    {
        int n = FileData.GetOwn_Char();
        Char_array[0] = true;
        if (n == 3)
        {
            Char_array[1] = true;
        }
        else if (n == 5)
        {
            Char_array[1] = true;
        }
        else if (n == 7)
        {
            Char_array[1] = true;
            Char_array[2] = true;
        }
    }

    public void Quitgame()
    {
        Application.Quit();
    }

    public void Playgame()
    {
        if (Char_array[character_num - 1] == true)
        {
            FileData.SetCurrent_Char(character_num);
            SceneManager.LoadScene("ingame");
        }
    }



    private IEnumerator Slide(bool Left)
    {
        sliding = true;
        float elapsedTime = 0;
        float time = 1;
        int l, m, r;
        m = character_num-1;
        if (character_num == 3)
        {
            r = 0;
            l = m - 1;
        }
        else if(character_num == 1)
        {
            r = m + 1;
            l = 2;
        }
        else
        {
            r = m + 1;
            l = m - 1;
        }
        while (elapsedTime < time)
        {
            if (Left)
            {
                character[m].GetComponent<Transform>().localPosition = Vector3.Lerp(character[m].GetComponent<Transform>().localPosition, centerpos, elapsedTime/ time);
                character[r].GetComponent<Transform>().localPosition = Vector3.Lerp(character[r].GetComponent<Transform>().localPosition, rightpos, elapsedTime / time);
                character[l].GetComponent<Transform>().localPosition = Vector3.Lerp(character[l].GetComponent<Transform>().localPosition, leftpos, elapsedTime / time);
            }
            else
            {
                character[m].GetComponent<Transform>().localPosition = Vector3.Lerp(character[m].GetComponent<Transform>().localPosition, centerpos, elapsedTime / time);
                character[l].GetComponent<Transform>().localPosition = Vector3.Lerp(character[l].GetComponent<Transform>().localPosition, leftpos, elapsedTime / time);
                character[r].GetComponent<Transform>().localPosition = Vector3.Lerp(character[r].GetComponent<Transform>().localPosition, rightpos, elapsedTime / time);
            }
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        sliding = false;
    }
    IEnumerator FadeIn()
    {
        //Debug.Log("FadeIn");
        for (int i = 20; i >= 0; i--)
        {
            fadepanel.color = new Vector4(0, 0, 0, (float)i / 20);
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator FadeOut()
    {
        for (int i = 0; i <= 20; i++)
        {
            fadepanel.color = new Vector4(0, 0, 0, (float)i / 20);
            yield return new WaitForSeconds(0.05f);
        }
        story.SetActive(false);
        StartCoroutine(FadeIn());
    }
    IEnumerator StoryUp()
    {
        for (int i = -230; i <= 230; i++)
        {
#if UNITY_ANDROID
            if (Input.touchCount == 1)
            {
                break;
            }
            #elif UNITY_EDITOR || UNITY_STANDARD
            if (Input.GetMouseButton(0))
            {
            break;
            }
            #endif
            story_text.transform.localPosition = new Vector3(story_text.transform.localPosition.x, i, 0);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(FadeOut());
    }

    void Start ()
    {
        if (FileData.GetFirstGame() == 2)
            Playgame();
        OwnCharacter();
        character_num = 1;
        sliding = false;
        StartCoroutine(FadeIn());
        StartCoroutine(StoryUp());
        //Tutorial t = new Tutorial();
        //t.ShowTutorial();
    }


    public void right()
    {
        if (sliding)
            return;
        if (character_num == 3)
            character_num = 1;
        else
            character_num++;
        StartCoroutine(Slide(true));
    }
    public void left()
    {
        if (sliding)
            return;
        if (character_num == 1)
            character_num = 3;
        else
            character_num--;
        StartCoroutine(Slide(false));
    }

    // Update is called once per frame
    void Update () {
    }



}
