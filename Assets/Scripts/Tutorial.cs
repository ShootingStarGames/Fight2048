using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    Image panel;
    Sprite[] img;
	// Use this for initialization

    public void ShowTutorial()
    {
        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        for (int i = 0; i < 10; i++)
        {
            panel.sprite = img[i];
            yield return new WaitForSeconds(0.5f);
        }
        panel.gameObject.SetActive(false);
    }

    void Start () {
        img = new Sprite[10];
        for (int i = 0; i < 10; i++) {
            img[i] = Resources.Load("Images/tuto/" + (i+1).ToString("D2"), typeof(Sprite)) as Sprite;
        }
        panel = this.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
