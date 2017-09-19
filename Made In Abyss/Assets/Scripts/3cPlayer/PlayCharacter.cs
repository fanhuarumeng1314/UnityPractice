using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayCharacter : PlayData
{
    public static PlayCharacter Instance;
    PlayGameMode gameMode;
    public Animator playAnimator;
    CharacterController charact;
    public float moveSpeed = 3f;
    public float rotaSpeed = 10f;
    Vector3 tmp_Pos;
    public int anmitorTimeOne = 0;//2个技能的施放时间
    public int anmitorTimeTwo = 0;
    public bool isRota = true;//是否转向
    public bool isMove = true;//是否移动
    public bool isPlayDamge = false;//是否播放受伤动画

    public SkillOne imgSkillOne;
    public SkillTwo imgSkillTwo;
    public SkillBoom imgBoom;

    public GameObject txPrefeb;
    public GameObject txPrefebSkillTwo;

    public Transform txPoint;

    public GameObject atkTrigger;//获取攻击触发器
    public AtkTrgger tmp_AtkScprits;//获取攻击脚本

    public AudioClip oneClip;
    public AudioClip twoClip;
    public AudioClip deameClip;

    private void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        deameClip = Resources.Load("AudioClicp/被击倒1_爱给网_aigei_com", typeof(AudioClip))as AudioClip;
        oneClip = Resources.Load("AudioClicp/刀剑划过、气体爆炸_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        twoClip = Resources.Load("AudioClicp/风特技抡_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        imgBoom = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu/Img_Skill_Bg/Boom").GetComponent<SkillBoom>();
        imgSkillOne = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu/Img_Skill_Bg/SkillOne").GetComponent<SkillOne>();
        imgSkillTwo = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu/Img_Skill_Bg/SkillTwo").GetComponent<SkillTwo>();//获取3个技能图片挂载的脚本

        bag.Add(PropLibry.Instance.propTable["1022"], 10);
        bag.Add(PropLibry.Instance.propTable["1019"], 3);
        bag.Add(PropLibry.Instance.propTable["1002"], 3);
        bag.Add(PropLibry.Instance.propTable["1003"], 1);
        bag.Add(PropLibry.Instance.propTable["1006"], 1);
        bag.Add(PropLibry.Instance.propTable["1007"], 1);
        bag.Add(PropLibry.Instance.propTable["1008"], 1);
        Debug.Log("加入炸弹成功");

        atkTrigger = transform.Find("AtkTrigger").gameObject;
        tmp_AtkScprits = atkTrigger.GetComponent<AtkTrgger>();

        txPoint = transform.Find("TxPoint").GetComponent<Transform>();
        txPrefeb = Resources.Load("EffectPrefeb/Atk1_TX") as GameObject;
        txPrefebSkillTwo = Resources.Load("EffectPrefeb/Atk_Tx2") as GameObject;

        charact = GetComponent<CharacterController>();
        playAnimator = GetComponent<Animator>();
        Debug.Log("执行玩家的Start");
    }
	
	
	void Update ()
    {
#region//技能释放相关的函数
        anmitorTimeOne--;
        anmitorTimeTwo--;
        if (anmitorTimeOne < 0)
        {
            anmitorTimeOne = 0;

            playAnimator.SetBool("SkillOne", false);
            atkTrigger.SetActive(false);

        }
        if (anmitorTimeOne==5)
        {
            AudioPlay(oneClip);

            atkTrigger.SetActive(true);
            tmp_AtkScprits.damae = atk;

            var tmp_AtkTX = Instantiate(txPrefeb);
            tmp_AtkTX.transform.rotation = transform.rotation;
            tmp_AtkTX.transform.position = transform.position + transform.forward*0.5f- transform.right*0.2f;
            tmp_AtkTX.transform.DOMove((transform.position + transform.forward * 2f),1);
            Debug.Log("技能释放次数");

        }
        float changeSkillOneValue = (float)anmitorTimeOne / 45f;
        float changeSkillTwoValue = (float)anmitorTimeTwo / 45f;
        imgSkillOne.ChangeMask(changeSkillOneValue);
        imgSkillTwo.ChangeSkillTwoMask(changeSkillTwoValue);//更改技能图片

        if (anmitorTimeTwo<0)
        {
            anmitorTimeTwo = 0;
            playAnimator.SetBool("SkillTwo", false);
        }

        if (anmitorTimeTwo==20)
        {
            AudioPlay(twoClip);
            atkTrigger.SetActive(true);
            tmp_AtkScprits.damae = atk;

            var tmp_AtkTwo = Instantiate(txPrefebSkillTwo);
            tmp_AtkTwo.transform.rotation = transform.rotation;
            tmp_AtkTwo.transform.position = transform.position + transform.forward * 0.5f - transform.right * 0.2f;
            tmp_AtkTwo.transform.DOMove((transform.position + transform.forward * 1.2f),1f);
        }
#endregion
        if (isMove)
        {
            charact.Move(tmp_Pos * Time.deltaTime);
        }
        

        playAnimator.SetFloat("MoveSpeed", charact.velocity.magnitude);


    }


    public void Move(float h,float v)//移动
    {
        tmp_Pos = Vector3.forward * h * moveSpeed+Vector3.right*v* moveSpeed;
        tmp_Pos.y += charact.isGrounded ? 0f : Physics.gravity.y * 10f * Time.deltaTime;
    }

    public void Rotate(Vector3 dir)//转向
    {
        Quaternion tmp = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, tmp,rotaSpeed*Time.deltaTime);
    }

    public void BattleSkillOne()//战斗技能1
    {
        playAnimator.SetBool("SkillOne",true);

    }

    public void BattleSkillTwo()//战斗技能2
    {
        playAnimator.SetBool("SkillTwo", true);
    }

    public void Boom()//丢炸弹
    {
        if (bag.ContainsKey(PropLibry.Instance.propTable["1022"]))
        {
            if (bag[PropLibry.Instance.propTable["1022"]] > 0)
            {
                bag[PropLibry.Instance.propTable["1022"]]--;

                var tmp_Boom = Instantiate(Resources.Load("PropsPrefeb/SpecialPropPrefeb/Boom") as GameObject, transform.position, Quaternion.identity);
                tmp_Boom.AddComponent<Boom>();
            }
        }
        imgBoom.boomNuber.text = bag[PropLibry.Instance.propTable["1022"]].ToString();//更新炸弹的数量
    }

    public void Damge(int msAtk)//受伤方法
    {
        AudioPlay(deameClip);
        if (msAtk <= dfs)
        {
            heathly--;
        }
        else
        {
            heathly = heathly - (msAtk - dfs);
        }
        if (heathly<=0)
        {
            Deth();
            return;
        }

        if (!isPlayDamge)
        {
            isPlayDamge = true;
            playAnimator.SetBool("IsDamge", true);
            StartCoroutine(CloseDamgeAnmitor());
        }
    }


    IEnumerator CloseDamgeAnmitor()
    {
        yield return new WaitForSeconds(0.2f);
        playAnimator.SetBool("IsDamge", false);
        isPlayDamge = false;
        yield return null;
    }

    public void Deth()//死亡
    {
        playAnimator.SetBool("IsAlive", true);

        isMove = false;
        ///<summary>
        ///出现死亡Ui，玩家不能移动，游戏结束
        /// </summary>

        var overUi = UiManger.Instance.GreatUI<Btn_Over>(UiManger.Instance.UIRoot.transform.Find("Canvas_Menu"));

    }

    public void MoveStart(Vector3 startPoint)//移动到起始点的位置
    {
        transform.position = startPoint;
    }

    public void UpdeteData()//刷新玩家属性
    {
        atk = 30;
        dfs = 10;
        if (nowWeapon!=null)
        {
            Prop tmp_Prop = null;
            foreach (var pair in PropLibry.Instance.propTable)
            {
                if (pair.Value.Route == nowWeapon)
                {
                    tmp_Prop = pair.Value;
                    break;
                }
            }
            atk += int.Parse(tmp_Prop.Atk);
            dfs += int.Parse(tmp_Prop.Dfs);
        }

        if (nowArmor!=null)
        {

            Prop tmp_Prop = null;
            foreach (var pair in PropLibry.Instance.propTable)
            {
                if (pair.Value.Route == nowArmor)
                {
                    tmp_Prop = pair.Value;
                    break;
                }
            }
            atk += int.Parse(tmp_Prop.Atk);
            dfs += int.Parse(tmp_Prop.Dfs);
        }

        if (nowRing != null)
        {

            Prop tmp_Prop = null;
            foreach (var pair in PropLibry.Instance.propTable)
            {
                if (pair.Value.Route == nowRing)
                {
                    tmp_Prop = pair.Value;
                    break;
                }
            }
            atk += int.Parse(tmp_Prop.Atk);
            dfs += int.Parse(tmp_Prop.Dfs);
        }
    }

    IEnumerator DeletClip(GameObject audio)
    {
        yield return new WaitForSeconds(1f);
        Destroy(audio);
        yield return null;
    }

    public void AudioPlay(AudioClip nowClip)
    {
        GameObject audio = new GameObject();
        var source = audio.AddComponent<AudioSource>();
        source.clip = nowClip;
        source.volume = 0.5f;
        source.Play();
        StartCoroutine(DeletClip(audio));
    }
}
