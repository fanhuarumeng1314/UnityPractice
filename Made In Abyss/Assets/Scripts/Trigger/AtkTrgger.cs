using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkTrgger : MonoBehaviour {

    public int damae;//伤害
	void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        var moster = other.GetComponent<AiCharacter>();
        if (moster)
        {
            moster.AiAtk(damae);
            moster.UpDateHealth();
        }
    }
}
