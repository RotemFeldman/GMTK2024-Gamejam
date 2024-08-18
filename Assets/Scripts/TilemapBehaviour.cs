using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBehaviour : MonoBehaviour
{
   // [SerializeField] private ScriptableTile tile;

    [SerializeField] public Tilemap tilemap;
    [SerializeField] private int strength;
    [SerializeField] private AudioClip BreakSound;
    public float BreakVolume =1;
    [SerializeField] private AudioClip HitSound;
    public float HitVolume =1;

    public bool IsPlant;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(this.name + "collision");
        if (other.gameObject.CompareTag("Projectile"))
        {
            var pr = other.gameObject.GetComponent<Projectile>();

            if (CanDestroyTile(pr.strength))
            {
                Vector3 hitPosition = Vector3.zero;
                foreach (ContactPoint2D hit in other.contacts)
                {
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                    AudioManager.Instance.PlaySFX(BreakSound,BreakVolume);

                    if (IsPlant)
                    {
                        hitPosition.y++;
                        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                        hitPosition.y -= 2;
                        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                    }
                }
            }
            else
            {
                AudioManager.Instance.PlaySFX(HitSound,HitVolume);
            }
        }
        
    }
    
    
    
    

    

    private bool CanDestroyTile(int inputStrength)
    {
        return inputStrength >= strength;
    }
}
