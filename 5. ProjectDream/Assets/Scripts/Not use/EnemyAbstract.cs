using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    public int hp;
    public int speed;

    public abstract void SomeAction();
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
