using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class SelectionManager : MonoBehaviour
{
 
    public static SelectionManager instance {get; set;}

    public bool onTarget;
    public GameObject interaction_Info_UI;
    TMP_Text interaction_text;
 
    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();
    }
 
    public void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
          instance = this;  
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.PlayerInRange)
            {   
                onTarget = true;

                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else 
            { 
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }
        }
            else 
            { 
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }
    
 
        }
    }

