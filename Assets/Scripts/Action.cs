using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ActionItem", menuName = "GUI/ActionItem", order = 1)] //used to add Action item into the 'create new' menu
public class Action : ScriptableObject
{
    public string actionName;
    public string actionDescription;

    public Sprite icon;
    public Color color = Color.white;
}
