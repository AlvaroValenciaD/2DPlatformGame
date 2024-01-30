using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int newLvlIndex;
    [SerializeField] Vector3 newLvlPlayerSpawnPosition;

    public int GetNewLvlIndex()
    {
        return newLvlIndex;
    }

    public Vector3 GetnewLvlPlayerSpawnPosition()
    {
        return newLvlPlayerSpawnPosition;
    }
}
