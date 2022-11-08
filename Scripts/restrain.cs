using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restrain : MonoBehaviour
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
            ramp = 1f - Mathf.Clamp(transform.parent.GetComponent<scaleGroup>().oldScale, 0f, 5f) / 5f;
            GetComponent<Rigidbody>().AddForce((anchor - transform.position).normalized * 50f * ramp, ForceMode.Force);
        }
    }
}
