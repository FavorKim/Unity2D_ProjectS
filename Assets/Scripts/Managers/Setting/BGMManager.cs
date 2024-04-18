using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip loop;
    bool isStop = false;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void BGMStop()
    {
        isStop = true;
        //source.Stop();
    }

    public void BGMPlay()
    {
        isStop=false;
        source.Play();
    }

    void Update()
    {
        if (!source.isPlaying && !isStop)
        {
            if (source.clip != loop)
                source.clip = loop;
            source.Play();
        }
    }
}
