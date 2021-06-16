using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Animator anim;

    public enum NPCType {Shop, Nurse, Smith, HideMap }
    public NPCType npcType;

   


    private void Start()
    {
        anim = GetComponentInParent<Animator>();

    }

    public void OpenPannel()
    {
        WhoMeet.instanse.npcPannels[WhoMeet.instanse.number].SetActive(true);
    }

    public void closePannel()
    {
        WhoMeet.instanse.npcPannels[WhoMeet.instanse.number].SetActive(false);
    }

    

}
