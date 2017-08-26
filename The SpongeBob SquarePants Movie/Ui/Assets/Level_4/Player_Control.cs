using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    Player_Character character;
    void Start()
    {
        character = GetComponent<Player_Character>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        character.Move(h);

        if (h != 0)
        {
            if (character.IsControl)
            {
                Vector3 tmp_Dir = Vector3.forward * -h;
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
