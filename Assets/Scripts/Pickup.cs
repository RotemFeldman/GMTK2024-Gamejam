using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour
{
    [SerializeField] private ParticleSystem pickupAnimation;


    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(this.name + "collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("isPlayer");
            PlayParticle(transform.position);
            Destroy(gameObject,1f);
            

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

    private void PlayParticle(Vector3 pos)
    {
        var p = Instantiate(pickupAnimation, pos, Quaternion.identity);
        p.Play();
    }
    
}
