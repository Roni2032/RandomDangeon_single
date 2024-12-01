using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    float attack;
    [SerializeField]
    float hp;
    [SerializeField]
    float defence;

    public float GetAttack()
    {
        return attack;
    }
    public float GetHp()
    {
        return hp;
    }
    public float GetDefence()
    {
        return defence;
    }
    public virtual void Damage(float amount)
    {
        Debug.Log(this.gameObject.name + "‚ª" +  amount + "‚Ìƒ_ƒ[ƒW‚ğó‚¯‚Ü‚µ‚½");
        hp -= amount;
        if(hp <= 0)
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
