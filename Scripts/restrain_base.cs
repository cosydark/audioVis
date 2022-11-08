using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restrain_base : MonoBehaviour
{
    [HideInInspector] public bool isActive = true;
    Vector3 anchor;
    float ramp;
    void Start()
    {
        anchor = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            GetComponent<Rigidbody>().AddForce((anchor - transform.position).normalized * 50f, ForceMode.Force);
        }
    }
}
