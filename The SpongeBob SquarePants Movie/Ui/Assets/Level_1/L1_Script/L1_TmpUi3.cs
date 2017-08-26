using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_TmpUi3 : MonoBehaviour {

    public Text tips;
    bool isTriger = false;
    void Start()
    {

    }


    void Update()
    {
        if ( isTriger == true)
        {
            Destroy(tips.gameObject,3);
            Destroy(gameObject,3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tips.text = "注意，前方有机关，踩上去说不定有惊喜哦！";
        tips.gameObject.SetActive(true);
        isTriger = true;
    }
}
