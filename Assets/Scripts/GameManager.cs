using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerStatusData playerData;

    //상태
    public bool is1stCam = true;
    public bool isInfoOn = false;
    

    public Text cameraTxt;  //  카메라 시점 텍스트


    public Image[] weaponImage; // 현재 무기 상태

    //필요 컴포넌트
    public PlayerControleer player;
    public GameObject controlPad;   // 3인칭용 컨트롤 패드
    public GameObject statusCount;  // 스텟 패널
    public Text statusCountTxt; // 스텟 추가 갯수

    public Material[] skyMaterials; // 하늘 매터리얼

    int skynum = 0; // 하늘배경
    float degree = 0;   //회전각

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        ControlStatus();
        RotateSky();
    }

    // 하늘 회전 및 변경
    void RotateSky()
    {
        degree += 10 * Time.deltaTime;

        if (degree >= 360)
        {
            degree = 0;
            skynum++;
            if (skynum >= 4)
                skynum = 0;

            RenderSettings.skybox = skyMaterials[skynum];
        }
            

        RenderSettings.skybox.SetFloat("_Rotation",degree);
    }

    // 카메라 시점 변경
    public void ChangeCamera()
    {
        is1stCam = !is1stCam;

        if (!is1stCam)
        {
            cameraTxt.text = "3";
            controlPad.SetActive(true);


        }
        else
        {
            cameraTxt.text = "1";
            controlPad.SetActive(false);
        }
            
    }

    // 스텟 찍기 열기
    public void ControlStatus()
    {
        if(playerData.stateCount > 0)
        {
            statusCount.SetActive(true);
        }
        else
        {
            statusCount.SetActive(false);
        }
        statusCountTxt.text = playerData.stateCount.ToString();
    }

    
}
