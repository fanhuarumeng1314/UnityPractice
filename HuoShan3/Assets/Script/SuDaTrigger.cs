using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;
using Leap;
using UnityEngine.UI;

public class SuDaTrigger : MonoBehaviour
{
    public enum myHand
    {
        Left,
        Right,
    }

    public myHand myHandState = myHand.Left;

    public AudioManger audioManger;

    public LeapProvider provider;
    public GameObject volcano; //火山

    public Text tips;

    public GameObject shuDaEffect;//传入醋以及苏打的特效物体
    public GameObject cuEffect;
    public CraterTX timer;//传入火山特效脚本

    List<Finger> fingerList;//获取手的数组

    Vector3 initPosition; //杯子初始位置
    Quaternion initRotation; //杯子初始旋转
    Vector3 upwardDirection;
    GameObject rigidPalm;

    public bool isVolcanoUpper = false;//是否在火山上方

    public float pickTime;
    public bool isInit = false;

    void Start()
    {
        audioManger = FindObjectOfType<AudioManger>();
        volcano = GameObject.Find("Volcano");
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
        fingerList = new List<Finger>();
        //checkTran = gameObject.transform.Find("Sphere");

        initPosition = gameObject.transform.position;
        initRotation = gameObject.transform.rotation;
    }

    void Update()
    {
        Frame frame = provider.CurrentFrame;//得到每一帧

        foreach (Hand hand in frame.Hands)//获取每一帧里面的手
        {
            if (hand != null)
            {
                if ((hand.IsLeft && myHandState == myHand.Left) || (hand.IsRight && myHandState == myHand.Right))
                { Move(hand); }
            }
        }

        Angle();
        if (isInit)
        {
            Init();
        }


    }



    private void Move(Hand rHand)
    {
        var angleCheck = Vector3.Angle(transform.up, volcano.transform.up);
        //Debug.Log("angleCheck:" + angleCheck);
        if (myHandState == myHand.Left)
        {
            rigidPalm = GameObject.Find("palmLeft");
            upwardDirection = -rigidPalm.transform.right;
        }
        else
        {
            rigidPalm = GameObject.Find("palmRight");
            upwardDirection = rigidPalm.transform.right;
        }

        var palmPosition = rHand.PalmPosition.ToVector3(); //手掌位置

        var distanceCupToVolcano = (transform.position - volcano.transform.position).magnitude; //杯子到火山的距离
        Debug.DrawLine(transform.position, volcano.transform.position, Color.yellow);

        fingerList = rHand.Fingers;

        if (fingerList.Count != 0)
        {

            var distanceToPalm = (palmPosition - transform.position).magnitude; //手掌到杯子的距离
            var distBetweenFinger = (palmPosition - fingerList[0].TipPosition.ToVector3()).magnitude; //大拇指到杯子的距离

            if (distanceToPalm < 0.15f && distBetweenFinger < 0.08f)
            {
                transform.position = palmPosition + rHand.PalmNormal.ToVector3().normalized * 0.06f;

                transform.rotation = Quaternion.LookRotation(palmPosition - transform.position, upwardDirection);

                if (myHandState == myHand.Left)
                {
                    //audioManger.TakePlayLeft();
                    tips.text = "你拿起了小苏打";
                }
                else
                {
                    //audioManger.TakePlayRight();
                    tips.text = "你拿起了醋";
                }
            }
            else
            {
                //gameObject.transform.position = initPosition;
                //gameObject.transform.rotation = initRotation;
                isInit = true;
            }

            if (distanceCupToVolcano > 0.55f)
            {
                isInit = true;
                //gameObject.transform.position = initPosition;
                //gameObject.transform.rotation = initRotation;
            }

        }
    }

    public void Init()
    {
       
        pickTime += Time.deltaTime ;
        transform.position = Vector3.Lerp(transform.position, initPosition, pickTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, initRotation, pickTime);
        if (pickTime>1)
        {
            isInit = false;
            pickTime = 0;
        }
    }

    public void Angle()
    {

        if (myHandState == myHand.Left)
        {
            float cosAngleLeft = Vector3.Dot(transform.up, Vector3.up) / transform.up.magnitude;//通过叉乘得到夹角的cos值
            if (cosAngleLeft < 0 && isVolcanoUpper)
            {

                shuDaEffect.SetActive(true);
                //audioManger.SuDaPlay();//苏打音效
                RaycastHit hit;
                if (Physics.Raycast(shuDaEffect.transform.position,-Vector3.up,out hit,2.0f))//以特效发生点向下检测，如果打到火山口，增加持续时间
                {
                    tips.text = "您可以把小苏打尝试倒入火山口中";
                    if (hit.transform.tag== "Crater")
                    {
                        timer.timeSpeed_SuDa += Time.deltaTime;
                        tips.text = "";
                    }

                    
                }


            }
            if (!isVolcanoUpper | cosAngleLeft > 0)
            {
                
                shuDaEffect.SetActive(false);
            }
        }
        else
        {
            float cosAngleRight = Vector3.Dot(transform.up, Vector3.up) / transform.up.magnitude;//通过叉乘得到夹角的cos值
            if (cosAngleRight < 0 && isVolcanoUpper)
            {
                
                cuEffect.SetActive(true);
                //audioManger.CuPlay();//醋的音效
                RaycastHit hit;

                if (Physics.Raycast(cuEffect.transform.position, -Vector3.up, out hit, 2.0f))//以特效发生点向下检测，如果打到火山口，增加持续时间
                {
                    tips.text = "您可以把小苏打尝试倒入火山口中";
                    if (hit.transform.tag == "Crater")
                    {
                        timer.timeSpeed_Cu += Time.deltaTime;
                        tips.text = "";
                    }
                    
                }

            }

            if (!isVolcanoUpper | cosAngleRight > 0)
            {
                cuEffect.SetActive(false);
            }
        }

    }
}
