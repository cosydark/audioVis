using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillarCreater : MonoBehaviour
{
    public GameObject egg;
    public Transform eggPos;
    public Av_getData data;
    GameObject[] Cube = new GameObject[512];
    public float scale;
    void Start()
    {
        for (int i = 0; i < 512; i++)
        {
            Cube[i] = Instantiate(egg);
            Cube[i].transform.SetParent(transform.parent.transform);
            Cube[i].transform.position = eggPos.position;
            Cube[i].transform.rotation = eggPos.rotation;
            Cube[i].name = "cube_"+i;
            transform.eulerAngles = new Vector3(0, 0.7045f * i, 0);
        }
    }
    void Update()
    {
        for (int i = 0; i < 512; i++)
        {
            scale = data.ASsample_512_smoothed[i];
            Cube[i].transform.localScale = new Vector3(1, Mathf.Clamp(scale * 10f * i, 0, 50) + 1f, 0.2f);
        }
    }
}
