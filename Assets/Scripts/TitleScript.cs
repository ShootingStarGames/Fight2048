using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour {

    public SpriteRenderer fadepanel;
    public AudioSource audio;
    public SpriteRenderer Whitepanel;
    IEnumerator FadeOutFirst()
    {
        //Debug.Log("FadeIn");
        for (int i = 0; i <= 30; i++)
        {
            fadepanel.color = new Vector4(0, 0, 0,(float)i/30);
            yield return new WaitForSeconds(0.05f);
        }
        Whitepanel.gameObject.SetActive(false);
        fadepanel.color = new Vector4(0, 0, 0, 0);
        audio.Play();
    }

    void FadeIn()
    {
        Whitepanel.gameObject.SetActive(false);
        fadepanel.color = new Vector4(0, 0, 0, 0);
        audio.Play();
    }
    IEnumerator FadeInFirst()
    {
        for (int i = 30; i >= 0; i--)
        {
            fadepanel.color = new Vector4(0, 0, 0, (float)i / 30);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(FadeOutFirst());

    }
    IEnumerator FadeOut()
    {
        for (int i = 0; i <= 20; i++)
        {
            fadepanel.color = new Vector4(0, 0, 0, (float)i / 20);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartGame()
    {
        StartCoroutine(FadeOut());
        SceneManager.LoadScene("select");
    }

    void Awake()
    {
        Screen.SetResolution(720, 1280, true);
        CGoogleplayGameServiceManager.Init();
        CGoogleplayGameServiceManager.Login();
    }

    void Start () {
        StartCoroutine(FadeInFirst());
    }
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID 
        if (Input.touchCount == 1)
        {
            if (!Whitepanel.gameObject.activeSelf)
            {
                StartGame();
            }               
        }
#elif UNITY_EDITOR || UNITY_STANDARD
        if (Input.GetMouseButton(0))
        {
            if (!Whitepanel.gameObject.activeSelf){
                StartGame();
            }        
        }
#endif
    }
}
