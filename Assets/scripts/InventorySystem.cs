using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class InventorySystem : MonoBehaviour
{


    public GameObject ItemInfoUI;
 
   public static InventorySystem Instance { get; set; }
 
    public GameObject inventoryScreenUI;


    public List<GameObject> slotList = new List<GameObject>();


    public List<string> itemList = new List<string>();


    private GameObject itemToAdd;


    private GameObject whatSlotToEquip;


    public bool isOpen;


    //public bool isFull;
 
 
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
 
 
    void Start()
    {
        isOpen = false;
        PopulateSlotList();


    }
 
    public void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform)
        {
            if(child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }
 
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
 
                Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }




    public void AddToInventory(string ItemName)
    {
        whatSlotToEquip = FindNextEmptySlot();


        itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);


        itemList.Add(ItemName);
       
    }
   
    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }


    public bool CheckIfFull()
    {
        int counter = 0;


        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }


        if(counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;


        for(var i = slotList.Count -1; i >= 0; i--)
        {
            if(slotList[i].transform.childCount > 0)
            {
                if(slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);


                    counter -= 1;
                }
            }
        }
    }




    public void RecalculateList()
    {
        itemList.Clear();


        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");


                itemList.Add(result);
            }
        }
    }


}
