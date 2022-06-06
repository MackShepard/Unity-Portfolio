using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectTool : MonoBehaviour
{
    public GameObject lastHit;
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, ray.direction, out hit, 1000))
            {
                lastHit = hit.transform.gameObject;
                EnemyAbstract enemyAbstract = lastHit.GetComponent<EnemyAbstract>();
                if (enemyAbstract != null)
                {
                    enemyAbstract.SomeAction();
                }
            }
        }
    }
}
