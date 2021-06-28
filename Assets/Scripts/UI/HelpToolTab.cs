using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HelpToolTab : MonoBehaviour, IPointerClickHandler
{
    public int ID;

    // 필요 컴포넌트
    public HelpControl helpControl; 


    // 메뉴 탭 바꾸기
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            helpControl.MenuTab(ID);
        }

    }
}
