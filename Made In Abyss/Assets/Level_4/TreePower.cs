using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePower : MonoBehaviour
{
    Player_Character p_c;

    public GameObject tree;
    public GameObject coin;
    public Transform cubeTrans;
    bool treeActive = false;

    void Awake()
    {
        p_c = FindObjectOfType<Player_Character>();    
    }

    void Update()
    {
        if (treeActive)
        {
            tree.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime *5f;

            cubeTrans.position += new Vector3(0, 1, 0) * Time.deltaTime * 1f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && p_c.grabObjectCarry !=null)
        {
            tree.SetActive(true);
            other.transform.SetParent(cubeTrans);
            other.transform.localRotation = Quaternion.identity;

            coin.SetActive(false);
            p_c.grabObjectCarry = null;
            

            treeActive = true;
        }
    }
}
