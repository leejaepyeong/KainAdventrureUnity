using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpZone : MonoBehaviour
{
    public GameObject warpPannel;
    public InfoManager infoManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player" && !infoManager.isInfoOn)
            warpPannel.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
            warpPannel.SetActive(false);
    }
}
