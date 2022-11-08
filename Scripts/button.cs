using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    public GameObject newCollider;
    public AudioSource audioo;
    public Image playImage;
    public Image quitImage;
    bool isPlaying = false;
    bool isRestain = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            audioo.enabled = false;
            quitImage.enabled = false;
            playImage.enabled = true;

        }
        else
        {
            audioo.enabled = true;
            quitImage.enabled = true;
            playImage.enabled = false;
        }
        if (!isRestain)
        {
            newCollider.SetActive(false);
            for (int i = 0; i < audioo.transform.childCount; i++)
            {
                for (int g = 0; g < audioo.transform.GetChild(i).childCount; g++)
                {
                    audioo.transform.GetChild(i).GetChild(g).GetComponent<restrain>().isActive = false;
                }
            }
        }
        else
        {
            //newCollider.SetActive(true);
            for (int i = 0; i < audioo.transform.childCount; i++)
            {
                for (int g = 0; g < audioo.transform.GetChild(i).childCount; g++)
                {
                    audioo.transform.GetChild(i).GetChild(g).GetComponent<restrain>().isActive = true;
                }
            }

        }
    }
    public void quit()
    {
        isPlaying = false;
    }
    public void play()
    {
        isPlaying = true;
    }
    public void free()
    {
        isRestain = !isRestain;
    }
    void esc()
    {
        Application.Quit();
    }

}
