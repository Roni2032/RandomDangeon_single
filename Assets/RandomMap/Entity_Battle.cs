using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Battle : MonoBehaviour
{
    public float attack;
    public float hp;
    public float defence;

    public virtual void Damage(float amount)
    {
        hp -= amount;
        if(hp < 0)
        {
            hp = 0;
            Die();
        }
    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
