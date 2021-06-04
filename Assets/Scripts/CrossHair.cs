using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Animator animator;


    [SerializeField]
    private GameObject go_CrosshairHUD; //  ũ�ν���� Ȱ�� ��Ȱ��ȭ


    public void WalkingAnimation(bool _flag)
    {
        animator.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void JumpingAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void FireAnimation()
    {
        animator.SetTrigger("Idle_Fire");
        
    }

    
}
