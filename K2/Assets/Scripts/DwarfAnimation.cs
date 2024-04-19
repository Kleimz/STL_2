using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAnimation : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator anim;
    bool isRunning = false;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovement.jumped)
        {
            anim.Play("@jump running");
        }
        else
        {
            anim.Play("@idle");
        }
    }
}
