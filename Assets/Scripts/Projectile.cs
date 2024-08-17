using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] public int strength;
    [SerializeField] private float speed = 5;

    [HideInInspector] public int direction;

    public Vector2 moveVector;
    
    void Start()
    {
        moveVector = new Vector2((float)direction, 0);
    }
    
    void Update()
    {
        transform.Translate(moveVector * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("proj collision");
        Destroy(gameObject);
    }
}
