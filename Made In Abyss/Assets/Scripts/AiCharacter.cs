using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NowPos
{
    public int nowX;
    public int nowZ;

    
    public NowPos()
    {
    }
    
}

public class AiCharacter : AiData {

    public Slider aiealth;
    public NowPos nowPos = new NowPos();
    public GameObject aiDethTx;//怪物死亡特效

    public Color32 greenColor = new Color32(59, 251, 124, 255);
    public Color32 redColor = new Color32(251, 59, 59, 255);
    public Image aiealthColor;

    public Animator msAnimator;
    public float moveSpeed=1.0f;
    public float distance;

    public float playTime = 1.0f;
    Transform player;

    public AudioClip atkClip;
    public AudioClip demgeClip;
    public AudioClip dropClip;

    private void Awake()
    {

        atkClip = Resources.Load("AudioClicp/不死族男性主要 AttackA_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        demgeClip = Resources.Load("AudioClicp/敌人的死亡的声音2_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;
        dropClip = Resources.Load("AudioClicp/金属碰撞－掉落_爱给网_aigei_com",typeof(AudioClip)) as AudioClip;

        aiDethTx = Resources.Load("AiDethTx") as GameObject;

        aiealth = transform.Find("Canvas/Slider").GetComponent<Slider>();
        aiealthColor = transform.Find("Canvas/Slider/Fill Area/Fill").GetComponent<Image>();
        aiealthColor.color = greenColor;
        aiealth.gameObject.SetActive(false);
        nowPos.nowX = (int)transform.position.x;
        nowPos.nowZ = (int)transform.position.z;
        msAnimator = GetComponent<Animator>();
    }

    void Start ()
    {

        player = GameObject.FindObjectOfType<PlayCharacter>().gameObject.GetComponent<Transform>();
	}
	
	
	void Update ()
    {
        distance = Vector3.Distance(new Vector3(transform.position.x,player.position.y,transform.position.z),player.position);

        if (PlayCharacter.Instance.isMove)
        {
            if (distance < 5.0f)
            {
                if (distance < 1.0f)
                {
                    playTime += Time.deltaTime;
                    if (playTime >= 1.0f)
                    {
                        playTime = 0;
                        Debug.Log("执行次数");
                        msAnimator.SetBool("IsAtk", true);
                        StartCoroutine(CloseAtkAnimtor());
                    }
                    msAnimator.SetBool("IsMove", false);

                }
                else
                {
                    msAnimator.SetBool("IsMove", true);

                    transform.LookAt(player);
                    AiMove();
                }

            }
            else
            {
                msAnimator.SetBool("IsMove", false);
            }
        }
       
	}

    public void MsDamgeAnimtor()//受到伤害，动画播放
    {
        if (distance > 5.0f && distance < 1.0f)
        {
            msAnimator.SetBool("IsSufferAtk", true);
            StartCoroutine(CloseSufferAnimtor());
        }
        else
        {
            msAnimator.SetBool("IsDamge", true);
            StartCoroutine(CloseDamgeAnimto());
        }
    }

    IEnumerator CloseDamgeAnimto()
    {
        yield return new WaitForSeconds(0.5f);
        msAnimator.SetBool("IsDamge", false);
        yield return null;
    }

    IEnumerator CloseAtkAnimtor()//关闭攻击动画
    {
        yield return new WaitForSeconds(0.3f);
        MsAtkPlayer();
        yield return new WaitForSeconds(0.4f);
        msAnimator.SetBool("IsAtk", false);


        yield return null;
    }

    IEnumerator CloseSufferAnimtor()//关闭奔跑受到攻击动画
    {
        
        yield return new WaitForSeconds(0.9f);
        msAnimator.SetBool("IsSufferAtk", false);
        yield return null;
    }

    public void MsAtkPlayer()//怪物攻击玩家
    {
        AudioPlay(atkClip);
        Debug.Log("调用伤害函数");
        PlayCharacter.Instance.Damge(Atk);

    }

    public void Init(Moster tmp_Monster)//初始化怪物属性
    {
        if (PlayGameMode.Instance.nowCheckpoint <= 3)
        {
            ID = tmp_Monster.ID;
            Name = tmp_Monster.Name;
            Route = tmp_Monster.Route;
            Hp = int.Parse(tmp_Monster.Hp);
            Atk = int.Parse(tmp_Monster.Atk);
            Dfs = int.Parse(tmp_Monster.Dfs);
            Money = int.Parse(tmp_Monster.Money);
            Exp = int.Parse(tmp_Monster.Exp);
            Level = int.Parse(tmp_Monster.Level);
            HpMax = Hp;
        }
        else if (PlayGameMode.Instance.nowCheckpoint > 3 && PlayGameMode.Instance.nowCheckpoint <= 7)
        {
            ID = tmp_Monster.ID;
            Name = tmp_Monster.Name;
            Route = tmp_Monster.Route;
            Hp = 2 * int.Parse(tmp_Monster.Hp);
            Atk = 2 * int.Parse(tmp_Monster.Atk);
            Dfs = 2 * int.Parse(tmp_Monster.Dfs);
            Money = 2 * int.Parse(tmp_Monster.Money);
            Exp = int.Parse(tmp_Monster.Exp);
            Level = int.Parse(tmp_Monster.Level);
            HpMax = 2 * Hp;
        }
        else
        {
            ID = tmp_Monster.ID;
            Name = tmp_Monster.Name;
            Route = tmp_Monster.Route;
            Hp = 3 * int.Parse(tmp_Monster.Hp);
            Atk = 3 * int.Parse(tmp_Monster.Atk);
            Dfs = 3 * int.Parse(tmp_Monster.Dfs);
            Money = 3 * int.Parse(tmp_Monster.Money);
            Exp = int.Parse(tmp_Monster.Exp);
            Level = int.Parse(tmp_Monster.Level);
            HpMax = 3 * Hp;
        }
        
    }

    public void AiAtk(int damge)//受到攻击函数
    {
        AudioPlay(demgeClip);
        aiealth.gameObject.SetActive(true);
        if (damge<=Dfs)
        {
            Hp--;
            return;
        }

        Hp = Hp - (damge - Dfs);

        if (Hp<=0)
        {
            AiDath();
        }

    }


    public void UpDateHealth()//更新血条
    {
        aiealth.gameObject.SetActive(true);
        aiealth.value = (float)Hp / HpMax;

        aiealthColor.color = Color.Lerp(redColor, greenColor, aiealth.value);
    }

    public void AiMove()
    {
        transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
    }

    public void AiDath()
    {
        AudioPlay(dropClip);
        PlayCharacter.Instance.AddMoney(Money);
        PlayCharacter.Instance.AddExps(Exp);

        var dathTx = Instantiate(aiDethTx, transform.position, Quaternion.identity);
        dathTx.AddComponent<AiTxDelete>();

        var drapProp = DiaoLuo.Instance.GreatProp();
        drapProp.transform.position = transform.position;//掉落物品
        drapProp.transform.position += Vector3.up * 0.3f;
        PlayGameMode.Instance.MosterDeth(nowPos);
        Destroy(gameObject);
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
