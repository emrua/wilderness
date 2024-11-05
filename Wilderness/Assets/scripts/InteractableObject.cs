using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool PlayerInRange;
 
    public string GetItemName()
    {
        return ItemName;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && PlayerInRange && SelectionManager.instance.onTarget)
        {
            if(!InventorySystem.Instance.CheckIfFull())
            {
            InventorySystem.Instance.AddToInventory(ItemName);
            Destroy(gameObject);
            }
            else
            {
                Debug.Log("Invenotry is full");
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}