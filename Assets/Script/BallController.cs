using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Animator anim;
    
    private void Start(){
        anim = GetComponent<Animator>();
    }

    public void SelectBall(){
        anim.SetBool("isSelected", true);
    }

    public void UnselectedBall(){
        anim.SetBool("isSelected", false);
    }

    public void Explosive(){
        anim.SetBool("isExploding", true);
    }
}
