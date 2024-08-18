using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{

    [SerializeField] private int ID;
    [SerializeField] private AnimationClip explosion;
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (GameManager.Instance.PickupsID.Contains(ID))
        {
            gameObject.SetActive(false);
        }
        
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(this.name + "collision");
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.PickupsID.Add(ID);
            Debug.Log("isPlayer");
            animator.SetTrigger("Explosion");
            //PlayParticle(transform.position);


            StartCoroutine(AsyncDisactivate());


        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("is player");
            tilemap.SetTile(tilemap.WorldToCell(other.transform.position), null);
            PlayParticle(tilemap.WorldToCell(other.transform.position));
        }
    }*/

    IEnumerator AsyncDisactivate()
    {
      yield return  new WaitForSeconds(1);
      gameObject.SetActive(false);
    }
    
}
