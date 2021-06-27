using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;


    private void Start()
    {
        cam = Camera.main.transform;

    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);

        if(cam != Camera.main.transform)
            cam = Camera.main.transform;
    }
}
