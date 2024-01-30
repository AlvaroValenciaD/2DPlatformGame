using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExample : MonoBehaviour
{
    [SerializeField] Transform patrolPath; [SerializeField] float speed;
    Transform[] places; Vector3 destiny; int destinyIndex = 0;

    [Header("Combat")]
    [SerializeField] float dmgEnemy, hp;
    void Start()
    {
        places = new Transform[patrolPath.childCount];

        for (int i = 0; i < places.Length; i++)
        {
            places[i] = patrolPath.GetChild(i);
        }

        destiny = places[destinyIndex].position;

        StartCoroutine(MoveAndWait());
    }

    
    void Update()
    {
        
    }

    IEnumerator MoveAndWait()
    {
        while (true)
        {
            while (transform.position != destiny)
            {
                transform.position = Vector3.MoveTowards(transform.position, destiny, speed * Time.deltaTime);
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            }

            FindNewPlace();
        }
        
    }

    void FindNewPlace()
    {
        destinyIndex++;

        if (destinyIndex >= places.Length)
        {
            destinyIndex = 0;
        }

        destiny = places[destinyIndex].position;

        
        // Esto sirve para rotar el pivote del enemigo y que el movimiento siga siendo con x positiva
        float diffX = transform.position.x - destiny.x;

        if (diffX > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void TakeDmg (float dmg)
    {
        hp -= dmg;
        if (hp<=0)
        {
            Destroy(gameObject);
        }
        
    }
}
