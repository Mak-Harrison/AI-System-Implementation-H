using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAI : MonoBehaviour
{
    public enum State { Idle, Chase, Return }
    public State currentState = State.Idle;

    public Transform player;
    public float visionRange = 10f;
    public float hearingRange = 5f;
    public float moveSpeed = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                LookForPlayer();
                break;
            case State.Chase:
                ChasePlayer();
                break;
            case State.Return:
                ReturnToStart();
                break;
        }
    }

    void LookForPlayer()
    {
        Vector3 dir = player.position - transform.position;

        // Vision (Raycast)
        if (Vector3.Distance(player.position, transform.position) <= visionRange)
        {
            if (Physics.Raycast(transform.position + Vector3.up, dir.normalized, out RaycastHit hit, visionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    SetState(State.Chase);
                }
            }
        }

        // Hearing (Proximity)
        if (Vector3.Distance(player.position, transform.position) <= hearingRange)
        {
            SetState(State.Chase);
        }
    }

    void ChasePlayer()
    {
        MoveTowards(player.position);

        if (Vector3.Distance(transform.position, player.position) > visionRange * 1.5f)
        {
            SetState(State.Return);
        }
    }

    void ReturnToStart()
    {
        MoveTowards(startPos);

        if (Vector3.Distance(transform.position, startPos) < 0.5f)
        {
            SetState(State.Idle);
        }
    }

    void MoveTowards(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
    }

    void SetState(State newState)
    {
        currentState = newState;
    }
}
