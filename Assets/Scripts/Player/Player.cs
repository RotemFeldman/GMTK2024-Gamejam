using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("sizes")]
    [SerializeField] private Sprite[] playerSprites;

    public SpriteRenderer renderer;
    public BoxCollider2D collider;
    public int projStrengh;

    public void ChangeSize(int size)
    {

        var changeVec = new Vector2(0.2f, 0.2f);
        switch (size)
        {
            case 1:
                renderer.sprite = playerSprites[0];
                collider.size = renderer.sprite.bounds.size;
                collider.size -= changeVec;
               // collider.offset = new Vector2(playerSprites[0].bounds.size.x / 2, 0);
                break;
            case 2:
                renderer.sprite = playerSprites[1];
                collider.size = renderer.sprite.bounds.size;
                collider.size -= changeVec;
               // collider.offset = new Vector2(playerSprites[1].bounds.size.x / 2, 0);
                break;
            case 3:
                renderer.sprite = playerSprites[2];
                collider.size = renderer.sprite.bounds.size;
                collider.size -= changeVec;
              //  collider.offset = new Vector2(playerSprites[2].bounds.size.x / 2, 0);
                break;
                    
        }
    }
    
    
    
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunningState RunningState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }

    [Header("StateMachine")]
    [SerializeField] public PlayerData _playerData;

    public bool UsedCoyoteTime { get; set; }


    #endregion

    #region Components
    public Animator Animator { get; private set; }
    [SerializeField] public PlayerInputHandler InputHandler; //{ get; private set; }
    public Rigidbody2D RB { get; private set; }

    public GameObject Projectile;
    public Transform ProjSpawn;
    #endregion

    #region Check Transforms

    [SerializeField] Transform _groundCheck;
    [SerializeField] Transform _groundCheck2;

    #endregion

    #region Other Variables
    public Vector2 CurrentVelocity { get; private set; }

    public int FacingDirection = 1;

    private Vector2 _workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, _playerData, "idle");
        RunningState = new PlayerRunningState(this, StateMachine, _playerData, "running");
        JumpState = new PlayerJumpState(this, StateMachine, _playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, _playerData, "inAir");
        
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
        //InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        
        Debug.Log(StateMachine.CurrentState);


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSize(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSize(2);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSize(3);
        }
        
        
    }

    private void FixedUpdate()
    {
        
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void OnEnable()
    {
        InputHandler.onShoot.AddListener(Shoot);
    }



    private void OnDisable()
    {
        InputHandler.onShoot.RemoveListener(Shoot);
    }

    #endregion

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        _workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }
    
    public void SetVelocityY(float velocity)
    {
        _workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = _workspace;
        CurrentVelocity = _workspace;
    }

    

    #endregion

    #region Check Functions
    public void CheckIfShouldFlip(float xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        { Flip(); }
    }

    public bool CheckIfGrounded()
    {
        return (Physics2D.OverlapCircle(_groundCheck.position, _playerData.GroundCheckRadius, _playerData.WhatIsGround));
    }
    

    #endregion

    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Shoot()
    {
        Debug.Log("shoot");
        var pr = Instantiate(Projectile, ProjSpawn.position, quaternion.identity);
        pr.GetComponent<Projectile>().direction = FacingDirection;
    }
    
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
}