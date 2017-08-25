using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    float timeSpeed;
    int i = 0;
    GameMode gameMode;
	void Start ()
    {
        gameMode = FindObjectOfType<GameMode>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameMode.isWangCheng&&gameMode.road.Count>0)
        {
             
            timeSpeed += Time.deltaTime;
            if (timeSpeed>0.1f)
            {
               // timeSpeed = 0;
                if (i> (gameMode.road.Count - 1))
                {
                    gameMode.road.Clear();
                    i = 0;
                    return;
                }
                transform.position = gameMode.road[gameMode.road.Count-1-i];
                i++;


            }

        }
          

	}
}
