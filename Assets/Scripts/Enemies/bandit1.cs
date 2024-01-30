using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bandit1 : MonoBehaviour
{
    float hp = 100;
    GameObject player;
    Animator anim;
    bool hit;
    float meleeDmg = 1f;
    [SerializeField] float velocity;[SerializeField] float attRad;
    [SerializeField] LayerMask whatIsPlayer;[SerializeField] Transform spawnAttack;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 20)
        {
            anim.SetBool("IDLEcombat", true);

            if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 10)
            {

             if (Vector3.Distance(player.transform.position, gameObject.transform.position) > 2)
             {
                if (!anim.GetBool("Attack") && hit == false)
                {
                    if (player.transform.position.x < transform.position.x)
                    {
                        transform.localScale = new Vector3(1, 1, 1);
                        anim.SetBool("Move", true);
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), velocity * Time.deltaTime);
                    }
                    if (player.transform.position.x > transform.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                        anim.SetBool("Move", true);
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y, transform.position.z), velocity * Time.deltaTime);
                    }
                }
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 2)
                {
                anim.SetBool("Move", false);
                anim.SetBool("Attack", true);

                }

             }
                
            }
            
        }
        

    }

    void Attack()
    {
        Collider2D collPlayer = Physics2D.OverlapCircle(spawnAttack.position, attRad, whatIsPlayer);
        if (collPlayer != null)
        {
            collPlayer.gameObject.GetComponent<Player>().TakeDmgPlayer(meleeDmg);
        }
    }
    void EndAttack()
    {
        anim.SetBool("Attack", false);
    }

    void HitSwitch()
    {
        hit = false;
        anim.SetTrigger("BackIdle");
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    public void TakeDmg(float dmg)
    {
        hit = true;
        anim.SetTrigger("Hurt");
        hp -= dmg;
        Debug.Log(hp);
        if (hp <= 0)
        {
            anim.SetTrigger("Death");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(spawnAttack.position, attRad);
    }
}

