using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour {

    [Header("5 Texts Required: Text, Text1, Text2, Text3, Text4 MUST BE THEIR NAMES")]
    [Header("They correspond to the spawn locales")]
    public Text Text = null;
    public Text Text1 = null;
    public Text Text2 = null;
    public Text Text3 = null;
    public Text Text4 = null;

    private GameObject gameManager;
    private float timeElapsed = 0f;
    private bool text1FadeInComplete = false;
    private bool fadeOutComplete = false;
    private Text objectiveText;
    private int spawnIndex;
    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        spawnIndex = gameManager.GetComponent<SelectWinAndSpawnLocation>().getSpawnIndex();
        Debug.Log("SpawnIndex: " + spawnIndex);
        if(spawnIndex == 0)
        {
            objectiveText = GameObject.Find("Text").GetComponent<Text>();
            GameObject.Find("Text1").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text2").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text3").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text4").GetComponent<Text>().gameObject.SetActive(false);
        }
        else if(spawnIndex == 1)
        {
            objectiveText = GameObject.Find("Text1").GetComponent<Text>();
            GameObject.Find("Text").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text2").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text3").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text4").GetComponent<Text>().gameObject.SetActive(false);
        }
        else if(spawnIndex == 2)
        {
            objectiveText = GameObject.Find("Text2").GetComponent<Text>();
            GameObject.Find("Text").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text1").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text3").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text4").GetComponent<Text>().gameObject.SetActive(false);
        }
        else if(spawnIndex == 3)
        {
            objectiveText = GameObject.Find("Text3").GetComponent<Text>();
            GameObject.Find("Text").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text1").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text2").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text4").GetComponent<Text>().gameObject.SetActive(false);
        }
        else if(spawnIndex == 4)
        {
            objectiveText = GameObject.Find("Text4").GetComponent<Text>();
            GameObject.Find("Text").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text1").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text2").GetComponent<Text>().gameObject.SetActive(false);
            GameObject.Find("Text3").GetComponent<Text>().gameObject.SetActive(false);
        }
        objectiveText.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > 2f && !text1FadeInComplete)
        {
            Color curColor1 = objectiveText.color;
            curColor1 += new Color(1f, 1f, 1f, 1f) * Time.deltaTime;
            objectiveText.color = curColor1;
            if (curColor1.a >= 0.9f)
            {
                text1FadeInComplete = true;
            }
        }

        if (timeElapsed > 10f && !fadeOutComplete)
        {
            Debug.Log("Clearing text");
            Color curColor1 = objectiveText.color;
            curColor1 -= new Color(1f, 1f, 1f, 1f) * Time.deltaTime;
            objectiveText.color = curColor1;
            if (curColor1.a <= 0f)
            {
                objectiveText.gameObject.SetActive(false);
                fadeOutComplete = true;
            }
        }
    }
}
