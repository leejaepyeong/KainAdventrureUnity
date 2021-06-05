using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{

    // ????
    public Transform Player;        //  ????.
    public Transform Stick;         // stick

    // ??????
    private Vector3 StickFirstPos;  // ?? ?? ??
    private Vector3 JoyVec;         // ??
    private float Radius;           // ???
    private bool MoveFlag;          // ???

    void Start()
    {
        Radius = GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        StickFirstPos = Stick.transform.position;

        // ?? ???? ?
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;  // ???? ?? ??? ??

        MoveFlag = false;
    }

    void Update()
    {
        if (MoveFlag)
            Player.transform.Translate(Vector3.forward * Time.deltaTime * 10f);
    }

    // ???
    public void Drag(BaseEventData _Data)
    {
        MoveFlag = true;
        PointerEventData Data = _Data as PointerEventData;
        Vector3 Pos = Data.position;

        
        JoyVec = (Pos - StickFirstPos).normalized;

        
        float Dis = Vector3.Distance(Pos, StickFirstPos);

        
        if (Dis < Radius)
            Stick.position = StickFirstPos + JoyVec * Dis;
        
        else
            Stick.position = StickFirstPos + JoyVec * Radius;

        Player.eulerAngles = new Vector3(0, Mathf.Atan2(JoyVec.x, JoyVec.y) * Mathf.Rad2Deg, 0);
    }

    // ??? ?? > ???
    public void DragEnd()
    {
        Stick.position = StickFirstPos; // ?? ??
        JoyVec = Vector3.zero;          
        MoveFlag = false;
    }
}
