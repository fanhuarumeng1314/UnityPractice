using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class Img_Bj_Prop : UIBase {

    public Image img_Bj;//背景
    public Image nowPropImg;//现在的道具图片
    public Text nowPropNumber;//现在的道具数量

    public Btn_Bag upDateProp;

    public Transform propParnt;

    public GameObject tmp_UiAbout;

    public Sprite bj;
    public GameObject target;//拖动的物体
    public GameObject tmp_TargetParnt;//临时的父物体
    public bool isUse = false;

    public AudioClip btnAudio;

    void Awake()
    {
        btnAudio = Resources.Load("AudioClicp/按钮12_爱给网_aigei_com", typeof(AudioClip)) as AudioClip;

        tmp_TargetParnt = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu/Img_Bj_BackGround/Scroll View/Point_Prant").gameObject;
        nowPropImg = transform.Find("Btn_Prop/Img_Prop").GetComponent<Image>();
        nowPropNumber = transform.Find("Btn_Prop/Img_Prop/Txt_Number").GetComponent<Text>();

        upDateProp = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu/MaskBtn/AnimtorUi/Btn_Bag").GetComponent<Btn_Bag>();

        //img_Bj = GetComponent<Image>();
        //bj = Resources.Load("Photo/ui_gg_itemkuang4", typeof(Sprite)) as Sprite;
        //img_Bj.sprite = bj;
        target = gameObject.transform.Find("Btn_Prop").gameObject;

        propParnt = transform.parent;

        SetEventTrigger(target).onBeginDrag = OnBeginDrag;
        SetEventTrigger(target).onDrag = OnDrag;
        SetEventTrigger(target).onEndDrag = OnEndDrag;
        SetEventTrigger(target).onPointEnter = OnPointEntry;
        SetEventTrigger(target).onPointExit = OnPointExit;

        if (tmp_TargetParnt == null)
        {
            Debug.Log("获取父物体失败");
        }

        if (target ==  null)
        {
            Debug.Log("获取下级节点失败");
        }
    }


    private void Update()
    {
        if (isUse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioPlay();
                LookProp();
                Debug.Log("查看属性");
            }

            if (Input.GetMouseButtonDown(1))
            {
                AudioPlay();
                UseProp();
                Debug.Log(nowPropImg.sprite.name);
            }
        }
        
        
    }


    public void OnPointExit(PointerEventData eventData)
    {
        isUse = false;
    }


    public void OnPointEntry(PointerEventData eventData)
    {
        isUse = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        target.transform.parent = tmp_TargetParnt.transform;
        TuozhuaiMove(eventData);
    }


    public void OnDrag(PointerEventData eventData)
    {

        TuozhuaiMove(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        target.transform.parent = transform;
        target.transform.localPosition = Vector3.zero;
    }

    public void TuozhuaiMove(PointerEventData eventData)
    {
        RectTransform rt = target.GetComponent<RectTransform>();//获取自己的字物体的组件进行移动

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out globalMousePos))//返回世界空间坐标进行移动
        {
            rt.position = globalMousePos;
        }
    }

    public void UseProp()//使用道具
    {
        Prop tmp_Prop = null;
        foreach (var pair in PlayCharacter.Instance.bag)
        {
            if (pair.Key.Route== nowPropImg.sprite.name)//通过图片的名字进行索引背包道具
            {
                tmp_Prop = pair.Key;
                break;
            }
        }

        if (tmp_Prop.Type == "")
        {
            if (PlayCharacter.Instance.bag[tmp_Prop] > 0)
            {

                if (tmp_Prop.Name == "生命药剂")
                {
                    PlayCharacter.Instance.heathly += int.Parse(tmp_Prop.Dfs);
                    if (PlayCharacter.Instance.heathly >= PlayCharacter.Instance.heathlyMax)
                    {
                        PlayCharacter.Instance.heathly = PlayCharacter.Instance.heathlyMax;
                    }
                }
                else if (tmp_Prop.Name == "攻击药剂")
                {
                    PlayCharacter.Instance.atk += int.Parse(tmp_Prop.Atk);

                }
                else if (tmp_Prop.Name == "防御药剂")
                {
                    PlayCharacter.Instance.dfs += int.Parse(tmp_Prop.Dfs);
                }
                else
                {
                    return;
                }




                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    if (tmp_Prop != PropLibry.Instance.propTable["1022"])//如果不是炸弹
                    {
                        PlayCharacter.Instance.bag.Remove(tmp_Prop);
                        Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                        Debug.Log("删除道具");
                    }
                    
                }
            }
        }
        else if (tmp_Prop.Type == "Weapon") //如果道具类型是武器
        {
            
            if (PlayCharacter.Instance.nowWeapon == null)//如果玩家当前没有装备
            {
                PlayCharacter.Instance.nowWeapon = nowPropImg.sprite.name;
                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    PlayCharacter.Instance.bag.Remove(tmp_Prop);//删除如果出现错误，引用获取异常导致，遍历列表获取匹配项进行删除
                    Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                }
            }
            else
            {

                Prop nowProp = null;
                foreach (var pair in PropLibry.Instance.propTable)//有装备，从道具库获取道具引用
                {
                    if (pair.Value.Route== PlayCharacter.Instance.nowWeapon)
                    {
                        nowProp = pair.Value;
                        break;
                    }
                }

                PlayCharacter.Instance.nowWeapon = nowPropImg.sprite.name;
                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    PlayCharacter.Instance.bag.Remove(tmp_Prop);//删除如果出现错误，引用获取异常导致，遍历列表获取匹配项进行删除
                    Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                }

                foreach (var bagProp in PlayCharacter.Instance.bag)//此步注意，可能出现错误!=====判断背包是否存在已经装备的道具，有就改变数量
                {
                    if (bagProp.Key.Route== nowProp.Route)
                    {
                        PlayCharacter.Instance.bag[bagProp.Key]++;
                        upDateProp.UpdateBag();
                        return;
                    }
                }

                PlayCharacter.Instance.bag.Add(nowProp,1);//没有就进行添加
                AddProp(nowProp);//添加图标

            }

            PlayCharacter.Instance.UpdeteData();//刷新玩家数据
        }
        else if (tmp_Prop.Type == "Armor")
        {
            if (PlayCharacter.Instance.nowArmor == null)//如果玩家当前没有装备
            {
                PlayCharacter.Instance.nowArmor = nowPropImg.sprite.name;
                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    PlayCharacter.Instance.bag.Remove(tmp_Prop);//删除如果出现错误，引用获取异常导致，遍历列表获取匹配项进行删除
                    Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                }
            }
            else
            {

                Prop nowProp = null;
                foreach (var pair in PropLibry.Instance.propTable)//有装备，从道具库获取道具引用
                {
                    if (pair.Value.Route == PlayCharacter.Instance.nowArmor)
                    {
                        nowProp = pair.Value;
                        break;
                    }
                }

                PlayCharacter.Instance.nowArmor = nowPropImg.sprite.name;
                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    PlayCharacter.Instance.bag.Remove(tmp_Prop);//删除如果出现错误，引用获取异常导致，遍历列表获取匹配项进行删除
                    Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                }

                foreach (var bagProp in PlayCharacter.Instance.bag)//此步注意，可能出现错误!=====判断背包是否存在已经装备的道具，有就改变数量
                {
                    if (bagProp.Key.Route == nowProp.Route)
                    {
                        PlayCharacter.Instance.bag[bagProp.Key]++;
                        upDateProp.UpdateBag();
                        return;
                    }
                }

                PlayCharacter.Instance.bag.Add(nowProp, 1);//没有就进行添加
                AddProp(nowProp);//添加图标

            }

            PlayCharacter.Instance.UpdeteData();//刷新玩家数据

        }
        else if (tmp_Prop.Type == "Ring")
        {
            if (PlayCharacter.Instance.nowRing == null)//如果玩家当前没有装备
            {
                PlayCharacter.Instance.nowRing = nowPropImg.sprite.name;
                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    PlayCharacter.Instance.bag.Remove(tmp_Prop);//删除如果出现错误，引用获取异常导致，遍历列表获取匹配项进行删除
                    Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                }
            }
            else
            {

                Prop nowProp = null;
                foreach (var pair in PropLibry.Instance.propTable)//有装备，从道具库获取道具引用
                {
                    if (pair.Value.Route == PlayCharacter.Instance.nowRing)
                    {
                        nowProp = pair.Value;
                        break;
                    }
                }

                PlayCharacter.Instance.nowRing = nowPropImg.sprite.name;
                PlayCharacter.Instance.bag[tmp_Prop]--;
                nowPropNumber.text = PlayCharacter.Instance.bag[tmp_Prop].ToString();
                if (PlayCharacter.Instance.bag[tmp_Prop] == 0)
                {
                    PlayCharacter.Instance.bag.Remove(tmp_Prop);//删除如果出现错误，引用获取异常导致，遍历列表获取匹配项进行删除
                    Destroy(gameObject);//道具数量为零，删除掉图标，从背包字典删除
                }

                foreach (var bagProp in PlayCharacter.Instance.bag)//此步注意，可能出现错误!=====判断背包是否存在已经装备的道具，有就改变数量
                {
                    if (bagProp.Key.Route == nowProp.Route)
                    {
                        PlayCharacter.Instance.bag[bagProp.Key]++;
                        upDateProp.UpdateBag();
                        return;
                    }
                }

                PlayCharacter.Instance.bag.Add(nowProp, 1);//没有就进行添加
                AddProp(nowProp);//添加图标

            }
            PlayCharacter.Instance.UpdeteData();//刷新玩家数据

        }
    }

    public void AddProp(Prop addProp)
    {
        Debug.Log("添加图标");
        var tmp_Prop = UiManger.Instance.GreatUI<Img_Bj_Prop>(propParnt);

        Image img_Prop = tmp_Prop.transform.Find("Btn_Prop/Img_Prop").GetComponent<Image>();
        Text prop_Number = tmp_Prop.transform.Find("Btn_Prop/Img_Prop/Txt_Number").GetComponent<Text>();

        Sprite tmp_ImgProp = Resources.Load("Photo/" + addProp.Route, typeof(Sprite)) as Sprite;
        img_Prop.sprite = tmp_ImgProp;
        prop_Number.text = "1";
    }

    public void LookProp()//查看属性
    {
        Prop tmp_Prop = null;
        foreach (var pair in PlayCharacter.Instance.bag)
        {
            if (pair.Key.Route == nowPropImg.sprite.name)//通过图片的名字进行索引背包道具
            {
                tmp_Prop = pair.Key;
                break;
            }
        }

        tmp_UiAbout = UiManger.Instance.GreatUI<Img_About>(UiManger.Instance.UIRoot.transform.Find("Canvas_Menu"));

        var aboutScprit = tmp_UiAbout.GetComponent<Img_About>();
        aboutScprit.PrintShuXing(tmp_Prop);
    }


    IEnumerator DeletAudio(GameObject audioObj)
    {
        yield return new WaitForSeconds(1f);
        Destroy(audioObj);
        yield return null;
    }

    public void AudioPlay()
    {
        GameObject tmp_Audio = new GameObject();
        var tmp_AudioPlay = tmp_Audio.AddComponent<AudioSource>();
        tmp_AudioPlay.clip = btnAudio;
        tmp_AudioPlay.Play();
        StartCoroutine(DeletAudio(tmp_Audio));
    }

}
