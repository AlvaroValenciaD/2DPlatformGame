using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepasoExamen : MonoBehaviour
{
    GameObject player;
    [SerializeField] float velocity;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, velocity * Time.deltaTime);   
        }
    }

  
}
