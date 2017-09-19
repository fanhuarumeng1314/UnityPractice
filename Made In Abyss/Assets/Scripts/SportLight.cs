using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SportLight : MonoBehaviour {

    public Transform playerPos;
    public float disTance = 7.0f;
    private void Awake()
    {
        playerPos = GameObject.FindObjectOfType<PlayCharacter>().gameObject.GetComponent<Transform>();
    }

    void Update () {
		
	}
    private void FixedUpdate()
    {
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y+disTance, playerPos.position.z);
    }
}
