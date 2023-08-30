using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false); //start the game object as hidden
    }
    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf); //toggle the game object
    }
}
