using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour {

    PlayCharacter character;

	
	void Start ()
    {
        character = GetComponent<PlayCharacter>();

    }
	
	
	void Update ()
    {
        if (character.isMove)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            character.Move(v, h);
            Vector3 dir = new Vector3(h, 0, v);

            if (dir != Vector3.zero)
            {
                character.Rotate(dir);
            }



            if (Input.GetKeyDown(KeyCode.J))
            {
                if (character.anmitorTimeOne==0)
                {
                    character.BattleSkillOne();
                    character.anmitorTimeOne = 45;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (character.anmitorTimeTwo == 0)
                {
                    character.BattleSkillTwo();
                    character.anmitorTimeTwo = 45;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                character.Boom();
            }
        }
    }
}
