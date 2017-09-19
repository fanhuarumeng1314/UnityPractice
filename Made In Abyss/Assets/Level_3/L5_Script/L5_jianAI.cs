using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L5_jianAI : MonoBehaviour
{
    public GameObject qishi1;
    public GameObject yellowKey;
    public GameObject Player;
    float time;

    void Update()
    {
        qishi1.transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Mubei")
        {
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            yellowKey.gameObject.SetActive(true);
            yellowKey.transform.position = Player.transform.position + new Vector3(0, 3, 0);
            yellowKey.transform.parent = Player.transform;
        }
        else if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<L5_PlayerCharacter>().Die();
        }
    }

}
