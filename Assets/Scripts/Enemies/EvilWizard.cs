using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : MonoBehaviour
{
    float hp = 500;
    int attCount = 0;
    GameObject player;
    Animator anim;
    bool hit;
    float meleeDmg1 = 1f; float meleeDmg2 = 2f;
    [SerializeField] float velocity;[SerializeField] float attRad;
    [SerializeField] LayerMask whatIsPlayer;[SerializeField] Transform spawnAttack;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 30)
        {
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) > 3)
            {
                if (!anim.GetBool("Attack1") && !anim.GetBool("Attack2") && hit == false)
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

            }
            if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= 3)
            {

                
                anim.SetBool("Move", false);
                anim.SetBool("Attack1", true);
                if (attCount == 3)
                {
                    anim.SetBool("Attack2", true);
                    
                }
            }
          
        }
        
    }

    void Attack1()
    {
        Collider2D collPlayer = Physics2D.OverlapCircle(spawnAttack.position, attRad, whatIsPlayer);
        if (collPlayer != null)
        {
            collPlayer.gameObject.GetComponent<Player>().TakeDmgPlayer(meleeDmg1);
            attCount++;
        }
    }

    void Attack2()
    {
        Collider2D collPlayer = Physics2D.OverlapCircle(spawnAttack.position, attRad, whatIsPlayer);
        if (collPlayer != null)
        {
            collPlayer.gameObject.GetComponent<Player>().TakeDmgPlayer(meleeDmg2);
        }
    }

    void EndAttack1()
    {
        anim.SetBool("Attack1", false);
        attCount++;
    }

    void EndAttack2()
    {
        anim.SetBool("Attack2", false);
        attCount = 0;
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

    public float GetDmgAtt1()
    {
        return meleeDmg1;
    }

    public float GetDmgAtt2()
    {
        return meleeDmg2;
    }
    public void TakeDmg(float dmg)
    {
        hit = true;
        anim.SetTrigger("Hit");
        hp -= dmg;
        Debug.Log(hp);
        if (hp <= 0)
        {
            anim.SetTrigger("Die");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(spawnAttack.position, attRad);
    }
}
