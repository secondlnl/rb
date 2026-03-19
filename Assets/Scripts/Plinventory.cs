using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plinventory : MonoBehaviour
{
    public List<Item> Inventory = new List<Item>();

    [SerializeField] private GameObject slot, inventoryPanel, lanternBut, SwordBut;
    public bool haveLantern;
    private PlayerController Plc;
    public string currentItem = "";
    private Image slotImg;
    [SerializeField] private Sprite sword, lantern;
    private Color invis;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Plc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        slotImg = GetComponent<Image>();
        slotImg.enabled = false;
        slot.GetComponent<Image>().enabled = false;

    }

    /// <summary>
    /// Checks if the slot is active
    /// </summary>
    /// <returns>true if the slot is active, false if the slot is not active</returns>
    public bool isActive()
    {
        return slot.activeInHierarchy;
    }
    public void Activate()
    {
        slotImg.enabled = true;
        currentItem = "empty";
        slot.GetComponent<Image>().enabled = true;
        slotImg.color = invis;
    }
    public void Add(string str, Sprite sprite)
    {
        if (string.IsNullOrEmpty(currentItem))
        {
            currentItem = str;
        }
        if (str.ToLower() == "lantern")
        {
            haveLantern = true;
            GetComponentInParent<PlayerController>().Lit(true);
        }
        Debug.Log(str);
        Item tmp = new Item(str, sprite);
        Inventory.Add(tmp);
        slotImg.color = Color.white;
        slotImg.sprite = tmp.GetSprite();

    }
    public void UpdateInventory()
    {
        // if (Plc.haveSword && !SwordBut.activeInHierarchy)
        // {
        //     SwordBut.SetActive(true);
        // }

        if (!haveLantern && lanternBut.activeInHierarchy)
        {
            lanternBut.SetActive(false);
        }

        if (haveLantern && !lanternBut.activeInHierarchy)
        {
            lanternBut.SetActive(true);
        }
    }
    public void SetCurrentItem(string str, Sprite sprite)
    {
        switch (str)
        {
            case "empty":
                currentItem = "empty";
                slotImg.sprite = null;
                slotImg.color = invis;
                GetComponentInParent<PlayerController>().Lit(false);
                break;
            default:
                currentItem = str;
                slotImg.sprite = sprite;
                slotImg.color = Color.white;
                break;
        }
    }
    public void SetCurrentItemSword()
    {
        SetCurrentItem("Sword", sword);
    }
    public void SetCurrentItemLantern()
    {
        GetComponentInParent<PlayerController>().Lit(true);
        SetCurrentItem("Lantern", lantern);

    }
}
