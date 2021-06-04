using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //상태
    public bool is1stCam = true;
    public bool isInfoOn = false;

    public Text cameraTxt;  //  카메라 시점 텍스트

    public int stat = 0;   // 스탯 추가 갯수

    public Image[] weaponImage; // 현재 무기 상태

    //필요 컴포넌트
    public PlayerControleer player;
    public GameObject controlPad;
    public GameObject statusCount;
    public Text statusCountTxt;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        ControlStatus();
    }

    public void ChangeCamera()
    {
        is1stCam = !is1stCam;

        if (!is1stCam)
        {
            cameraTxt.text = "3";
            player.theCamera[1].gameObject.SetActive(true);
            player.theCamera[0].gameObject.SetActive(false);
            controlPad.SetActive(true);


        }
        else
        {
            cameraTxt.text = "1";
            player.theCamera[1].gameObject.SetActive(false);
            player.theCamera[0].gameObject.SetActive(true);
            controlPad.SetActive(false);
        }
            
    }

    public void ControlStatus()
    {
        if(stat > 0)
        {
            statusCount.SetActive(true);
        }
        else
        {
            statusCount.SetActive(false);
        }
        statusCountTxt.text = stat.ToString();
    }

    
}
