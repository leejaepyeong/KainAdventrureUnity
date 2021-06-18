using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryOn : MonoBehaviour
{
    public static StoryOn instance;

    public NPCStory npcStory;

    public bool isStory = false;

    public GameObject storyPannel;
    public Text storyText;
    public Image characterImg;

    int turn;


    private void Start()
    {
        instance = this;
    }

    public void Story()
    {
        isStory = true;
        storyPannel.SetActive(true);
        turn = 0;


        storyText.text = npcStory.StoryTxt[turn];
        characterImg.sprite = npcStory.npcDB[npcStory.stroyTurn[turn]].charcterImg;
    }

    public void NextBtn()
    {
        turn++;
        if (turn >= npcStory.StoryTxt.Length)
        {
            isStory = false;
            storyPannel.SetActive(false);
            return;
        }

        storyText.text = npcStory.StoryTxt[turn];
        characterImg.sprite = npcStory.npcDB[npcStory.stroyTurn[turn]].charcterImg;

        
    }
}
