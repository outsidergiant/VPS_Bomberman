using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour {

    private Rigidbody rigidBody;

    ///protected SpeedTypes speed;

    private int lockMove;

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
            transform.eulerAngles = new Vector3(0, 180, 0);
        if (xDir == 1)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (zDir == -1)
            transform.eulerAngles = new Vector3(0, 90, 0);
        if (zDir == 1)
            transform.eulerAngles = new Vector3(0, -90, 0);
    }

    public virtual bool Move(int xDir, int zDir, out RaycastHit hit, SpeedTypes speed)
    {
        
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDir, 0f, zDir);
        Physics.Linecast(start, end, out hit);
        if (lockMove == 0) TurnHead(xDir, zDir);
        if (hit.transform == null)
        {
            if (lockMove == 0)
            {
                
                StartCoroutine(SmoothMovement(end, speed));
            }
            return true;
        }
        return false;
    }

    public void GoSmooth(int xDir, int zDir, SpeedTypes speed)
    {
        Vector3 end = transform.position + new Vector3(xDir, 0f, zDir);
        StartCoroutine(SmoothMovement(end, speed));
    }

    protected IEnumerator SmoothMovement(Vector3 end, SpeedTypes speed)
    {
        StartMove();
        float journeyLength = (float)Math.Round((transform.position - end).magnitude, 3);

        while (journeyLength > 0f)
        {
            Vector3 newPostion = Vector3.MoveTowards(rigidBody.position, end, (int)speed * Time.deltaTime);
            rigidBody.MovePosition(newPostion);
            journeyLength = (float)Math.Round((transform.position - end).magnitude, 3);
            yield return null;
        }
        StopMove();
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
