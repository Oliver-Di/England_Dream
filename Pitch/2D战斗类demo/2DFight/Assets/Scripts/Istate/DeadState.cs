using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private FSM manager;
    private Parameter parameter;

    public DeadState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        parameter.anim.Play("dead");
    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
