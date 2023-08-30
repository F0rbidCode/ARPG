using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugControler : MonoBehaviour
{
    public Slider slider;

    //float used to update and set the value on the attached slider
    public float _floatValue;

    public float floatValue
    {
        get { return _floatValue; }
        set
        {
            //update the internal variable
            _floatValue = value;

            //make sure controles match the current value
            if(slider)
            {
                slider.value = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //add a listener to the slider
        if(slider)
        {
            slider.onValueChanged.AddListener((float value) => { floatValue = value; });
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
