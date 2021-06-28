using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpControl : MonoBehaviour
{
    public HelpTool[] helpTool;    //툴 박스

    int helpNum;    // 메뉴 아이디(?)
    int turn;   // 페이지 수 

 

    public Text titleTxt;
    public Text descTxt;
    public Image DescImg;

    // 메뉴 탭 클릭
    public void MenuTab(int _num)
    {
        helpNum = _num;
        turn = 0;

        titleTxt.text = helpTool[helpNum].HelpTiltle;
        descTxt.text = helpTool[helpNum].HelpTxt[turn];
        DescImg.sprite = helpTool[helpNum].HelpImg[turn];

    }

    //다음페이지
    public void NextBtn()
    {
        turn++;
        if (turn >= helpTool[helpNum].HelpImg.Length)
            turn = helpTool[helpNum].HelpImg.Length-1;

        descTxt.text = helpTool[helpNum].HelpTxt[turn];
        DescImg.sprite = helpTool[helpNum].HelpImg[turn];
    }

    //이전페이지
    public void PreviewBtn()
    {
        turn--;
        if (turn <= 0)
            turn = 0;

        descTxt.text = helpTool[helpNum].HelpTxt[turn];
        DescImg.sprite = helpTool[helpNum].HelpImg[turn];
    }
}
