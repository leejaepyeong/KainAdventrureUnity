using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

    public Camera mainCam;
    public Camera seconCam;

    private void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);

        if (GameManager.instance.is1stCam)
            cam = mainCam.transform;
        else
            cam = seconCam.transform;
    }
}
