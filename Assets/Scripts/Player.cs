using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float h, jumpCounter; Rigidbody2D rb; Animator anim; int rngDir; float playerDmg; float timer = 0, shootRatio = 0.5f;


    [Header("Movement System")]
    [SerializeField] float moveSpeed; [SerializeField] float jumpSpeed; [SerializeField] float maxSpeed; [SerializeField] float detectionRad;
    [SerializeField] float maxJumps;[SerializeField] Transform feet; [SerializeField] LayerMask whatIsFloor;
    [Header("Combat")]
    [SerializeField] float rangeDmg; [SerializeField] float meleeDmg; [SerializeField] float hp; [SerializeField] float attRad;
    [SerializeField] LayerMask whatIsDmgble; [SerializeField] Transform spawnAttack; [SerializeField] GameObject attackPrefab, spawnpoint;
    [Header("Interactuar")]
    [SerializeField] LayerMask whatIsInteract; [SerializeField] float radiusInt; [SerializeField] GameObject interactPoint;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rngDir = 1;
        jumpCounter = maxJumps;
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Death();
        FloorDetective();
        timer += Time.deltaTime;

        if (!anim.GetBool("Attack") && !anim.GetBool("Attack_Ranged") && hp > 0)
        {
            AnimationMove();
            if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0)
            {
                ResetJump();
                Jump();
            }

        }

        if (FloorDetective() == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                h = 0;
                anim.SetBool("Attack", true);
            }
            if (Input.GetMouseButton(1) && timer > shootRatio)
            {
                h = 0;
                anim.SetBool("Attack_Ranged", true);
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(h, 0, 0) * moveSpeed, ForceMode2D.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    bool FloorDetective()
    {
       Collider2D coll = Physics2D.OverlapCircle(feet.position, detectionRad, whatIsFloor);

        if (coll != null)
        {
            if (rb.velocity.y <= 0)
            {
                jumpCounter = maxJumps;
                anim.SetBool("Fall", false);
            }
            return true;
        }
        else
        {
            if (rb.velocity.y <= 0)
            {
                anim.SetBool("Fall", true);
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(feet.position, detectionRad);
        Gizmos.DrawSphere(spawnAttack.position, attRad);
    }

    void AnimationMove()
    {
        h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            anim.SetBool("IDLE 2 Run", true);
            transform.localScale = Vector3.one;
            rngDir = 1;
        }
        else if (h < 0)
        {
            anim.SetBool("IDLE 2 Run", true);
            transform.localScale = new Vector3(-1,1,1);
            rngDir = -1;
        }
        else
        {
            anim.SetBool("IDLE 2 Run", false);
        }
    }

    void Interact()
    {
        Collider2D collTouch = Physics2D.OverlapCircle(interactPoint.transform.position, radiusInt, whatIsInteract);
        if (collTouch !=null)
        {
            Debug.Log(collTouch.gameObject.name);
            if (collTouch.gameObject.CompareTag("NewLvlDoor"))
            {
                Door doorScript = collTouch.gameObject.GetComponent<Door>();
                SceneManager.LoadScene(doorScript.GetNewLvlIndex());
            }
        }
    }
    void Jump()
    {
        rb.AddForce(new Vector3(0, 1, 0) * jumpSpeed, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
        anim.SetBool("Attack", false);
        jumpCounter--;
    }
    void ResetJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        anim.SetBool("Fall", false);
    }
    void Attack()
    {
        Collider2D collEnemy = Physics2D.OverlapCircle(spawnAttack.position, attRad, whatIsDmgble);
        if (collEnemy != null && collEnemy.CompareTag("Dummy"))
        {
            collEnemy.gameObject.GetComponent<CombatDummy>().TakeDmg(meleeDmg);
        }
        if (collEnemy != null && collEnemy.CompareTag("EvilWizard"))
        {
            collEnemy.gameObject.GetComponent<EvilWizard>().TakeDmg(meleeDmg);
        }
        if (collEnemy != null && collEnemy.CompareTag("Bandit1"))
        {
            collEnemy.gameObject.GetComponent<bandit1>().TakeDmg(meleeDmg);
        }
    }
    void EndAttack()
    {
        anim.SetBool("Attack", false);
    }
    void Shoot()
    {
        GameObject copy = Instantiate(attackPrefab, spawnpoint.transform.position, Quaternion.identity);
        copy.GetComponent<Player_Ranged_Attack>().SetXDirection(rngDir);
        copy.GetComponent<Player_Ranged_Attack>().SetRangedDmg(rangeDmg);
        Destroy(copy, 3);
    }
    void EndShoot()
    {
        anim.SetBool("Attack_Ranged", false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EvilWizard"))
        {
            Hit();
            hp--;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            ContactPoint2D contactPoint = collision.GetContact(0);
            Vector2 planeNormal = contactPoint.normal;
            float dot = Vector3.Dot(planeNormal, Vector3.up);
            if (dot != 1 && FloorDetective())
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 4;
            }
        }
    }

    void Death()
    {
        if (hp <= 0)
        {
            anim.SetTrigger("Death");
        }
    }

    void Hit()
    {
        anim.SetTrigger("Hit");
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.gravityScale = 4;
        }
    }

    public float GetHp()
    {
        return hp;
    }

    public float GetRangedDmg()
    {
        return rangeDmg;
    }

    public int RangedDirection()
    {
        return rngDir;
    }

    public void TakeDmgPlayer(float playerDmg)
    {
        Hit();
        hp -= playerDmg;
        Debug.Log(hp);
    }
}
