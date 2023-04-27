using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mousedOver = false;
    public GameObject additionalInfo;
    public GameObject buy;
    public GameObject soldText;
    public GameObject display;
    public GameObject title;
    private float yoffset = -4;
    public int moneycost = 100;
    public int crystalcost = 0;
    public int exoticcost = 0;
    public bool bought;

    public string sellingItem;
    public int itemType;
    private string[] loottable = new string[] { "Basic", "Railgun", "Storm", "Minigun", "Rocket Launcher", "Shield", "Hull" };
    private string[] shiptable = new string[] { "Avocet", "Ghost", "Meatship", "Reaper"};
    private string[] contracts = new string[] { "Dude", "OtherGuy" };
    public ShopkeeperDialouge keeperDialouge;
    //i could do a fetch damage from prefab type thing for each entry. this makes it so this part only needs to have flavortext written.
    //checks each entry compared to the loottable[] to get the prefab name, then gets the spawned projectile, and from there the damage can be got.
    //stats and descriptions probably need to be set to different objectss
    private string[] guninfo = new string[] {
        "Damage: 5 \nRate of Fire:1\n0 Pierces\n0s Charge Time\n\nA reliable, standard kinetic slug cannon.",
        "Damage: 20 \nRate of Fire:0.5\nInfinite Pierces\n1s Charge Time\n\nShoots a high velocity slug using magnets.",
        "Damage: 5 \nRate of Fire:0.5\n4 Pierces\n0s Charge Time\n\nShoots an  arcing bolt of electricity.",
        "Damage: 1 \nRate of Fire:4\n0 Pierces\n0s Charge Time\n\nIncredibly high rate of fire.",
        "Damage: 20 \nRate of Fire:0.33\n0 Pierces\n0s Charge Time\n\nFires an accelerating missle",
        "Increases maximum shield by 5",
        "Increases maximum health by 15"
    };
    private string[] shipinfo = new string[] {
        "Health: \nSpeed: \nAgility: \nShield\n\nA small ship used frequently by pirates",
        "Health: \nSpeed: \nAgility: \nShield\n\nA medium ship with many guns",
        "Health: \nSpeed: \nAgility: \nShield\n\nA ship nicknamed after it's flesh-like plating",
        "Health: \nSpeed: \nAgility: \nShield\n\nLarge ship that's basically a space station",

    };
    // Start is called before the first frame update
    void Start()
    {
        //get damage from attack
        keeperDialouge = GameObject.Find("ShopkeeperText").GetComponent<ShopkeeperDialouge>();
        //weapon
        if(itemType == 0)
        {
            var item = Random.Range(0, loottable.Length);
            sellingItem = loottable[item];
            display.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Textures/guns/" + sellingItem);
            additionalInfo.transform.Find("WeaponInfo").gameObject.GetComponent<TextMeshProUGUI>().text = guninfo[item];
            title.GetComponent<TextMeshProUGUI>().text = sellingItem;
        }
        //ships
        if(itemType == 1)
        {
            var item = Random.Range(0, shiptable.Length);
            sellingItem = shiptable[item];
            display.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Textures/ships/" + sellingItem);
            additionalInfo.transform.Find("WeaponInfo").gameObject.GetComponent<TextMeshProUGUI>().text = shipinfo[item];
            title.GetComponent<TextMeshProUGUI>().text = sellingItem;
        }
        //contracts
        if(itemType == 2)
        {
            display.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load("Textures/contracts/" + sellingItem) as Sprite;
        }
        Debug.Log(shiptable.Count());
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
        if (Input.GetKey(KeyCode.Mouse0) && !bought)
        {
            //lowers the additional info
            var newpos = Vector2.Lerp(additionalInfo.transform.localPosition, new Vector2(0, -4), Time.deltaTime * 6);
            if (newpos.y < -4)
                newpos.y = -4;
            additionalInfo.transform.localPosition = newpos;

            if (mousedOver)
            {
                var buypos = Vector2.Lerp(buy.transform.localPosition, new Vector2(0, 0), Time.deltaTime * 6);
                
                if (buypos.y > -0.01f)
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
        var currentGamedata = DataPersistanceManager.Instance.gameData;
        //check for money here
        Debug.Log(currentGamedata.money);
        if (currentGamedata.money >= moneycost)
        {
            if (currentGamedata.crystals >= crystalcost)
            {
                if (currentGamedata.exoticmatter >= exoticcost)
                {
                    Debug.Log("bought new item");
                    currentGamedata.money -= moneycost;
                    currentGamedata.crystals -= crystalcost;
                    currentGamedata.exoticmatter -= exoticcost;
                    bought = true;
                    display.GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.5f, 0.5f);
                    soldText.GetComponent<TextMeshProUGUI>().enabled = true;
                    keeperDialouge.SayThankYouMechanic();
                    //weapons
                    if(itemType == 0)
                    {
                        currentGamedata.weapons.Add(sellingItem);
                    }
                    //ships
                    if(itemType == 1)
                    {
                        //reload equipped prefab
                        currentGamedata.shipframe = sellingItem;
                        GameObject.Find("SpawnPrefab").GetComponent<SpawnEquippablePrefab>().ReloadShip();
                    }
                }
                else
                {
                    //shakes the exotic counter
                }
            }
            else
            {
                //also shakes the crystal counter and meep-merp
            }
        }
        else
        {
            //shake the money counter and meep-merp
            NotEnoughMoney();
        }
        //if you have money, subtract it here
        
    }
    void NotEnoughMoney()
    {
        keeperDialouge.YouBroke();
    }
}
