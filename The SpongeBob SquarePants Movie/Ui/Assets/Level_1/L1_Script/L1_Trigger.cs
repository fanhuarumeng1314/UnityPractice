using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Trigger : MonoBehaviour {

    public Transform bridge;
    bool rotate = false;
    bool isMove = false;
	void Update ()
    {
        if (rotate)
        {
            BridgeRotate();
        }

        if (isMove)
        {
            Move();
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rotate = true;
            isMove = true;
        }
    }

    void BridgeRotate()
    {
        var slerp = Quaternion.Slerp(bridge.rotation,Quaternion.Euler(0,180,0),0.4f*Time.deltaTime);
        bridge.rotation = slerp;
        if (bridge.rotation == Quaternion.Euler(0,180,0))
        {
            rotate = false;
        }
    }

    public void Move()
    {
        if (transform.position.y>0.131f)
        {
            transform.position -= new Vector3(0, 0.01f, 0);
        }
    }
}
