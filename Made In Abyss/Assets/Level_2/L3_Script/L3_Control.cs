using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_Control : MonoBehaviour
{

    L3_Player character;
    void Start()
    {
        character = GetComponent<L3_Player>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        character.Move(h);

        if (h != 0)
        {
            Vector3 tmp_Dir = Vector3.forward * h;
            if (character.IsControl)
            {
                character.Rotate(tmp_Dir);
            }
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
