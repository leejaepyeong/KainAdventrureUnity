using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// NPC와의 대화 내용 관리
public class StoryOn : MonoBehaviour
{
    public static StoryOn instance; // 스토리 싱글톤

    public NPCStory npcStory;   // 대사 목록

    public bool isStory = false;    // 이야기 중인지

    public GameObject storyPannel;  // 스토리 패널
    public Text storyText;  // 스토리 텍스트
    public Image characterImg;  // 이야기 중인 캐릭터 이미지

    int turn;   // 대화 턴 수 


    private void Start()
    {
        instance = this;
    }

    // 이야기 시작
    public void Story()
    {
        isStory = true;
        storyPannel.SetActive(true);
        turn = 0;


        storyText.text = npcStory.StoryTxt[turn];
        characterImg.sprite = npcStory.npcDB[npcStory.stroyTurn[turn]].charcterImg;
    }

    // 다음 이야기 (턴)
    public void NextBtn()
    {
        turn++;
        // 턴 종료  이야기 종료
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
