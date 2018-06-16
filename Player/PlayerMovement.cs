using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float MaxVelocity = 300f;

    [SerializeField]
    float regVelocity = 170f;

    public float speed;

    CharacterController player;

    PlayerAnimations anim;

    public bool ENABLED = true;

    [SerializeField]
    Camera cam;

    // Use this for initialization
    void Start ()
    {
        speed = regVelocity;

        player = GetComponent<CharacterController>();
        anim = GetComponent<PlayerAnimations>();

    }

    public void MoveCharacter(float h, float v, bool Sprint)
    {
        Vector3 DirVector = new Vector3();
        DirVector.x = h;
        DirVector.y = 0f;
        DirVector.z = v;
        DirVector = DirVector.normalized;
        //Change speed
        if (v != 0)
        {
            if (Sprint)
            {
                speed = MaxVelocity;
                anim.Running();
            }
            else
            {
                speed = regVelocity;
                anim.Walking();
            }
        }
        else
        {
            speed = regVelocity;
            anim.Idle();
        }
        if (ENABLED)
        {
            if (Vector3.Magnitude(player.velocity) < MaxVelocity)
            {
                player.SimpleMove(transform.forward * v * Time.deltaTime * speed);
                player.SimpleMove(transform.right * h * Time.deltaTime * speed);
            }
        }
    }

}
