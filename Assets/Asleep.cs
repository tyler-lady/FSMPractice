using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Asleep : State
{
    public Asleep(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
        : base(_npc, _agent, _anim, _player)
    {
        name = STATE.ASLEEP;
        agent.isStopped = true;
    }

    public override void Enter()
    {
        anim.SetTrigger("isSleeping");
        base.Enter();
    }

    public override void Update()
    {
        if (CanAttackPlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (!CanSeePlayer())
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isSleeping");
        base.Exit();
    }
}
