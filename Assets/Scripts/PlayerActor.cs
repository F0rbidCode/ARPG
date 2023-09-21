using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Serialization;

/// Controls the PlayerActor
/// 
/// Handles all player inputs aswell as keeps track of player variables such as health, stamina, mana and movement speed. Also handles triggers for the animation controller and triggering GameOver event
public class PlayerActor : MonoBehaviour
{
    ///initialise the player controller variable
    private CharacterController controller;

    ///set the players movement speed
    public float speed = 5.0f;
    ///set the speed the player can dodge at
    public float dodgeSpeed = 10f; 

    ///Hold a referance to the PlayerInput component
    private PlayerInput _playerInput;
    /////stores the last direction the player moved in
    //private float _playersMovementDirection;

    


    /////////////////////////////////////////////////
    ///Player Stats
    /////////////////////////////////////////////////
    [Header ("player stats")]
    ///stores the players health
    public float Health = 100;  
    ///stores the players stamina
    public float Stamina = 100;  
    ///stores the players mana
    public float Mana = 100;

    ///stores the players level
    public int Level = 1;
    ///stores the players experiance points
    public int Exp = 0;

    //public float projectileSpeed;


    ///set and switch between the available weapons 
    public enum SpellType 
    {
        SPELL_FIREBALL,
    }
    [Header("player Skills")]
    ///used to select weapon type
    public SpellType spell_type; 

    ///the amount of mana a spell consumes
    public float ManaConsumption = 5;
    ///the ammount of stamina an attack or roll consumes
    public float StamConsumption = 5;

    ///the regeneration rate of mana
    public float ManaRegenRate = 1;
    ///the regeneration rate of stamina
    public float StamRegenRate = 2;

    [Header("Game Objects")]
    ///referance to the projectile prefab
    public GameObject projectile;
    ///referance to the slash prefab
    public GameObject slash;
    ///referance to the game camera
    public CameraActor camera_actor;
    ///referance to the menu game object
    public GameObject menu;
    ///referance to the spawn location of spells
    public GameObject spawnPoint;

    ///stores the vector the player last moved in
    private Vector3 move_direction;


    //////////////////////////////////
    ///FOR ANIMATION
    //////////////////////////////////
    ///store a referance to the animator componant
    private Animator animator; 


    ///Awake is called when script is being loaded
    private void Awake()
    {
        ///set time scale to 1 so that game will restart on load after game over
        Time.timeScale = 1.0f; 

        ///get the chacter controller from the object in the scene
        controller = gameObject.GetComponent<CharacterController>();
        ///get player Input controller
        _playerInput = new PlayerInput();

        ///get the animator comonant
        animator = GetComponent<Animator>(); 
        
    }

    ///Called when the GameObject becomes enabled
    private void OnEnable()
    {
        ///enable player input
        _playerInput.Enable();
    }

    ///Called when the GameObject becomes disabled
    private void OnDisable()
    {
        ///disable player input
        _playerInput.Disable(); 
    }


    ///// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //private void OnMove()
    //{
    //
    //}

    /// Update is called once per frame
    void Update()
    {
        /////////////////////////////////////////
        ////Player Movement
        ////////////////////////////////////////
        ///Get the player move input from the _playerInput componant
        Vector2 moveInput = _playerInput.Land.Move.ReadValue<Vector2>();

        ///turn the Vector2 input into a Vector3 move direction
        move_direction = new Vector3(moveInput.x, 0, moveInput.y);
        ///move the character controller based on the player move direction, speed and delta time
        controller.Move((move_direction * speed * Time.deltaTime) * Time.deltaTime) ;

        //camera_actor.offset = fire_direction; //set the camera offset to the fire direction

        ///set camera offset to move direction
        camera_actor.offset = move_direction * speed;





        //////////////////////////////////////////////
        ////Dodge
        //////////////////////////////////////////////
        ///Check if the Dodge input was pressed this frame
        if (_playerInput.Land.Dodge.WasPressedThisFrame())
        {
            ///trigger the roll animation
            animator.SetTrigger("isRolling"); 
           // Debug.Log("willRoll");
            //Dodge(move_direction);
        }

        ///////////////////////////////////////
        /////Attack
        //////////////////////////////////////
        ///Check if the Ranged Attack input was pressed this frame
        if (_playerInput.Land.RangedAttack.WasPressedThisFrame())
        {
            ///check wich spell is quiped
            switch (spell_type)
            {
                //case spellType.WAPON_HITSCAN:
                //    FireHitscan();
                //    break;

                ///if the fireball is equiped
                case SpellType.SPELL_FIREBALL:
                    ///call the FireBall() function
                    FireBall();
                    break;
                default:
                    break;
            }
        }
        ///Check if the Mele Attack button was pressed this frame
        if (_playerInput.Land.MeleAttack.WasPressedThisFrame())
        {
            ///check if the player has enough stamina
            if (Stamina > StamConsumption)
            {
                ///trigger the slashing animation
                animator.SetTrigger("isSlashing");
                ///reduce the players mana by the mana consumtion
                Stamina -= StamConsumption;
            }
                /////Call the Slash() function
                //Slash();
        }




        //Vector3 fire_direction = GetFireDirection(); //get the direction we want to fire in
        ///check that move direction is not 0
        if (move_direction != new Vector3(0, 0, 0))
        {
            ///get the direction we want to fire in (in this case same direction as travel)
            Vector3 fire_direction = move_direction;
            ///rotate the player to face that direction
            transform.forward = fire_direction; 
            /// Set the IsWalking bool to true to start the walkign animation
            animator.SetBool("isWalking", true);
        }
        ///if no movement currently
        else
        {
            ///set the IsWalking bool to false 
            animator.SetBool("isWalking", false);
        }






        //////////////////////////////////////////////////
        ///REGEN MANA AND STAMINA
        //////////////////////////////////////////////////
        ///check if mana is less then 100%
        if (Mana < 100) 
        {
            ///add the mana regen multiplied by delta time to mana
            Mana += ManaRegenRate * Time.deltaTime;
        }
        ///check if stamina is less then 100%
        if (Stamina < 100) 
        {
            ///add the stamina regen rate multiplied by delta time to stamina
            Stamina += StamRegenRate * Time.deltaTime;
        }



        ///////////////////////////////////////////////////////
        ///////MENU
        ////////////////////////////////////////////////////////
        ///if the menu button gets pressed this frame
        if (_playerInput.Land.Menu.WasPressedThisFrame()) 
        {
            ///toggle the menue
            menu.SetActive(!menu.activeSelf); 
        }
    }


   

