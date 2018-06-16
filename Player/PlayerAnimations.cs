using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimations : MonoBehaviour
{

    Animator anim;
    PlayerMovement player;

    float timer = 0f;

    public bool AI = false;

    void Start ()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerMovement>();
        SetGunEnable(true);
    }

    void Update()
    {
        if (AI)
        {
            return;
        }

       if (timer <= 0f)
       {
            player.ENABLED = true;
       }
       else
       {
            player.ENABLED = false;
            timer -= Time.deltaTime;
       }

    }

    public void SetGunEnable(bool state)
    {
         anim.SetBool("HasGun", state);
    }

    public void Idle()
    {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsGrabbing", false);
            anim.SetBool("IsShooting", false);
    }

    
    public void Walking()
    {
        anim.SetBool("IsWalking",true);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsGrabbing", false);
        anim.SetBool("IsShooting", false);
    }

    public void Shooting()
    {
        anim.SetBool("IsShooting", true);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsGrabbing", false);       
    }

    public void Running()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", true);
        anim.SetBool("IsGrabbing", false);
        anim.SetBool("IsShooting", false);
    }

    public void Grabbing()
    {
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsGrabbing", true);
        anim.SetBool("IsShooting", false);

        timer = 3f;
    }

}
