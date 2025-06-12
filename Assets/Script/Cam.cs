using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform tr;
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,tr.position + Vector3.back + Vector3.up, Time.deltaTime * 3);
    }
}
