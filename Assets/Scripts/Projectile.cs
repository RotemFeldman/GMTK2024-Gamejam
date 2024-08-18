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
    //[SerializeField] private AnimatorOverrideController level1;

    [SerializeField] public int strength;
    [SerializeField] private float speed = 5;
    public Animator animator;

    [HideInInspector] public int direction;

    public Vector2 moveVector;
    
    void Start()
    {
        moveVector = new Vector2(direction, 0);
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        transform.Translate(moveVector * (speed * Time.deltaTime));
    }

    public void ChangeLevel(int level)
    {
        switch (level)
        {   
            case 1:
                animator.runtimeAnimatorController = level1;
                strength = 0;
                break;
            case 2:
                animator.runtimeAnimatorController = level2;
                strength = 1;
                break;
            case 3:
                animator.runtimeAnimatorController = level3;
                strength = 2;
                break;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")||other.gameObject.CompareTag("Projectile"))
            return;
        animator.SetTrigger("Impact");
        speed = 0;
        GetComponent<Collider2D>().enabled = false;
        
        Debug.Log("proj collision");
        Destroy(gameObject,1);
    }
}
