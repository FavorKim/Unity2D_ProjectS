using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Test : MonoBehaviour
{
    [SerializeField] Anchor target;
    void FixedUpdate()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }
}
