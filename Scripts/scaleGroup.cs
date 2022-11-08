using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleGroup : MonoBehaviour
{
    public int sequence;
    [HideInInspector] public float oldScale;
    Av_getData data;
    float scale;
    float axisScale;
    void Start()
    {
        data = transform.parent.GetComponent<Av_getData>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<restrain>();
            axisScale = Vector3.Dot(data.scaleAxis[sequence].position.normalized, transform.GetChild(i).transform.position.normalized);
        }
    }
    void Update()
    {
        scale = data.ASsample_8_smoothed[sequence];
        oldScale = data.ASsample_8_smoothed[sequence];
        //
        scale = Mathf.Clamp(scale, 0f, 2f) * 1.5f + 1f;
        //scale += 0.0001f;
        //
        for (int i = 0; i < transform.childCount; i++)
        {
            //axisScale = Vector3.Dot(data.scaleAxis[sequence].position.normalized, transform.GetChild(i).transform.position.normalized);
            if(data.enableAxis)
            {
                scale *= axisScale;
                //Debug.Log(axisScale);
            }
            transform.GetChild(i).transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
