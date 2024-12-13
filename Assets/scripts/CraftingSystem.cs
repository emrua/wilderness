using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingSystem : MonoBehaviour
{


    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;


    public List<string> inventoryItemList = new List<string> ();


    //Category Buttons
    Button toolsBTN;


    //Craft Buttons
    Button craftAxeBTN;


    //Requirement Text
    Text AxeReq1, AxeReq2;


    public bool isOpen;


    //All Blueprints






    public static CraftingSystem Instance { get; set; }




    private void Awake()
    {
        if (Instance !=null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }




    // Start is called before the first frame update
    void Start()
    {


        isOpen = false;


        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button> ();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });


        // AXE
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();


        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(); });


    }


   
    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive (false);
        toolsScreenUI.SetActive (true);
    }




    void CraftAnyItem()
    {


        //add item into inventory




        //remove resources from inventory












    }






    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();


        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {


            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;


        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen) {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
        }
    }
        public void RefreshNeededItems()
        {
            int stone_count = 0;
            int stick_count = 0;


            inventoryItemList = InventorySystem.Instance.itemList;


            foreach (string itemName in inventoryItemList)
            {
                switch(itemName)
                {
                    case "Stone":
                    stone_count += 1;
                    break;
                    case "Stick":
                    stick_count += 1;
                    break;
                }
            }


        }
}
