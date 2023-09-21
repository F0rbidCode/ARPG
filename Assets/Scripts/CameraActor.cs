using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controls the main camera
/// 
/// Keeps the main camera focused on the player but taking into account the movement direction to allow for the player to have better visablity in the direction they are traveling
public class CameraActor : MonoBehaviour
{
    ///store a referance to the target (player)
    public Transform target;
    ///controles how fast the camera will move
    public float speed = 5.0f; 

    [HideInInspector]
    ///The offset position bassed on players movement direction
    public Vector3 offset;
    ///used to controle the distance between the player and the camera
    private Vector3 boom; 

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        ///Get the vector from the target to the player
        boom = this.transform.position - target.position;
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        ///Set our position to be the same relative to the player
        Vector3 target_pos = target.position + boom + offset;

        //this.transform.position = target_pos;

        ///smoothly move the camera to centre on the player
        this.transform.position = Vector3.Lerp(transform.position, target_pos, speed * Time.deltaTime);
    }
}
