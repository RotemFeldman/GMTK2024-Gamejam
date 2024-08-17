using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    protected bool _isAnimationFinished;
    protected bool _isExitingState;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    public virtual void EnterState()
    {       
        DoChecks();
        player.Animator.SetBool(animBoolName, true);
        startTime = Time.time;

        //Debug.Log("entered state" + GetType().Name);

        _isAnimationFinished = false;
        _isExitingState = false;
    }
    
    public virtual void ExitState()
    {
        player.Animator.SetBool(animBoolName, false);
        _isExitingState = true;
    }

    public virtual void LogicUpdate()
    {
    
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks() { }
   
    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() 
    {
        _isAnimationFinished = true;
    }
}