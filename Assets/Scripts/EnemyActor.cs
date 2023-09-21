using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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

    ///used to control enemy movement
    private bool canMove = true;

    //////////////////////////////////
    ///FOR ANIMATION
    //////////////////////////////////
    ///store a referance to the animator componant
    private Animator animator;


    /// Start is called before the first frame update
    void Start()
    {
        ///find the player actor in scene and set it as a referance to the variable
        player = GameObject.FindObjectOfType<PlayerActor>();

        ///get the animator comonant
        animator = GetComponent<Animator>();
    }

    /// Update is called once per frame
    void Update()
    {        
        if (canMove)
        {
            ///calculate the vector to the player
            toTarget = player.transform.position - this.transform.position;
            ///normalise that vector
            toTarget = toTarget.normalized;
            ///rotate the enemy to face the player
            this.transform.forward = toTarget;
            ///move the enemy towards the target
            this.transform.position += toTarget * Speed * Time.deltaTime;
            /// Set the IsWalking bool to true to start the walkign animation
            animator.SetBool("isWalking", true);
        }
        //else
        //{
        //    ///move the enemy towards the target
        //    this.transform.position -= toTarget * Speed * Time.deltaTime;
        //}

    }   


    /// detect collisions
    /// 
    /// Check if the enemy has made a collision with the player and deal the apporpiate amount of damage
    /// <param name="Collision hit"></param>
    private void OnTriggernEnter(Collider hit)
    {
        //this.transform.position -= toTarget * Speed * Time.deltaTime;
        ///check if the collision was with the player
        if (hit.tag == "Player")
        {
            ///stop the enemy moving
            canMove = false;

            /// Set the IsWalking bool to true to start the walkign animation
            animator.SetBool("isWalking", false);

            ///trigger the attacking animation
            animator.SetTrigger("isAttacking");

        }
        //else if (hit.collider.tag != "Floor")
        //{
        //    this.transform.position -= toTarget * Speed * Time.deltaTime;
        //}
    }

    ///Repeatedly attack player if in collider
    private void OnTriggerStay(Collider hit)
    {
        ///check if the collision was with the player
        if (hit.tag == "Player")
        {
            ///stop the enemy moving
            canMove = false;

            /// Set the IsWalking bool to true to start the walkign animation
            animator.SetBool("isWalking", false);

            ///trigger the attacking animation
            animator.SetTrigger("isAttacking");

        }
        //else if (hit.collider.tag != "Floor")
        //{
        //    this.transform.position -= toTarget * Speed * Time.deltaTime;
        //}
    }
    

    ///used to enable movement when collision ended
    private void OnTriggerExit(Collider hit)
    {
        ///check if the collision that ended was with the player
        if (hit.tag == "Player")
        {
            ///enable movement
            canMove = true;
        }

    }

    ///Called by the Enemy attack animation to damage playerActor
    private void DoDamage()
    {
        ///reduce the players health be the enemy damage
        player.Health -= Damage;
        // Debug.Log("player took damage");
        ///Call the PlayerActor::onPlayerDamaged() function
        player.onPlayerDamaged();
    }


    /// detect collisions
    /// 
    /// Check if the enemy has made a collision with the player and deal the apporpiate amount of damage
    /// <param name="Collision hit"></param>
    //private void OnCollisionEnter(Collision hit)
    //{
    //    //this.transform.position -= toTarget * Speed * Time.deltaTime;
    //    ///check if the collision was with the player
    //    if (hit.collider.tag == "Player") 
    //    {
    //        ///stop the enemy moving
    //        canMove = false;

    //        /// Set the IsWalking bool to true to start the walkign animation
    //        animator.SetBool("isWalking", false);

    //        ///trigger the attacking animation
    //        animator.SetTrigger("isAttacking");

    //    }
    //    //else if (hit.collider.tag != "Floor")
    //    //{
    //    //    this.transform.position -= toTarget * Speed * Time.deltaTime;
    //    //}
    //}

    ///Repeatedly attack player if in collider
    //private void OnCollisionStay(Collision hit)
    //{
    //    ///check if the collision was with the player
    //    if (hit.collider.tag == "Player")
    //    {
    //        ///stop the enemy moving
    //        canMove = false;

    //        /// Set the IsWalking bool to true to start the walkign animation
    //        animator.SetBool("isWalking", false);

    //        ///trigger the attacking animation
    //        animator.SetTrigger("isAttacking");

    //    }
    //    //else if (hit.collider.tag != "Floor")
    //    //{
    //    //    this.transform.position -= toTarget * Speed * Time.deltaTime;
    //    //}
    //}

    /////used to enable movement when collision ended
    //private void OnCollisionExit(Collision hit)
    //{
    //    ///check if the collision that ended was with the player
    //    if (hit.collider.tag == "Player")
    //    {
    //        ///enable movement
    //        canMove = true;
    //    }

    //}
}
