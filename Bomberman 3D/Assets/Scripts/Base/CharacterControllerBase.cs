using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{

    private Rigidbody rigidBody;

    public int lockMove;

    protected virtual void OnStart()
    {
        rigidBody = GetComponent<Rigidbody>();
        lockMove = 0;
    }

    void Start()
    {
        OnStart();
    }

    protected void TurnHead(int xDir, int zDir)
    {
        if (xDir == -1)
            transform.eulerAngles = new Vector3(0, -90, 0);
        if (xDir == 1)
            transform.eulerAngles = new Vector3(0, 90, 0);
        if (zDir == -1)
            transform.eulerAngles = new Vector3(0, 180, 0);
        if (zDir == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public virtual bool Move(int xDir, int zDir, out RaycastHit hit, CharacterBase character)
    {

        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, 0f, zDir);
        Physics.Linecast(start, end, out hit);

        if (hit.transform == null || hit.transform.gameObject.CompareTag("Brick") && character.abilities.Contains(PowerUp.Wallpass) //.Exists(ability => ability == PowerUp.Wallpass) //.Contains(PowerUp.Wallpass)
                || hit.transform.gameObject.CompareTag("PowerUp") || hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("Bomb"))
            {
            Animator anim = character.gameObject.GetComponent<Animator>();

                GoSmooth(xDir, zDir, end, character.speed, character.gameObject.GetComponent<Animator>());
                return true;
            }
        return false;
    }

    public virtual void GoSmooth(int xDir, int zDir, Vector3 end, SpeedTypes speed, Animator animator)
    {
        if (lockMove == 0)
        {
            TurnHead(xDir, zDir);
            StartCoroutine(SmoothMovement(end, speed, animator));
        }
    }

    protected virtual IEnumerator SmoothMovement(Vector3 end, SpeedTypes speed, Animator animator)
    {
        StartMove();
        float journeyLength = (float)Math.Round((transform.position - end).magnitude, 3);

        while (journeyLength > 0f)
        {
            Vector3 newPostion = Vector3.MoveTowards(rigidBody.position, end, (int)speed * Time.deltaTime);
            if (animator != null)
            {
                animator.SetFloat("Speed", (float)speed);
            }
            rigidBody.MovePosition(newPostion);
            journeyLength = (float)Math.Round((transform.position - end).magnitude, 3);
            yield return null;
        }
        StopMove();
        if (animator != null)
        {
            animator.SetFloat("Speed", 0);
        }
    }

    private void StartMove()
    {
        lockMove++;
    }

    private void StopMove()
    {
        lockMove--;
    }
}
