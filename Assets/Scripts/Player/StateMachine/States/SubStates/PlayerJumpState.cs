using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        _amountOfJumpsLeft = playerData.AmountOfJumps;
    }

    private int _amountOfJumpsLeft;

    public override void EnterState()
    {
        base.EnterState();

        if(AudioManager.Instance != null)
           AudioManager.Instance.PlaySFX(player.jumpAudio,player.transform,1f);
        
        DecreaseAmountOfJumpsLeft();

        player.UsedCoyoteTime = true;
        player.InAirState.SetIsJumpingTrue();
        player.SetVelocityY(playerData.JumpVelocity);
        isAbilityDone = true;
    }

    public bool CanJump()
    {
        if (_amountOfJumpsLeft > 0)
        {
            return true;
        }
        else return false; 
    }

    public void ResetJumpsLeft() => _amountOfJumpsLeft = playerData.AmountOfJumps;

    public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    public void IncreaseAmountOfJumpsLeft() => _amountOfJumpsLeft++;
}