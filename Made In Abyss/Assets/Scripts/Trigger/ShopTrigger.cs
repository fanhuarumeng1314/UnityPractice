using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour {

    public GameObject shop;
    public Transform shopParnt;
   

    private void OnTriggerEnter(Collider other)
    {
        var plar = other.gameObject.GetComponent<PlayCharacter>();

        if (plar)
        {
            shopParnt = UiManger.Instance.UIRoot.transform.Find("Canvas_Menu");
            shop = UiManger.Instance.GreatUI<Img_Shop_Bg>(shopParnt);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var plar = other.gameObject.GetComponent<PlayCharacter>();
        if (plar)
        {
            if (shop!=null)
            {
                Destroy(shop);
            }
        }
    }
}
