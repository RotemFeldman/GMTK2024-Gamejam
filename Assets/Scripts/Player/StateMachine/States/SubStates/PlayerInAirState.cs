using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    

    
    private float _coyoteTime;
    private bool _isGrounded;
    private Vector2 _input;
    private bool _jumpInput;
    private bool _jumpInputStop;
    private bool _grabInput;
    private bool _isJumping;
    private bool _isTouchingWall;
    

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = player.CheckIfGrounded();
    }

    public override void EnterState()
    {
        base.EnterState();


    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CoyoteTimeTimer();
        

        _jumpInput = player.InputHandler.JumpInput;
        _jumpInputStop = player.InputHandler.JumpInputStop;
        _input.x = player.InputHandler.NormInputX;
        _grabInput = player.InputHandler.GrabInput;

        CheckJumpMulti();

        if (_isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        //Coyote Time
        else if (_coyoteTime > 0 && !player.UsedCoyoteTime)
        {
            if (_jumpInput)
            {
                player.UsedCoyoteTime = true;
                player.JumpState.IncreaseAmountOfJumpsLeft();
                player.InputHandler.UseJumpInput();
                stateMachine.ChangeState(player.JumpState);
            }
        }
        else if (player._playerData.CanMultiJump && _jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJumpInput();
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            player.CheckIfShouldFlip(_input.x);
            player.SetVelocityX(playerData.MovementVelocity * _input.x);

            player.Animator.SetFloat("Velocity Y", player.CurrentVelocity.y);
            player.Animator.SetFloat("Velocity X", Mathf.Abs(player.CurrentVelocity.x));
        }

        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        var pVel = player.RB.velocity;
        if (pVel.y < 0)
        {
            pVel.y -= playerData.GravityMult * Time.deltaTime;
        }
    }

    public void SetIsJumpingTrue()
    {
        _isJumping = true;
    }

    private void CoyoteTimeTimer()
    {
        if (!_isGrounded)
        {
            _coyoteTime -= Time.deltaTime;
        }
        else
        {
            _coyoteTime = player._playerData.CoyoteTime;
        }

    }

    private void CheckJumpMulti()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                player.SetVelocityY(player.CurrentVelocity.y * player._playerData.JumpHeightMulti);
                _isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }

    
}