using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L1_PlayerCharacter : MonoBehaviour
{
    public AudioClip jumpSound; // 跳 音乐
    public AudioClip grabSound; // 抓 音乐
    public AudioClip deathSound; // 死亡 音乐

    AudioSource audioS;

    public float moveSpeed;//移动变化量
    public float jumpPower;//跳跃力量
    public float rotaSpeed;//转向速度
    public Transform parentPos;//传入父节点的坐标
    public Button uiButton;//死亡显示
    public Text dethText;//死亡显示文本
    public Transform radialPos;//传入射线发射的位置


    protected Animator animator;
    CharacterController cc;
    bool isRotate = false;//旋转是否成功
    bool isBox = false;//角色是否有箱子
    bool isAlive = true;//角色是否活着
    Vector3 tmp_CcPos;//角色控制器移动的临时坐标

    public int armsAnimatorLayer; // 动画权重

    void Start ()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();

        if (animator)
            animator.SetLayerWeight(armsAnimatorLayer, 1f);
    }
	
	
	void Update ()
    {
        if (!isAlive)
        {
            return;
        }
        tmp_CcPos.x = 0;

        cc.Move(tmp_CcPos*Time.deltaTime);
        animator.SetFloat("Speed", cc.velocity.magnitude);//通过角色的速度变化更新动作
        animator.SetBool("IsGround", cc.isGrounded);//角色在地面时不触发此动作，在空中更新动作
        animator.SetBool("IsGrab",isBox?true:false);

        tmp_CcPos.y += cc.isGrounded ? 0f : Physics.gravity.y * 1f * Time.deltaTime;//更新重力

        if (transform.position.y < -10)
        {
            Deth();
        }
        

    }

    public void Move(float h)//移动
    {
        tmp_CcPos.z = h* moveSpeed;
    }

    public void Jump()//跳跃
    {

        if (cc.isGrounded)
        {
            tmp_CcPos.y = jumpPower;
            audioS.clip = jumpSound;
            audioS.Play();
        }

    }

    public void Rotate(Vector3 dir)//转向  这是Z轴移动的旋转，如移动是在X轴移动，需要改变移动方法，以及传入进来的方向
    {
        isRotate = false;

        var tmpPos = dir + transform.position;
        var targetPos = transform.position;//获取当前的位置
        tmpPos.y = 0;
        targetPos.y = 0;

        var tmp_Dir = (tmpPos - targetPos).normalized;//计算方向向量

        Quaternion force = Quaternion.LookRotation(tmp_Dir);//转换为旋转四元数

        Quaternion slerp = Quaternion.Slerp(transform.rotation, force, rotaSpeed * Time.deltaTime);//取差值的四元数进行物体的旋转


        if (slerp == force)
        {
            isRotate = true;
        }

        transform.rotation = slerp;

    }

    public void Interaction()//互动
    {
        RaycastHit hit;
        Debug.DrawRay(radialPos.position, radialPos.forward*0.1f,Color.red,10.0f);//画出射线

        if (Physics.Raycast(radialPos.position, radialPos.forward,out hit,0.1f))//射线检测，可更改！
        {

            Debug.Log(hit.transform.tag);
            if (hit.transform.gameObject.tag == "Box" && isBox == false)
            {
                audioS.clip = grabSound;
                audioS.Play();

                var tmp_BoxPos = hit.transform;
                tmp_BoxPos.SetParent(parentPos);//改变父子关系
                tmp_BoxPos.localPosition = Vector3.zero;
                tmp_BoxPos.localRotation = Quaternion.identity;
                tmp_BoxPos.GetComponent<Rigidbody>().isKinematic = true;
                
                isBox = true;
            }
            else if (hit.transform.gameObject.tag == "Box" && isBox == true)
            {
                audioS.clip = grabSound;
                audioS.Play();

                var tmp_BoxPos = hit.transform;

                tmp_BoxPos.SetParent(null);//接除父子关系
                tmp_BoxPos.GetComponent<Rigidbody>().isKinematic = false;
                isBox = false;
            }
        }
    }

    public void Deth()
    {
        audioS.clip = deathSound;
        audioS.Play();

        isAlive = false;
        dethText.text = "很抱歉，您死了呢〒_〒";
        dethText.gameObject.SetActive(true);
        uiButton.gameObject.SetActive(true);
    }

   
}
