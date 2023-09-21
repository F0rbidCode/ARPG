using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controls the Enemy actors
/// 
/// Moves the Enemy actors to the PlayerActor and checks for collisions with the PlayerActor to cause damage
public class EnemyActor : MonoBehaviour
{
    
    /// store a referance to the target (player)    
    private PlayerActor player;
    /// store the movement speed of the enemy
    public float Speed = 4.0f; 
    /// store the Damage delt by the enemy
    public float Damage = 10;

    ///Stores the vector to the target (player)
    private Vector3 toTarget;

    /// Start is called before the first frame update
    void Start()
    {
        ///find the player actor in scene and set it as a referance to the variable
        player = GameObject.FindObjectOfType<PlayerActor>();
    }

    /// Update is called once per frame
    void Update()
    {
        ///calculate the vector to the player
        toTarget = player.transform.position - this.transform.position;
        ///normalise that vector
        toTarget = toTarget.normalized;
        ///move the enemy towards the target
        this.transform.position += toTarget * Speed * Time.deltaTime;       
                
    }

    /// detect collisions
    /// 
    /// Check if the enemy has made a collision with the player and deal the apporpiate amount of damage
    /// <param name="Collision hit"></param>
    private void OnCollisionEnter(Collision hit)
    {
        ///check if the collision was with the player
        if (hit.collider.tag == "Player") 
        {
            ///reduce the players health be the enemy damage
            player.Health -= Damage; 
           // Debug.Log("player took damage");
           ///Call the PlayerActor::onPlayerDamaged() function
            player.onPlayerDamaged();
        }
    }
}
