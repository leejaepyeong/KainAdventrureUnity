using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject pannel;

    public Animator anim;


    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    public void OpenPannel()
    {
        pannel.SetActive(true);
    }

    public void closePannel()
    {
        pannel.SetActive(false);
    }

}
