using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialWindow;
    public Text tutorialText;
    [HideInInspector] public int tutorialStage = -1;
    public string[] tutorialTexts;
    public WorldController world;
    public PlayerController player;

    public GameObject promptParent;
    public Text prompt;
    public string[] promptsTexts;
    void Start()
    {
        if(world.levelStates[world.curLevel].Id == 1)
        {
            tutorialStage = 0;
            tutorialText.text = tutorialTexts[0];
            prompt.text = promptsTexts[tutorialStage];
            tutorialWindow.SetActive(true);
            promptParent.SetActive(true);
            
            Time.timeScale = 0;
        }
    }

    public void CloseTutorialWindow()
    {
        tutorialWindow.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (world.levelStates[world.curLevel].Id == 1)
        {
            TutorialProcess();
        }
    }

    public void TutorialProcess()
    {
        if (tutorialStage == 0)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                tutorialStage++;
                StartCoroutine(NextStage(2f));
            }
        }
        else if (tutorialStage == 1)
        {
            if(player.isReloading)
            {
                tutorialStage++;
                StartCoroutine(NextStage(2f));
            }
        }
    }

    private IEnumerator NextStage(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialText.text = tutorialTexts[tutorialStage];
        tutorialWindow.SetActive(true);
        prompt.text = promptsTexts[tutorialStage];
        Time.timeScale = 0;
    }
}
