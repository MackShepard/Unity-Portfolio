using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : EnemyAbstract
{
    public override void SomeAction()
    {
        print(gameObject.name);
    }
}
