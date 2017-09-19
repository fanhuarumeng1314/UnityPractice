using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L3_Player : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip GrabSound;
    public AudioClip deathSound;

    AudioSource audioS;


    public Transform grabSocketGrab; // 抓取位置1
    public Transform grabSocketCarry; // 抓取位置2
    public Transform grabObjectGrab; // 抓取物体
    public Transform grabObjectCarry;// 举起物体
    public int armsAnimatorLayer; // 动画权重
    public Text deth_Text4;//死亡文字
    public Button deth_Button;//死亡按钮


    protected Animator animator;

    public float moveSpeed;//移动变化量
    public float jumpPower;//跳跃力量
    public float rotaSpeed;//转向速度

    bool isAlive = true;
    protected CharacterController cc;
    bool isRotate = false;//旋转是否成功
    Vector3 tmp_CcPos;//角色控制器移动的临时坐标

    public bool IsControl = true;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        if (animator)
            animator.SetLayerWeight(armsAnimatorLayer, 1f);

        audioS = GetComponent<AudioSource>();
    }


    void Update()
    {
        //  动画
        animator.SetFloat("Speed", cc.velocity.magnitude);
        animator.SetBool("IsRound", cc.isGrounded);
        animator.SetBool("Grab", grabObjectGrab ? true : false);
        animator.SetBool("Carry", grabObjectCarry ? true : false);

        // 锁定X轴
        tmp_CcPos.x = 0;
        cc.Move(tmp_CcPos * Time.deltaTime);

        tmp_CcPos.y += cc.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;//更新重力

        if (transform.position.y<-5)
        {
            Deth();
        }
    }

    public void Move(float h)//移动
    {
        if (IsControl)
        {
            tmp_CcPos.z = h * moveSpeed;//这里玩家在Z轴上移动；
            transform.position = new Vector3(0, transform.position.y, transform.position.z); // 锁定x轴
        }
    }

    public void Jump()//跳跃
    {
        if (IsControl)
        {
            if (cc.isGrounded)
            {
                audioS.clip = jumpSound;
                audioS.Play();

                tmp_CcPos.y = jumpPower;
            }
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


        /*if (slerp == force)
        {
            isRotate = true;
        }*/

        transform.rotation = slerp;

    }

    public void Interaction()//互动
    {
        GrabCheck();
    }

    public void GrabCheck() // 抓取
    {
        //如果有抓取物，且转向完成
        if (grabObjectGrab != null)
        {
            audioS.clip = GrabSound;
            audioS.Play();

            grabObjectGrab.transform.SetParent(null);

            // 使其受到动力学影响
            grabObjectGrab.GetComponent<Rigidbody>().isKinematic = false;
            grabObjectGrab = null;
        }
        else if (grabObjectCarry != null)
        {
            audioS.clip = GrabSound;
            audioS.Play();

            grabObjectCarry.transform.SetParent(null);

            // 使其受到动力学影响
            grabObjectCarry.GetComponent<Rigidbody>().isKinematic = false;
            grabObjectCarry = null;
        }
        //如果没有抓取物
        else if (grabObjectGrab == null && grabObjectCarry == null)
        {
            var dist = cc.radius;

            RaycastHit hit; // 光线投射碰撞  用来获取从raycast函数中得到的信息反馈的结构。

            //从start起点到end末点，经过duration一段时间，绘制一条color颜色的线。如果duration为0，那么这条线在1帧中被渲染。
            Debug.DrawLine(transform.position, transform.position + transform.forward * (dist + 1f), Color.green, 10f);

            // 光线投射（射线发射位置， 射线发射方向， 射线长度）
            if (Physics.Raycast(transform.position, transform.forward, out hit, dist + 1f))
            {
                if (hit.collider.CompareTag("GrabBox"))
                {
                    audioS.clip = GrabSound;
                    audioS.Play();

                    grabObjectGrab = hit.transform;
                    grabObjectGrab.SetParent(grabSocketGrab);
                    grabObjectGrab.localPosition = Vector3.zero;

                    // 同一性旋转。该四元数，相当于"无旋转"：这个物体完全对齐于世界或父轴。
                    grabObjectGrab.localRotation = Quaternion.identity;

                    grabObjectGrab.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            if (Physics.Raycast(transform.position, transform.forward, out hit, dist + 1f))
            {
                if (hit.collider.CompareTag("RaiseBox"))
                {
                    audioS.clip = GrabSound;
                    audioS.Play();

                    grabObjectCarry = hit.transform;
                    grabObjectCarry.SetParent(grabSocketCarry);
                    grabObjectCarry.localPosition = Vector3.zero;

                    // 同一性旋转。该四元数，相当于"无旋转"：这个物体完全对齐于世界或父轴。
                    grabObjectCarry.localRotation = Quaternion.identity;

                    grabObjectCarry.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }
    }


    public void Deth()
    {
        if (isAlive)
        {
            isAlive = false;
            audioS.clip = deathSound;
            audioS.Play();

            deth_Text4.text = "很抱歉，你死了呢！";
            deth_Text4.gameObject.SetActive(true);
            deth_Button.gameObject.SetActive(true);
        }
        
    }
}
