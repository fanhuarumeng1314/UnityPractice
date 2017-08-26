using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_TmpUi4 : MonoBehaviour {

    public Text tips;
    bool isTriger = false;
    void Start()
    {

    }


    void Update()
    {
        if (isTriger == true)
        {
            Destroy(tips.gameObject,3);
            Destroy(gameObject,3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tips.text = "当心了，海绵宝宝，前面有邪恶的东西，运用你的智慧，冲过去吧！";
        tips.gameObject.SetActive(true);
        isTriger = true;
    }
}
