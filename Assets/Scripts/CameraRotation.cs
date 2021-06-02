using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Rigidbody playerRigid;

    [SerializeField]
    private float lookSensitivity;  // ī�޶� ����

    //ī�޶� �Ѱ�
    [SerializeField]
    private float cameraRotationLimit;  // ī�޶� ���� 
    private float currentCameraRotationX = 0;   // ���� ���� ����

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
        //�¿� ĳ���� ȸ��
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;

        playerRigid.MoveRotation(playerRigid.rotation * Quaternion.Euler(_characterRotationY));
        // ���Ϸ� ȸ������  ������ ȸ������ ��
    }

    void CameraRotations()
    {
        // ���� ī�޶� ȸ��

        float _xRotation = Input.GetAxisRaw("Mouse Y"); // ���콺 ���� ȸ��
        float _cameraRotationX = _xRotation * lookSensitivity;  //������ ���� ī�޶� ȸ����


        currentCameraRotationX -= _cameraRotationX; //�� ���¿��� ȸ���� ���ϱ�
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        // �ּ�  �ִ�  ī�޶� ȸ���� ����

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

    }
}
