using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


    [CreateAssetMenu(menuName = "Scriptable Tile",fileName = "new Scriptable Tile",order = 0)]
    public class ScriptableTile : TileBase
    {
        [SerializeField] public int strength;


        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
        {
            return base.StartUp(position, tilemap, go);
        }

        
    }

