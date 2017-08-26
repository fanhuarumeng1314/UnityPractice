using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5_PlayerController : MonoBehaviour {
    public GameObject[] Stone = new GameObject[3];
    L5_PlayerCharacter character;

    bool die = false;

    void Start()
    {

        character = GetComponent<L5_PlayerCharacter>();
    }

    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        character.Move(h);

        if (h != 0)
        {
            Vector3 tmp_Dir = Vector3.right * h;
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

        character.Interaction2(character.GetTransform(character.transform));
        for (int i = 0; i < 3; i++)
        {
            Stone[i].transform.Rotate(new Vector3(-80 * Time.deltaTime, 0, 0));
        }
    }

}
