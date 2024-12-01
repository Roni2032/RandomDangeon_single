using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    Entity enity;
    public enum Effect
    {

    }
    List<Effect> effects;
    
    Vector3 target;
    [SerializeField]
    bool isExplode;

    public float destroyTime;
    float destroyTimer;

    private void Start()
    {
        OnStart();
    }
    private void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > destroyTime)
        {
            Destroy(this.gameObject);
        }
        OnUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity enemy = collision.gameObject.GetComponent<Entity>();
        if(enemy != null)
        {
            Damage(enemy);
        }
        OnEnter(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnStay(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit(collision);
    }
    public virtual void OnStart(){ }
    public virtual void OnUpdate(){ }
    public virtual void OnEnter(Collider2D collision) { }
    public virtual void OnStay(Collider2D collision) { }
    public virtual void OnExit(Collider2D collision) { }
    public void Damage(Entity enemy)
    {
        float entityAttack = 10;
        float enemyDefence = enemy.GetDefence();

        float damage = entityAttack - enemyDefence;
        if (damage < 0)
        {
            damage = 0;
        }
        enemy.Damage(damage);
    }
    public Vector3 GetTarget()
    {
        return target;
    }
    public void SetTarget(Vector3 pos)
    {
        target = pos;
    }
    public void SetTarget(GameObject obj)
    {
        target = obj.transform.position;
    }
    public void SetExplode(bool flag)
    {
        isExplode = flag;
    }
    public bool GetExplode()
    {
        return isExplode;
    }
    public void StraightMove()
    {

    }

}