    /////function used to get fire direction
    //Vector3 GetFireDirection()
    //{
    //    //Vector3 mouse_pos = Input.mousePosition; //get the mouse position
    //    Vector3 mouse_pos = Mouse.current.position.ReadValue(); //get the mouse position

    //    Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_pos); //use the current camera to cnver mouse position to a ray

    //    Plane player_plane = new Plane(Vector3.up, transform.position); //create a plane that faces the same position as the player

    //    float ray_distance = 0;
    //    player_plane.Raycast(mouse_ray, out ray_distance); //claculate the distance along the ray that the intersect occurs

    //    Vector3 cast_point = mouse_ray.GetPoint(ray_distance); //use the ray distance to calculate the point of collision

    //    Vector3 to_cast_point = cast_point - transform.position;
    //    to_cast_point.Normalize(); //get the vector in the direction to the point

    //    return to_cast_point;
    //}


    ///cast FireBall
    void FireBall()
    {
        ///trigger the casting animation
        animator.SetTrigger("isCasting"); 

    } 

    ///Slash attack
    public void Slash()
    {
        Debug.Log("slash");
            //Vector3 fire_direction = GetFireDirection(); //determin fire direction
            //Vector3 spawnLocation = this.transform.position + fire_direction; //get the spawn location
            ///get the spawn location to be infront of the player
            Vector3 spawnLocation = spawnPoint.transform.position + this.transform.forward;

            ///spawn the projectile
            GameObject s = Instantiate(slash, spawnLocation, Quaternion.LookRotation(this.transform.forward)) as GameObject;
            
        
    }

    ///Dodge roll
    /// 
    /// Dodge() is called by the animation events at the point the feet leave the ground
    public void Dodge()
    {
        //Debug.Log("isRolling");
        ///Check stamina is grater then stamina consumption
        if (Stamina > StamConsumption)
        {
            ///reduce stamina consumption
            Stamina -= StamConsumption;
            ///move the player controler by the move direction multiplied by speed, delta tiem and dodgeSpeed
            controller.Move(move_direction * speed * Time.deltaTime * dodgeSpeed);
        }
        //animator.SetBool("isRolling", false);
    }


    //void FireHitscan()
    //{
    //    ////////////////////////////////////////////
    //    ///Destroying an enemy
    //    /////////////////////////////////////////////

    //       Vector3 fire_direction = GetFireDirection();
    //        Ray fire_ray = new Ray(transform.position, fire_direction); //cast a ray along that direction

    //        RaycastHit info;
    //        if (Physics.Raycast(fire_ray, out info)) //if the ray hits a target
    //        {
    //            if (info.collider.tag == "Enemy") //check that the ray hits an enemy
    //                Destroy(info.collider.gameObject); //destroy the target
    //        }

    //}

    ///gets called by the enemyActor script when the player is damaged
    public void onPlayerDamaged() 
    {
        //Debug.Log("player Damaged");
        ///check if the player should die
        if (Health <= 0) 
        {
            ///pause the game
            Time.timeScale = 0;
            ///disable the player input
            _playerInput.Disable();
            ///bring up the menu
            menu.SetActive(true); 
        }
    }

    ///Cast is called by the animation controler
    public void Cast()
    {
        ///check if the player has enough mana
        if (Mana > ManaConsumption) 
        {
            ///reduce the players mana by the mana consumtion
            Mana -= ManaConsumption;
            //Vector3 fire_direction = GetFireDirection(); //determin fire direction
            //Vector3 spawnLocation = this.transform.position + fire_direction; //get the spawn location
            ///get the spawn location to be infront of the player
            Vector3 spawnLocation = spawnPoint.transform.position + this.transform.forward; 

            ///Instantiate a projectile at the specified location
            GameObject p = Instantiate(projectile, spawnLocation, Quaternion.LookRotation(this.transform.forward)) as GameObject;
        }
    }   
}
