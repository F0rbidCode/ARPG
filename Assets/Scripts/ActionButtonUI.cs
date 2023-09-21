using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

/// Script for filling the Action Button that is part of the GUI
/// 
/// The Action Button that is displayed as part of the UI will have an Action(scriptable object) attached to it as a child, this will then be used to populate the details of the action button
public class ActionButtonUI : MonoBehaviour
{
    //set up referances to child action

    ///the action stored in the UI element
    public Action action; 

    [Header("Child Components")]
    ///Action icon
    public Image icon;
    ///Action name
    public TextMeshProUGUI nameTag;
    ///Action description
    public TextMeshProUGUI descriptionTag;


    /// Sets the details form the child Action to the action button
    /// 
    /// Takes the details attached to the child action and uses them to fill out the perapiters on the actiun button
    /// <param name=" Action a"></param>
    public void SetAction (Action a) //set up the stored action
    {
        action = a; //set the passed in action to the referance
        if(action)//check that the UI element has an action as a child
        {
            if(nameTag)
            {
                nameTag.text = action.name; ///set the actions name
            }
            if(descriptionTag)
            {
                descriptionTag.text = action.actionDescription; ///set the actions description
            }
            if(icon)
            {
                icon.sprite = action.icon; ///set the actions icon
                icon.color = action.color; ///set the actions color
            }
            
        }
    }

    public void Start()
    {
        SetAction(action); ///call set action passing in the child action
    }
}
