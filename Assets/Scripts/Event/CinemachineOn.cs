using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineOn : MonoBehaviour
{
    public Event cineEvent;
    public bool isOn = false;   // 시네머신 했는지
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isOn)
        {
            cineEvent.LastBossCinematic();
            isOn = true;
        }
    }
}
