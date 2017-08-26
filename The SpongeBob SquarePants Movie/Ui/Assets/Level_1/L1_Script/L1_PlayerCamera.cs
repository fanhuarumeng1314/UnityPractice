using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_PlayerCamera : MonoBehaviour {

    public Transform trs;

   
    private void LateUpdate()
    {
        transform.position = trs.position;
    }
}
