using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected Vector2 _input;

    private bool _jumpInput;
    private bool _grabInput;
    private bool _isGrounded;
    private bool _isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();

    }
    
    public override void EnterState()
    {
        base.EnterState();

        player.UsedCoyoteTime = false;
        player.JumpState.ResetJumpsLeft();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _input.x = player.InputHandler.NormInputX;
        _jumpInput = player.InputHandler.JumpInput;
        
        if (_jumpInput && _isGrounded && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!_isGrounded) 
        {
            player.InputHandler.UseJumpInput();
            player.JumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    
}