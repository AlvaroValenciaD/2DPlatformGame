using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float hpPlayerStoraged; int moneyPlayerStoraged;
    public static GameManager gM;
    void Awake()
    {
        if (gM == null)
        {
            gM = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    
   public void Storage(float hp, int money)
    {
        hpPlayerStoraged = hp;
        moneyPlayerStoraged = money;
    }
    public float LoadHp()
    {
        return hpPlayerStoraged;
    }

    public int LoadMoney()
    {
        return moneyPlayerStoraged;
    }
}
