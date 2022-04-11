using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
    {
    private static readonly int ANGLE = Shader.PropertyToID("_Angle");

    [SerializeField]
    private HingeJoint hinge = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = -hinge.angle;
        spriteRenderer.material.SetFloat(ANGLE, angle);
    }
}
