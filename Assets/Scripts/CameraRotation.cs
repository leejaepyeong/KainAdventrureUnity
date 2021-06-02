using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Rigidbody playerRigid;

    [SerializeField]
    private float lookSensitivity;  // 카메라 감도

    //카메라 한계
    [SerializeField]
    private float cameraRotationLimit;  // 카메라 각도 
    private float currentCameraRotationX = 0;   // 현재 보는 각도

    public Camera theCamera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotations();
        CharacterRotation();

    }

    void CharacterRotation()
    {
        //좌우 캐릭터 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
        // 오일러 회전값을  현상태 회전값에 곱
    }

    void CameraRotations()
    {
        // 상하 카메라 회전

        float _xRotation = Input.GetAxisRaw("Mouse Y"); // 마우스 각도 회전
        float _cameraRotationX = _xRotation * lookSensitivity;  //감도에 따른 카메라 회전값


        currentCameraRotationX -= _cameraRotationX; //현 상태에서 회전값 더하기
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        // 최소  최대  카메라 회전값 설정

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }
}
