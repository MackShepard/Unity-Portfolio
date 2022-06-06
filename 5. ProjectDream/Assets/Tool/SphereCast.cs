using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCast : MonoBehaviour
{
    //убрать паблики
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    public GameObject currentHitObject;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;
    

    void Update()
    {
        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        if (Physics.SphereCast(origin, sphereRadius, Vector3.zero, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        } else { 
            currentHitObject = null;
            currentHitDistance = maxDistance;
        }

    }

    private void OnDrawGizmos() // прорисование на сцене луча и сферы для проверки рейкаста
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
