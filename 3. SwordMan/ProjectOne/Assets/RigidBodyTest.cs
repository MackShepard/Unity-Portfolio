using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyTest : MonoBehaviour
{
    Rigidbody2D _rb;
    public Vector2 x;
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb.OverlapPoint(x))
            print("перекрывает");
    }

    private void Update()
    {
        
    }
}
