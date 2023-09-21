using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;
using Etouch = UnityEngine.InputSystem.EnhancedTouch;

/// set the start position of the joystick on touch
/// 
/// set the start position of the joy stick on touch and activate it on the screen
public class JoystickSetPosition : MonoBehaviour
{
    ///Referance to the onscreen joystick
    [SerializeField]
    public GameObject joystick;

    ///Referance to to the players finger
    private Finger playerFinger;

    ///called when the game object is enabled
    private void OnEnable()
    {
        ///Enable touch input
        EnhancedTouchSupport.Enable();
        ///add our own event to the onFingerDown event
        Etouch.Touch.onFingerDown += Touch_onFingerDown;
        ///add our own event to the onFingerUp event
        Etouch.Touch.onFingerUp += Touch_onFingerUp;
    }

    //called when the game object is disabled
    private void OnDisable()
    {
        ///remove our own event from the onFingerDown event
        Etouch.Touch.onFingerDown -= Touch_onFingerDown;
        ///remove our own event from the onFingerUp event
        Etouch.Touch.onFingerUp -= Touch_onFingerUp;
        ///Disable touch input
        EnhancedTouchSupport.Disable();
    }
    /// Will be caleld when a touch input is detected
    /// 
    /// When touch input is detected enable and set the anchor of the joystick
    /// <param name="Finger playerFinger"></param>
    private void Touch_onFingerDown(Finger touchedFinger)
    {
        //Debug.Log("finger Down");
        
        ///if the player touches the left hand side of the screen
        if (playerFinger == null && touchedFinger.screenPosition.x <= Screen.width /2f )
        {
            playerFinger = touchedFinger;
            ///set the joystick to active
            joystick.gameObject.SetActive(true);        
            ///set the Anchor position of the joystick
            joystick.transform.position = ClampStartPosition(playerFinger.screenPosition);
            //joystick.GetComponent<RectTransform>().anchoredPosition = playerFinger.screenPosition;
        }
    }

    /// Clamps the joystick on screen
    /// 
    /// stops the joystick from being able to be set on the edge oft the screen
    /// <param name="Vector2 startPosition"></param>
    /// <returns>Vector2 startPostion</returns>
    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        ///if the start position x is less then half the size of the joystick
        if (startPosition.x < joystick.GetComponent<RectTransform>().sizeDelta.x / 2)
        {
            ///set the start positions x to equal half the x size of the joystick
            startPosition.x = joystick.GetComponent<RectTransform>().sizeDelta.x / 2;
        }
        ///if the start postition y is less then half of the size of the joystick
        if (startPosition.y < joystick.GetComponent<RectTransform>().sizeDelta.y / 2)
        {
            ///set the start position y to equal half the y size of the joystick
            startPosition.y = joystick.GetComponent<RectTransform>().sizeDelta.y / 2;
        }
        ///if the start position y is grater then the screen height minus half the size of the joystick
        else if (startPosition.y > Screen.height - joystick.GetComponent<RectTransform>().sizeDelta.y / 2)
        {
            //set the start position y to equal screen hight minus half the y size of hte joystick
            startPosition.y = Screen.height - joystick.GetComponent<RectTransform>().sizeDelta.y / 2;
        }
        ///return the start positition
        return startPosition;
    }

    /// Will be caleld when a touch input has ended
    /// 
    /// When touch input ends disable the joystick
    /// <param name="Finger playerFinger"></param>
    private void Touch_onFingerUp(Finger lostFinger)
    {
        if(lostFinger == playerFinger)
        {
             ///set player finger to null
        playerFinger = null;
        ///deactivate joystick
        joystick.gameObject.SetActive(false);
        }
       
    }


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
