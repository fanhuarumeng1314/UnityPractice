using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_PlayerController : MonoBehaviour {

    L1_PlayerCharacter character;
	void Start ()
    {
        character = GetComponent<L1_PlayerCharacter>();	
	}
	
	void Update ()
    {
        var h = Input.GetAxis("Horizontal");
        character.Move(h);

        if (h != 0)
        {
            Vector3 tmp_Dir = Vector3.forward * h;
            character.Rotate(tmp_Dir);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            character.Interaction();
        }
	}
}
