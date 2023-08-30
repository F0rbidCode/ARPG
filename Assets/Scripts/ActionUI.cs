using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionUI : MonoBehaviour
{
    //set up referances to be allocated inside unity editor

    public Action action;


    [Header("Child Comonents")]
    public Image icon;
    public TextMeshProUGUI nameTag;
    public TextMeshProUGUI descriptionTag;

    Player player;


    public void Init(Player p)
    {
        //store the player referance fo use in lambda function
        player = p;

        //find the button in the prefab
        Button button = GetComponentInChildren<Button>();
        if(button)
        {
            //button.onClick.AddListener(() => player.DoAction(action)); //FINISH COMPLETING THE ACTION LATER
        }
    }

    public void SetAction (Action a) //used to set up the action
    {
        action = a;
        if(action)
        {
            if(nameTag)
            {
                nameTag.text = action.actionName;
            }
            if(descriptionTag)
            {
                descriptionTag.text = action.actionDescription;
            }
            if(icon)
            {
                icon.sprite = action.icon;
                icon.color = action.color;
            }    
        }
    }

    public void Start()
    {
        SetAction(action);
    }
}
