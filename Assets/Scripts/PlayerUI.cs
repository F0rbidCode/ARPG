using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
    public Player player; //store the link to the player information

    public Slider hpSlider; //used to store the hp slider information
    public Slider stamSlider; //used to store the stamina slider information
    public Slider manaSlider; //used to store the mana slider information

    public TMP_Text level; //used to store and update level information
    public string formatLevel = "00";
    
    public TMP_Text exp; //used to store and update exp information
    public string formatExp = "0000";


    //used for linking up the debug controler
    public DebugControler hpControler;
    public DebugControler stamControler;
    public DebugControler manaControler;

    public DebugTextControler levelControler;
    public DebugTextControler expControler;


    // Start is called before the first frame update
    void Start()
    {
        //add a listener to update the health value changes
        hpControler.slider.onValueChanged.AddListener(OnHealthValueChanged);
        //add a listener to update the stamina value changes
        stamControler.slider.onValueChanged.AddListener(OnStaminaValueChanged);
        //add a listener to update the mana value changes
        manaControler.slider.onValueChanged.AddListener(OnManaValueChanged);

        //add a listener to update the level value changes
        levelControler.inputField.onValueChanged.AddListener(OnLevelUp);
        //add a listener to update the exp value changes
        expControler.inputField.onValueChanged.AddListener(OnExpValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //create a function to update values and display when player health changed
    public void OnHealthValueChanged (float value)
    {
        player.Health = value;
        hpSlider.value = value;
    }

    //create a function to update values and display when player stamina changed
    public void OnStaminaValueChanged(float value)
    {
        player.Stamina = value;
        stamSlider.value = value;
    }

    //create a function to update values and display when player mana changed
    public void OnManaValueChanged(float value)
    {
        player.Mana = value;
        manaSlider.value = value;
    }

    //create a function to update values and display when the level changes
    public void OnLevelUp(string value)
    {
        int parsedValue;
        if (int.TryParse(value, out parsedValue))
        {
            player.Level = parsedValue;
            level.text = parsedValue.ToString(formatLevel);
        }
       
    }
    //create a function to update values and display when the exp changes
    public void OnExpValueChanged(string value)
    {
        int parsedValue;
        if (int.TryParse(value, out parsedValue))
        {
            player.exp = parsedValue;
            exp.text = parsedValue.ToString(formatExp);
        }
    }
}
