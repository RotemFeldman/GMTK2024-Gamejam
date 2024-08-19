using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("animations")] 
    [SerializeField] private AnimatorOverrideController level1;
    [SerializeField] private AnimatorOverrideController level2;
    [SerializeField] private AnimatorOverrideController level3;
    [SerializeField] private AnimatorOverrideController level4;

    [Header("Sounds")]
    [Header("Impact")] 
    public AudioClip smallImpact;
    public AudioClip midImpact;
    public AudioClip biglImpact;
    public AudioClip hugelImpact;
    public float ImpactVolume=1;

    [Header("shoot")] 
    public AudioClip[] smallShoot;
    public AudioClip[] midShoot;
    public AudioClip[] bigShoot;
    public AudioClip[] hugeShoot;
    public float ShootVolume = 1;

    private AudioClip[] _currentClips;
    private AudioClip _currentImpact;
    private static int _count = 0 ;
    
    
    
    [Header("other")]

    [SerializeField] public int strength;
    [SerializeField] private float speed = 5;
    public Animator animator;

    [HideInInspector] public int direction;

    public Vector2 moveVector;
    
    void Start()
    {
        moveVector = new Vector2(direction, 0);
        animator = GetComponent<Animator>();

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        if (Physics2D.OverlapCircle(pos, 0.5f, 6))
        {
            Hit();
        }
    }
    
    void Update()
    {
        transform.Translate(moveVector * (speed * Time.deltaTime));
    }

    public void SetDirection(int dir)
    {
        if (dir < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void ChangeLevel(int level)
    {
        switch (level)
        {   
            case 1:
                animator.runtimeAnimatorController = level1;
                strength = 0;
                _currentClips = smallShoot;
                _currentImpact = smallImpact;
                break;
            case 2:
                animator.runtimeAnimatorController = level2;
                strength = 1;
                _currentClips = midShoot;
                _currentImpact = midImpact;
                break;
            case 3:
                animator.runtimeAnimatorController = level3;
                strength = 2;
                _currentClips = bigShoot;
                _currentImpact = biglImpact;
                break;
            case 4:
                animator.runtimeAnimatorController = level4;
                strength = 4;
                _currentClips = hugeShoot;
                _currentImpact = hugelImpact;
                break;
            
        }
        
        PlayNextInSequence();
    }

    private void PlayNextInSequence()
    {
        
        if (_count >= _currentClips.Length)
            _count = 0;
        Debug.Log(_currentClips[_count]);
        
        AudioManager.Instance.PlaySFX(_currentClips[_count],ShootVolume);
        _count++;
    }

    //private bool _collided;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if((other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Projectile")))
            return;
        
        Hit();
    }

    void Hit()
    {
        AudioManager.Instance.PlaySFX(_currentImpact,ImpactVolume);
                animator.SetTrigger("Impact");
                speed = 0;
                GetComponent<Collider2D>().enabled = false;
                
                Debug.Log("proj collision");
                Destroy(gameObject,1);
    }
}
