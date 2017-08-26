using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5_PlayerCamera : MonoBehaviour
{
    public Transform target;
    Vector3 offset;

    void Start()
    {
        transform.position = new Vector3(-51, -2, 5);
        offset = target.position - transform.position;
    }

    void Update()
    {
        transform.position = target.transform.position + offset;
        transform.LookAt(target);
        transform.Rotate(new Vector3(-20, 0, 0));
        transform.Rotate(0, 5, 0);
    }


}
