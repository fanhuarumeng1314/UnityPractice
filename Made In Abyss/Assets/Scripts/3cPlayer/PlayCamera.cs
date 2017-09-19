using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayCamera : MonoBehaviour {

    public Transform target;
    public float distance = 2.0f;
    public float fowrdDistance = 2.9f;
    public float height = 7.8f;
    public float rightDistance = 0f;

    float rotationDamping = 5.0f;

    private void Awake()
    {
        target = GameObject.FindObjectOfType<PlayCharacter>().GetComponent<Transform>();
    }


    void LateUpdate()
    {
        if (!target)
            return;

        var heightDistannce = target.position.y + height;//计算出相应的高度差
        var nowfowrdDistance = target.position.z - fowrdDistance;//计算出相机的Z轴距离差值
        var nowRightDistance = target.position.x + rightDistance;

        transform.position = target.position;
        transform.position = new Vector3(nowRightDistance, heightDistannce, nowfowrdDistance); //- target.forward*distance;
        transform.LookAt(target);

    }
}
