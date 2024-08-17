using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/new Connection")]
public class LevelConnection : ScriptableObject
{
    public static LevelConnection ActiveConnection { get; set; }
}
