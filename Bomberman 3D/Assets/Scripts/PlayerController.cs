using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum SpeedTypes
//{
//    Slowest = 1, Slow = 2, Normal = 3
//}

public class PlayerController : CharacterControllerBase
{
    public Player player;
    protected AudioSource audioSource;
    public AudioClip stepSound;
    public AudioClip putBombSound;


    private BombManager bombManager;
    private float timeToReload = 0.2f;
    private Animator animator;

    void Start()
    {
        player = new Player(GameObject.FindGameObjectWithTag("Player"));
        bombManager = GetComponent<BombManager>();
        audioSource = GetComponentInChildren<AudioSource>();
        base.OnStart();
        animator = player.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        MonitorInput();
    }

    public virtual void MonitorInput( )
    {
        CheckPlayerActions();
        CheckMovingAttempts();
    }

    protected virtual void CheckPlayerActions()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (bombManager == null) return;
            if (bombManager.bombs.Count < player.bombNumber)
            {
                animator.SetTrigger("DropBomb");
                bombManager.DropNewBomb(transform.position, player.explosionRadius);
            }
        }
        else if (Input.GetKeyUp(KeyCode.B) && player.abilities.Contains(PowerUp.Detonator))
        {
            bombManager.ExplodeAllBombs();
        }
    }

    protected virtual void CheckMovingAttempts()
    {
        int horizontal = 0;
        int vertical = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -1;
        }

        if (horizontal != 0)
        {
            vertical = 0;
        }
        if (horizontal != 0 || vertical != 0)
        {
            RaycastHit hit;
            Move(horizontal, vertical, out hit, player);
            //Debug.Log("TOTAL ABILITIES: " + player.abilities.Count);
        }
    }

    public void PlayStepSound()
    {
        audioSource.PlayOneShot(stepSound);
    }

    public void PlayPutBombSound()
    {
        audioSource.PlayOneShot(putBombSound);
    }
}
