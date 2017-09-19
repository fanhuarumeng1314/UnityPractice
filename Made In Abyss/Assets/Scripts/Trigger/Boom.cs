using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour {


    public GameObject boomRangeUp;
    public GameObject boomRangeLeft;
    public GameObject boomTX;
    public AudioClip boomClicp;

	void Start ()
    {
        boomClicp = Resources.Load("AudioClicp/勘探大_20 - 爆炸_ 大爆炸_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        boomRangeUp = transform.Find("BoomRangeUP").gameObject;
        boomRangeUp.AddComponent<BoomTrigger>();
        boomRangeLeft = transform.Find("BoomRangeLeft").gameObject;
        boomRangeLeft.AddComponent<BoomTrigger>();
        boomTX = transform.Find("explosionBoom").gameObject;

        StartCoroutine(BoomStart());

    }


    void Update()
    {

    }

    IEnumerator BoomStart()
    {
        yield return new WaitForSeconds(0.6f);
        GameObject boomAudio = new GameObject();
        boomAudio.transform.parent = transform;
        var boomSource = boomAudio.AddComponent<AudioSource>();
        boomSource.clip = boomClicp;
        boomSource.volume = 0.65f;
        boomSource.Play();

        boomRangeUp.SetActive(true);
        boomRangeLeft.SetActive(true);
        boomTX.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);

        yield return null;
    }
}
