using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5_AI : MonoBehaviour
{
    public GameObject jian;
    public GameObject player;
    public GameObject mubei;
    float x;
    float y;
    bool isPlayer = false;
    bool time1 = false;
    bool time2 = false;
    float time = 0;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayer = true;
        }
    }


    void Start()
    {

    }


    void Update()
    {
        if (isPlayer)
        {
            jian.transform.Rotate(new Vector3(0, 400 * Time.deltaTime, 0));
            time += Time.deltaTime;
            if (time > 0.2 && !time1)
            {
                x = player.transform.position.x;
                y = player.transform.position.y;
                time1 = true;
            }
            if (time > 0.3)
            {
                if (!time2)
                {
                    jian.transform.position = new Vector3(player.transform.position.x, jian.transform.position.y, jian.transform.position.z);
                    time2 = true;
                }
                jian.transform.position -= new Vector3(0, 3f * Time.deltaTime, 0);
            }

            if (time > 3)
            {
                time = 0;
                time1 = false;
                time2 = false;
                jian.transform.position = new Vector3(player.transform.position.x, 10, jian.transform.position.z);
            }

        }
    }

}
