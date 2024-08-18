using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public GameObject level5;
    
    [Header("Animators")] 
    [SerializeField] private AnimatorOverrideController Level1Anim;
    [SerializeField] private AnimatorOverrideController Level2Anim;
    [SerializeField] private AnimatorOverrideController Level3Anim;
    [SerializeField] private AnimatorOverrideController Level4Anim;

    [Header("projectiles")] 
    public GameObject[] projSpawn1;
    public GameObject[] projSpawn2;
    public GameObject[] projSpawn3;
    private GameObject[] _currentProjSpawn;
    
    [Header("sizes")]
    [SerializeField] private Sprite[] playerSprites;

    [Header("Sounds")] 
    [SerializeField] public AudioClip jumpAudio;

    public float jumpVolume = 1;
    public AudioClip upgradeSound;
    public float upgradeVolume =1;
    public AudioClip[] walkSound;
    public float walkVolume = 1;
    
    [Header("other")]

    public SpriteRenderer renderer;
    public BoxCollider2D collider;
    public int projStrengh;

    public static Player Instance;
    public static int playerSize = 1;

    public void PlayRandomWalkSound()
    {
        int rnd = Random.Range(0, walkSound.Length);
        AudioManager.Instance.PlaySFX(walkSound[rnd],walkVolume);
    }

    private void EndSequence()
    {
        
    }

    public void ChangeSize(int size)
    {

        var changeVec = new Vector2(0.6f, 0.2f);
        switch (size)
        {
            case 1:
                renderer.sprite = playerSprites[0];
                collider.size = renderer.sprite.bounds.size;
                collider.size -= changeVec;
                collider.offset = new Vector2(0, 0);

                projStrengh = 0;
                _playerData.GravityMult = 20;
                _playerData.JumpVelocity = 22;
                Animator.runtimeAnimatorController = Level1Anim;
                _currentProjSpawn = projSpawn1;
                _playerData.GroundCheckRadius = 0.3f; 

               // collider.offset = new Vector2(playerSprites[0].bounds.size.x / 2, 0);
                break;
            case 2:
                renderer.sprite = playerSprites[1];
                collider.size = renderer.sprite.bounds.size;
                collider.size -= changeVec;
                collider.offset = new Vector2(0, 0);

                _playerData.GravityMult = 40;
                _playerData.JumpVelocity = 22;
                projStrengh = 1;
                Animator.runtimeAnimatorController = Level2Anim;
                _currentProjSpawn = projSpawn2;
                _playerData.GroundCheckRadius = 0.5f; 

               // collider.offset = new Vector2(playerSprites[1].bounds.size.x / 2, 0);
                break;
            case 3:
                renderer.sprite = playerSprites[2];
                collider.size = renderer.sprite.bounds.size;
                    collider.offset = new Vector2(0, 0);
                collider.size -= changeVec;
                projStrengh = 2;
                _playerData.GravityMult = 60;
                _playerData.JumpVelocity = 19;
                Animator.runtimeAnimatorController = Level3Anim;
                _currentProjSpawn = projSpawn3;
                _playerData.GroundCheckRadius = 0.5f; 

              //  collider.offset = new Vector2(playerSprites[2].bounds.size.x / 2, 0);
                break;
            case 4:
                renderer.sprite = playerSprites[3];
                collider.size = renderer.sprite.bounds.size;
                collider.size -= new Vector2(3.5f,4.5f);
                collider.offset = new Vector2(0, -0.25f);
                projStrengh = 4;  
                _playerData.GravityMult = 80;
                _playerData.JumpVelocity = 15;
                Animator.runtimeAnimatorController = Level4Anim;
                _currentProjSpawn = projSpawn3;
                _playerData.GroundCheckRadius = 1; 
                break;
        }
    }
    
    public void ChangeSizeUp()
    {
        playerSize++;
        
        ChangeSize(playerSize);
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
        if (Instance == null)
        {
            Instance = this;
        }
        
        
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
        ChangeSize(playerSize);
    }

    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.LogicUpdate();
        
        Debug.Log(StateMachine.CurrentState);


        HotKey();
        
        
    }

    
    #if UNITY_EDITOR
    void HotKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerSize = 1;
            ChangeSize(playerSize);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerSize = 2;
            ChangeSize(playerSize);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerSize = 3;
            ChangeSize(playerSize);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerSize = 4;
            ChangeSize(playerSize);
        }
    }
    #endif

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            AudioManager.Instance.PlaySFX(upgradeSound,upgradeVolume);
            ChangeSizeUp();
        }
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

        foreach (var proj in _currentProjSpawn)
        {
            var pr = Instantiate(Projectile, proj.transform.position, quaternion.identity);
            var sc = pr.GetComponent<Projectile>();
            sc.direction = FacingDirection;
            sc.ChangeLevel(playerSize);
        }
        
        
            
        Animator.SetTrigger("shoot");
    }
    
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
}