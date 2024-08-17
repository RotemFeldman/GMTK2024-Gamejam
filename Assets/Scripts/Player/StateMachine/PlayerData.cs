using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Running State")]
    public float MovementVelocity = 10f;

    [Header("Jump State")]
    public float JumpVelocity = 15f;
    public int AmountOfJumps = 1;   

    [Header("InAir State")]
    public float CoyoteTime = 0.1f;
    public float JumpHeightMulti = 0.5f;
    public bool CanMultiJump = false;
    public int GravityMult = 20;
    

    [Header("Check Variables")]
    public float GroundCheckRadius;
    public LayerMask WhatIsGround;
    public float WallCheckDistance = 0.5f;
}