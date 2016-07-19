using UnityEngine;

public class CObject : PoolObject
{
    public int hp;
    public float damage;
    public float moveSpeed;

    protected int _hp;
    protected float _damage;
    protected float _moveSpeed;

    public override void OnObjectReuse()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        _moveSpeed = moveSpeed;
        _hp = hp;
        _damage = damage;
    }

    public virtual float OutgoingDamage()
    {
        return _damage;
    }

    public virtual void IncomingDamage(int incomingDamage) {
        _hp -= incomingDamage;
        Debug.Log(gameObject.name + ": " + _hp);
        if(_hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.GetComponent<Collider>().enabled = false; // Отключаем коллайдер, во избежание набора лишних очков
        _moveSpeed = 0;
        Invoke("Destroy", 0.5f);
    }
}
