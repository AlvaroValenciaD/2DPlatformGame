using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ranged_Attack : MonoBehaviour
{
    float xDireccion, attRad, dmg; [SerializeField] LayerMask whatIsDmgble;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (xDireccion >= 0)
        {
            transform.Translate(new Vector3(xDireccion, 0, 0) * 10 * Time.deltaTime);
        }
        if (xDireccion < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(new Vector3(xDireccion, 0, 0) * 10 * Time.deltaTime);
        }
        Damage();
        
    }

    public void SetXDirection(float xDirection)
    {
        xDireccion = xDirection;
    }
    public void SetRangedDmg(float dmgRanged)
    {
        dmg = dmgRanged;
    }

    void Damage()
    {
        Collider2D collEnemy = Physics2D.OverlapCircle(gameObject.transform.position, attRad, whatIsDmgble);
        if (collEnemy != null && collEnemy.CompareTag("Dummy"))
        {
            collEnemy.gameObject.GetComponent<CombatDummy>().TakeDmg(dmg);
            Destroy(gameObject);
        }
        if (collEnemy != null && collEnemy.CompareTag("EvilWizard"))
        {
            collEnemy.gameObject.GetComponent<EvilWizard>().TakeDmg(dmg);
            Destroy(gameObject);
        }
        if (collEnemy != null && collEnemy.CompareTag("Bandit1"))
        {
            collEnemy.gameObject.GetComponent<bandit1>().TakeDmg(dmg);
            Destroy(gameObject);
        }
    }
}
