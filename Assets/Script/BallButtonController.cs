using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallButtonController : MonoBehaviour
{
    private Animator anim;
    
    private void Start(){
        anim = GetComponent<Animator>();
    }

    public void OnTriggerButton(){
        if (BallAttribute.selectedCount == 0){ //src selected
            BallAttribute.selectedCount = 1;

            //Set animation for this ball;
            anim.SetBool("isSelected", true);
            //Play selected sound
        }
        else if (BallAttribute.selectedCount == 1){ //des selected
            BallAttribute.selectedCount = 0;
            //Checking condition for moving;
            // if (BallMatrix.ins)
        }
    }
}
