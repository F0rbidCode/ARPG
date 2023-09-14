using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ActionItem", menuName = "GUI/ActionItem", order = 1)] //add the scriptable object to the asset menue
public class Action : ScriptableObject
{
    public string actionname; //set name of action
    public string actionDescription; //set description for action

    public Sprite icon; // set actions icon
    public Color color = Color.white; //set actions colour
}

    
