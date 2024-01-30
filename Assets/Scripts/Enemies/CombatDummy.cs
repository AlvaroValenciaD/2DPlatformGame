using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummy : MonoBehaviour
{
    float hp = 100;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void TakeDmg(float dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

    }

}
