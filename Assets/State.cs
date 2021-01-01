using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    
    public enum STATE
    {
        IDLE, PATROL, PURSUE, ATTACK, ASLEEP, FLEE
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name; //used to store the state's name
    protected EVENT stage; //used to represent the current stage of the state
    protected GameObject npc;
    protected Animator anim; //we will be using an animator
    protected Transform player; //transform for the player so that the guard can know where the player is
    protected State nextState;
    protected NavMeshAgent agent; 

    float visDist = 10.0f; //distance that the npc will begin to see the player
    float visAngle = 30.0f; //if player is in this 30degree angle, or view cone
    float shootDist = 7.0f; //range of guard's weapon, or shoot ability

    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
    }

    //next is our skeleton code for our different stages, or phases, that our states go thru
    public virtual void Enter() { stage = EVENT.UPDATE; } //this sets the stage for whatever is going to next
    public virtual void Update() { stage = EVENT.UPDATE; } //we want to stay in update until update kicks us out
    public virtual void Exit() { stage = EVENT.EXIT; } //tells us what to run and cleanup

    //incredibly important part of managing our states. maintaining this process and being able to transition
    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter(); //captures the stage, and calls the appropriate method
        }
        if (stage == EVENT.UPDATE)
        {
            Update(); //captures the stage, and calls the appropriate method
        }
        if (stage == EVENT.EXIT)
        {
            Exit(); //captures the stage, and calls the appropriate method
            return nextState; //when exiting our state, we want to begin the transition to our nextState
        }
        return this; //this maintains us returning the same state to keep us moving along states
    }

    //the following method is utilized in recognizing the player
    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position = npc.transform.position;
        if (direction.magnitude < shootDist)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        if (direction.magnitude < 2 && angle < 30)
        {
            return true;
        }
        return false;
    }

}

