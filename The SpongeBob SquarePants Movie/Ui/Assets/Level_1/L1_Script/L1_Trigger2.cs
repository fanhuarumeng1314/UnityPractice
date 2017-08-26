using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1_Trigger2 : MonoBehaviour {

    public AudioClip vectorySound;
    AudioSource audioS;

    public Text victory_Text;
    public Button victory_Button;

	void Start () {
        audioS = GetComponent<AudioSource>();
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioS.clip = vectorySound;
            audioS.Play();

            victory_Text.text = "恭喜你完成了教学关卡，快赶紧开始冒险吧！";
            victory_Button.gameObject.SetActive(true);
            victory_Text.gameObject.SetActive(true);
        }
    }
}
