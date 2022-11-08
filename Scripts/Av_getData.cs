using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Av_getData : MonoBehaviour
{
    AudioSource AS;
    [HideInInspector] public float[] ASsample_512_smoothed = new float[512];
    float[] ASsample_512 = new float[512];
    float[] ASsample_512_sub = new float[512];
    public float[] ASsample_8_smoothed = new float[8];
    float[] ASsample_8 = new float[8];
    float[] ASsample_8_sub = new float[8];
    //
    public Transform[] scaleAxis = new Transform[8];
    public bool enableAxis;
    public bool smooth;


    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        Get_ASD();
        ASD_remap();
        ASDR_smooth();
        ASDR_smooth_512();
    }

    void Get_ASD()
    {
        AS.GetSpectrumData(ASsample_512, 0, FFTWindow.Blackman);
    }
    void ASD_remap()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += ASsample_512[count] * (count + 1);
                count++;
            }
            average /= count;
            ASsample_8[i] = average * 10;
        }
    }
    void ASDR_smooth()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (smooth)
            {
                if (ASsample_8[g] > ASsample_8_smoothed[g])
                {
                    ASsample_8_smoothed[g] = ASsample_8[g];
                    ASsample_8_sub[g] = 0.001f;
                }
                if (ASsample_8[g] < ASsample_8_smoothed[g])
                {
                    ASsample_8_smoothed[g] -= ASsample_8_sub[g];
                    ASsample_8_sub[g] *= 1.2f;
                }
            }
            else
            {
                ASsample_8_smoothed[g] = ASsample_8[g];
            }
        }
    }
    void ASDR_smooth_512()
    {
        for (int g = 0; g < 512; ++g)
        {
            if (ASsample_512[g] > ASsample_512_smoothed[g])
            {
                ASsample_512_smoothed[g] = ASsample_512[g];
                ASsample_512_sub[g] = 0.00001f;
            }
            if (ASsample_512[g] < ASsample_512_smoothed[g])
            {
                ASsample_512_smoothed[g] -= ASsample_512_sub[g];
                ASsample_512_sub[g] *= 1.2f;
            }
        }
    }
}
