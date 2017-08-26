using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L5_PlayerCharacter : MonoBehaviour {

    public AudioClip jumpSound;
    public AudioClip GrabSound;
    public AudioClip deathSound;

    AudioSource audioS;


    float times = 0;
    public float moveSpeed;//移动变化量
    public float jumpPower;//跳跃力量
    public float rotaSpeed;//转向速度
    public GameObject chooper;//获取斧头物体
    public GameObject Door1;
    public GameObject[] door1 = new GameObject[2];//第一道门的旋转
    bool isDoor1 = false;
    public GameObject GreenKey;
    bool isGreenKey = false;
    public GameObject doorLast;
    public Text deth_Text5;//死亡文本
    public Button deth_Button5;//死亡按钮

    bool isAlive = true;
    private Animator animator;
    CharacterController cc;
    bool isRotate = false;//旋转是否成功
    Vector3 tmp_CcPos;//角色控制器移动的临时坐标
    void Start()
    {

        cc = GetComponent<CharacterController>();
        animator = this.GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
    }


    void Update()
    {

        if (!isAlive)
        {
            return;
        }
        tmp_CcPos.z = 0;

        cc.Move(tmp_CcPos * Time.deltaTime);
        animator.SetFloat("RunSpeed", cc.velocity.magnitude);
        animator.SetBool("IsGround", cc.isGrounded);
        animator.SetBool("OpenDoor", Input.GetKey(KeyCode.K) ? true : false);

        tmp_CcPos.y += cc.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;//更新重力
    }

    public void Move(float h)//移动
    {

        tmp_CcPos.x = h * moveSpeed;//这里玩家在Z轴上移动；
    }

    public void Jump()//跳跃
    {
        if (cc.isGrounded)
        {
            audioS.clip = jumpSound;
            audioS.Play();

            tmp_CcPos.y = jumpPower;
        }

    }

    public void Rotate(Vector3 dir)//转向  这是Z轴移动的旋转，如移动是在X轴移动，需要改变移动方法，以及传入进来的方向
    {
        isRotate = false;

        var tmpPos = dir + transform.position;
        var targetPos = transform.position;//获取当前的位置
        tmpPos.y = 0;
        targetPos.y = 0;

        var tmp_Dir = tmpPos - targetPos;//计算方向向量

        Quaternion force = Quaternion.LookRotation(tmp_Dir);//转换为旋转四元数

        Quaternion slerp = Quaternion.Slerp(transform.rotation, force, rotaSpeed * Time.deltaTime);//取差值的四元数进行物体的旋转


        if (slerp == force)
        {
            isRotate = true;
        }

        transform.rotation = slerp;

    }


    public bool GetTransform(Transform check)
    {
        foreach (Transform t in check.GetComponentsInChildren<Transform>())
        {
            if (t.name == "yellowkey")
                return true;
        }
        return false;
    }


    public void Interaction2(bool isyellowkey)//互动
    {
        if (isyellowkey)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))//射线检测门
            {
                if ((hit.transform.tag == "LastDoor") && (Input.GetKey(KeyCode.K)))
                {
                    Destroy(doorLast);
                }
            }
        }
    }



    public void Interaction()//互动
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))//射线检测绿钥匙
        {
            if ((hit.transform.tag == "GreenTree"))
            {
                audioS.clip = GrabSound;
                audioS.Play();

                GreenKey.transform.position = cc.transform.position + new Vector3(0, 3, 0);
                GreenKey.transform.parent = cc.transform;
                isGreenKey = true;
            }
        }


        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))//射线检测绿钥匙
        {
            if ((hit.transform.tag == "Door1"))
            {
                if (isGreenKey)
                {
                    audioS.clip = GrabSound;
                    audioS.Play();

                    GreenKey.gameObject.SetActive(false);
                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 0)
                        {
                            door1[i].transform.Rotate(new Vector3(0, 100, 0));
                        }
                        else
                        {
                            door1[i].transform.Rotate(new Vector3(0, -100, 0));
                        }

                    }
                }
            }
        }



        if (Physics.Raycast(transform.position, transform.up, out hit, 5f))//射线检测死亡阑珊
        {
            if (hit.transform.tag == "Die")
                Die();
        }

    }

    public void Chooper(bool chooper)
    {
        if (chooper)
        {

        }
    }

    public void Die()
    {
        if (isAlive)
        {
            audioS.clip = deathSound;
            audioS.Play();
        }

        deth_Text5.text = "很抱歉，你死了呢！";
        deth_Text5.gameObject.SetActive(true);
        deth_Button5.gameObject.SetActive(true);
        isAlive = false;
    }

}
