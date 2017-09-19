using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUi : MonoBehaviour {

    public Text loadTxt;
    public Slider loading;
    bool isAlive = true;

	void Start ()
    {
        Debug.Log("运行次数");
        loadTxt = transform.Find("Canvas/Slider/Txt_Loading").GetComponent<Text>();
        loading = transform.Find("Canvas/Slider").GetComponent<Slider>();

    }
	

	void Update () {
        if (isAlive)
        {
            LoadingAnmintor();
        }
        

    }

    public void LoadingAnmintor()
    {
        loading.value += Time.deltaTime * 0.45f;
        loadTxt.text = "加载中： " + (int)(loading.value * 100) + "%";
        if (loading.value>=1)
        {
            Destroy(loading.gameObject.transform.parent.gameObject);
            isAlive = false;
        }       
    }
}
