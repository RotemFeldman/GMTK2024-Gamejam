using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        PreviousState = startingState;
        CurrentState.EnterState();
    }

    public void ChangeState(PlayerState newState)
    {
        PreviousState = CurrentState;
        CurrentState.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }
}