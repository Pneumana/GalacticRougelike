using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mousedOver = false;
    public GameObject additionalInfo;
    public GameObject buy;
    private float yoffset = -4;

    public bool bought;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //behavior for mouse not being pressed
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            var buypos = Vector2.Lerp(buy.transform.localPosition, new Vector2(0, -4), Time.deltaTime * 6);
            if (buypos.y < -4)
                buypos.y = -4;
            buy.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1 - (buy.transform.localPosition.y / -4));
            buy.transform.localPosition = buypos;
            if (mousedOver)
            {
                var newpos = Vector2.Lerp(additionalInfo.transform.localPosition, new Vector2(0, 0), Time.deltaTime * 6);
                if (newpos.y > 0)
                    newpos.y = 0;
                additionalInfo.transform.localPosition = newpos;
            }
            else
            {
                var newpos = Vector2.Lerp(additionalInfo.transform.localPosition, new Vector2(0, -4), Time.deltaTime * 6);
                if (newpos.y < -4)
                    newpos.y = -4;
                additionalInfo.transform.localPosition = newpos;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //lowers the additional info
            var newpos = Vector2.Lerp(additionalInfo.transform.localPosition, new Vector2(0, -4), Time.deltaTime * 6);
            if (newpos.y < -4)
                newpos.y = -4;
            additionalInfo.transform.localPosition = newpos;

            if (mousedOver)
            {
                var buypos = Vector2.Lerp(buy.transform.localPosition, new Vector2(0, 0), Time.deltaTime * 6);
                if (buypos.y > 0)
                {
                    buypos.y = 0;
                    OnPurchase();
                }
                    
                buy.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1 - (buy.transform.localPosition.y / -4));
                buy.transform.localPosition = buypos;
            }
            else
            {
                var buypos = Vector2.Lerp(buy.transform.localPosition, new Vector2(0, -4), Time.deltaTime * 6);
                if (buypos.y < -4)
                    buypos.y = -4;
                buy.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1 - (buy.transform.localPosition.y / -4));
                buy.transform.localPosition = buypos;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mousedOver = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mousedOver = false;
    }
    void OnPurchase()
    {
        //check for money here

        //if you have money, subtract it here
        bought = true;
    }
}
