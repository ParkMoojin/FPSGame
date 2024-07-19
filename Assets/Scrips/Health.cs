using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float hp = 10;
    public IHealthListener healthListener;

    // Start is called before the first frame update
    void Start()
    {
        healthListener = GetComponent<Health.IHealthListener>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage( float damage)
    {
        if (hp>0)
        {
            hp -= damage;

            if (hp <= 0)
            {
                if (healthListener != null)
                {
                    healthListener.Die();
                }
            }
        }
    }

    public interface IHealthListener
    {
        void Die();
    }

}
