using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        print((x, y, z * Time.time));
        transform.rotation = Quaternion.Euler(x,y,z * Time.time); // b


    }

}
