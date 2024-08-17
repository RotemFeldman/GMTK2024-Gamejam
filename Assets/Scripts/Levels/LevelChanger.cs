using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private LevelConnection connection;

    [SerializeField] private string targetSceneName;
    
    [SerializeField] private Transform spawnPoint;

    public bool addImpulse;
    public float impulseForce;
    

    [ContextMenu("spawn")]
    private void Start()
    {
        var p = FindObjectOfType<Player>();
            p.transform.position = spawnPoint.position;
        if (addImpulse)
        {
            var rb = p.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(0, impulseForce);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            LevelConnection.ActiveConnection = connection;
            SceneManager.LoadScene(targetSceneName);
        }
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position,1);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPoint.position,0.2f);
    }

    
}
