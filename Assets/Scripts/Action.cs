using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ActionItem", menuName = "GUI/ActionItem", order = 1)] //add the scriptable object to the asset menue
///Scriptable Object to hold pre defined Actions
///
///This scriptable object holds information about each action that is created in the unity editor
public class Action : ScriptableObject
{
    ///set name of action
    public string actionname;
    ///set description for action
    public string actionDescription;
    /// set actions icon
    public Sprite icon;
    ///set actions colour
    public Color color = Color.white; 
}

    
