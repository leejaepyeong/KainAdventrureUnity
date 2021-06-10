using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHp;

    public Vector3 offset = Vector3.zero;
    public Transform targetTr;    //  타겟 트랜스폼


    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }


    //
    private void LateUpdate()
    {
        //스크린 상의 좌표로 변경
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);

        // x, y값만 필요  z 는 x,y평면 까지의 거리(?)
        if(screenPos.z < 0f)
        {
            screenPos *= -1f;
            
        }

        //스크린에서 캔버스 좌표로 변경
        // localPos에  변경 된 좌표 저장
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent,screenPos, uiCamera, out localPos);

        //체력 게이지의 위치에 저장
        rectHp.localPosition = localPos;

    }
}
