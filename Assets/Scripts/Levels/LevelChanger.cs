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
    

    private void Start()
    {
        FindObjectOfType<Player>().transform.position = spawnPoint.position;
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
