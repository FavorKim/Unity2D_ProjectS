using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private static Zoom instance;
    public static Zoom Instance { get { return instance; } }


    private float zoomSpeed;
    private float targetSize;
    private float curSize;
    bool finished = false;

    CinemachineVirtualCamera cam;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            cam = FindAnyObjectByType<CinemachineVirtualCamera>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }



    public void ZoomIn(float speed, float size)
    {
        //Debug.Log(cam.m_Lens.OrthographicSize);
        finished = false;
        zoomSpeed = speed;
        targetSize = size;
        StartCoroutine(CorZoomIn());
    }

    public void ZoomOut(float speed, float size)
    {
        finished = false;
        zoomSpeed = speed;
        targetSize = size;
        StartCoroutine(CorZoomOut());
    }

    public void ZoomInandOut(float speed, float size)
    {
        zoomSpeed = speed;
        targetSize = size;
        curSize = cam.m_Lens.OrthographicSize;
        StartCoroutine(CorInandOut());
    }

    IEnumerator CorZoomIn()
    {
        yield return null;

        if (cam.m_Lens.OrthographicSize <= targetSize)
        {
            Debug.Log("ZoomEnd");
            StopCoroutine(CorZoomIn());
        }

        Debug.Log("Zoom");

        cam.m_Lens.OrthographicSize -= zoomSpeed * Time.deltaTime;

    }

    IEnumerator CorZoomOut()
    {
        yield return null;

        if (cam.m_Lens.OrthographicSize >= targetSize)
            StopCoroutine(CorZoomOut());
        cam.m_Lens.OrthographicSize += zoomSpeed * Time.deltaTime;
    }

    IEnumerator CorInandOut()
    {
        yield return null;

        if (!finished)
            cam.m_Lens.OrthographicSize -= zoomSpeed * Time.deltaTime;

        if (cam.m_Lens.OrthographicSize <= targetSize)
            finished = true;

        if (finished)
        {
            cam.m_Lens.OrthographicSize += zoomSpeed * Time.deltaTime;
            if (cam.m_Lens.OrthographicSize >= curSize)
                StopCoroutine(CorInandOut());
        }

    }

}
