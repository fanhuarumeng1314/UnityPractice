using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Carem : MonoBehaviour
{

    public Transform trans;
    public float xDistance = 2.85f;
    public float yDistance = 2.85f;
    public float zDistance = 5.97f;

    void Update()
    {
        if (trans != null)
        {
            transform.position = new Vector3(trans.position.x - xDistance, trans.position.y - yDistance, trans.position.z - zDistance);
        }
    }
}
