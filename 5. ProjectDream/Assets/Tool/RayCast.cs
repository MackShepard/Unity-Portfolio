using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour // �������
{
    //������ �������
    public GameObject lastHit;
    public Vector3 collision = Vector3.zero;
    public LayerMask layer;
    public float rayLenghth;
    [SerializeField]
    public PlayerMovement character;
    Vector3 lookDirection;

    void LateUpdate()
    {
        if (character.Movement.normalized != Vector3.zero)
            lookDirection = character.Movement.normalized;

        var ray = new Ray(this.transform.position, lookDirection);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLenghth))
        {
            lastHit = hit.transform.gameObject;
            collision = hit.point; // ����� ��� ��������� ���������������
        } else lastHit = null;

    }

    private void OnDrawGizmos() // ������������ �� ����� ���� � ����� ��� �������� ��������
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(collision, 0.2f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, character.Movement.normalized * rayLenghth);
    }
}
