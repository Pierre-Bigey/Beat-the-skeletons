using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private PlayerManager playerManager;
    private AnimatorManager animatorManager;
    private PlayerAudio playerAudio;

    public bool isBlocking { get; set; }
    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        playerAudio = GetComponent<PlayerAudio>();
        //Debug.Log("Setting is punching to false at awake");

        updateBlocking(false);

    }

    private void Update()
    {
        //Debug.Log("UPDATE : isPunching = " + isPunching);
    }

    public void updateBlocking(bool value)
    {
        isBlocking = value;
        playerManager.updateBlocking(value);
    }
    

    public void HandlePunching()
    {
        //Debug.Log("HandlePunching with : animatorIsPunching  " + animatorManager.animator.GetBool("isPunching"));
        if (!animatorManager.animator.GetBool("isPunching") && !isBlocking)
        {
            playerAudio.PlayPunchSound();
            animatorManager.PlayTargetAnimation("PunchLeft");
        }
    }

    public void HandleBLocking()
    {

        updateBlocking(true);
        if (!animatorManager.animator.GetBool("isPunching"))
        {
            animatorManager.PlayTargetAnimation("BlockingLoop");
        }
    }



    /*public void HandlePunching()
    {
        Debug.Log("HandlePunching with : animatorIsPunching  "+ animatorManager.animator.GetBool("isPunching"));
        Debug.Log("player action is Punching :"+ isPunching);
        if (!animatorManager.animator.GetBool("isPunching"))
        {
            if (!isPunching)
            {
                Debug.Log("Playing first animation, setting is punching to true");
                isPunching = true;
                animatorManager.animator.SetBool("isPunching", false);
                animatorManager.PlayTargetAnimation("PunchLeft", false);
                Debug.Log("Just a test : isPunching " + isPunching);
            }
            else
            {
                //isPunching = false;
                animatorManager.animator.SetBool("isPunching", true);
                //animatorManager.PlayTargetAnimation("PunchRight", false);
            }
        }
        else
        {
            animatorManager.animator.SetBool("isPunching", false);
        }
    }*/
}
