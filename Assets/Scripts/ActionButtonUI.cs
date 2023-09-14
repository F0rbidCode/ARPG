using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class ActionButtonUI : MonoBehaviour
{
    //set up referances to child action

    public Action action; //the action stored in the UI element

    [Header("Child Components")]
    public Image icon;
    public TextMeshProUGUI nameTag;
    public TextMeshProUGUI descriptionTag;
   


    public void SetAction (Action a) //set up the stored action
    {
        action = a; //set the passed in action to the referance
        if(action)//check that the UI element has an action as a child
        {
            if(nameTag)
            {
                nameTag.text = action.name; //set the actions name
            }
            if(descriptionTag)
            {
                descriptionTag.text = action.actionDescription; //set the actions description
            }
            if(icon)
            {
                icon.sprite = action.icon; //set the actions icon
                icon.color = action.color; //set the actions color
            }
            
        }
    }

    public void Start()
    {
        SetAction(action); //call set action passing in the child action
    }
}
