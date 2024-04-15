using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer lR;
    [SerializeField] private float size = 0.5f;

    [SerializeField] private Transform player;
    [SerializeField] private Transform anchor;


    private void Awake()
    {
        lR = GetComponent<LineRenderer>();

        lR.startWidth = size;
    }

    void Update()
    {
        lR.SetPosition(0, player.position);
        lR.SetPosition(1, anchor.position);
    }
}
