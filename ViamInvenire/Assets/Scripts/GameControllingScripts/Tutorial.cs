using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public int SceneToLoad;
    public AudioSource VoiceOver;
    public GameObject Panel;
    private float tutorialLength;
    private float timeElapsed;
    // Use this for initialization
	void Start ()
    {
        Panel.SetActive(false);
        VoiceOver.volume = 1.0f;
        tutorialLength = VoiceOver.clip.length + 3f;
        timeElapsed = 0f;
        Panel.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0);
        VoiceOver.Play();
        VoiceOver.loop = false;
        Debug.Log("tutorialLength: " + tutorialLength);
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > tutorialLength)
        {
            Panel.SetActive(true);
            if (Panel.GetComponent<Image>().color.a < 1.0f)
            {
                Panel.GetComponent<Image>().color += new Color(0.0f, 0.0f, 0.0f, 0.5f) * Time.deltaTime;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneToLoad);
            }
            
        }
	}
}
