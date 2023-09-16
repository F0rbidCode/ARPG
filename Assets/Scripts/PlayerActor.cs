using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Serialization;

public class PlayerActor : MonoBehaviour
{
    //initialise the player controller variable
    private CharacterController controller;

    public float speed = 5.0f; //set the players movement speed
    public float dodgeSpeed = 10f; //set the speed the player can dodge at

    private PlayerInput _playerInput;
    private float _playersMovementDirection;

    


    /////////////////////////////////////////////////
    ///Player Stats
    /////////////////////////////////////////////////
    [Header ("player stats")]
    public float Health = 100;   
    public float Stamina = 100;   
    public float Mana = 100;

    public int Level = 1;
    public int Exp = 0;

    //public float projectileSpeed;

    
    public enum SpellType //set and switch between the available weapons 
    {
        SPELL_FIREBALL,
    }
    [Header("player Skills")]
    public SpellType spell_type; //used to select weapon type

    public float ManaConsumption = 5;
    public float StamConsumption = 5;

    public float ManaRegenRate = 1;
    public float StamRegenRate = 2;

    [Header("Game Objects")]
    public GameObject projectile; //referance to the projectile prefab
    public GameObject slash; //referance to the slash prefab
    public CameraActor camera_actor; //referance to the game camera
    public GameObject menu; //referance to the menu game object

    private void Awake()
    {
        Time.timeScale = 1.0f; //set time scale to 1 so that game will restart on load after game over

        //get the chacter controller from the object in the scene
        controller = gameObject.GetComponent<CharacterController>();
        //get player Input controller
        _playerInput = new PlayerInput();
        
    }

    private void OnEnable()
    {
        _playerInput.Enable();//enable player input
    }

    private void OnDisable()
    {
        _playerInput.Disable(); //disable player input
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void OnMove()
    //{
    //
    //}

    // Update is called once per frame
    void Update()
    {
        /////////////////////////////////////////
        ////Player Movement
        ////////////////////////////////////////
        Vector2 moveInput = _playerInput.Land.Move.ReadValue<Vector2>();

        Vector3 move_direction = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move_direction * speed * Time.deltaTime);

        //////////////////////////////////////////////
        ////Dodge
        //////////////////////////////////////////////
        if (_playerInput.Land.Dodge.WasPressedThisFrame())
        {
            Dodge(move_direction);
        }

        ///////////////////////////////////////
        /////Attack
        //////////////////////////////////////
        if (_playerInput.Land.RangedAttack.WasPressedThisFrame())
        {
            switch (spell_type)
            {
                //case spellType.WAPON_HITSCAN:
                //    FireHitscan();
                //    break;

                case SpellType.SPELL_FIREBALL:
                    FireBall();
                    break;
                default:
                    break;
            }
        }
        if (_playerInput.Land.MeleAttack.WasPressedThisFrame())
        {
            Slash();
        }




        //Vector3 fire_direction = GetFireDirection(); //get the direction we want to fire in
        //check that move direction is not 0
        if (move_direction != new Vector3(0, 0, 0))
        {
            Vector3 fire_direction = move_direction; //get the direction we want to fire in (in this case same direction as travel)
            transform.forward = fire_direction; //rotate the player to face that direction
        }



        //camera_actor.offset = fire_direction; //set the camera offset to the fire direction

        camera_actor.offset = move_direction * speed;//set camera offset to move direction


        //////////////////////////////////////////////////
        ///REGEN MANA AND STAMINA
        //////////////////////////////////////////////////
        if(Mana < 100) //check if mana is less then 100%
        {
            Mana += ManaRegenRate * Time.deltaTime;
        }
       if (Stamina < 100) //check if stamina is less then 100%
        {
            Stamina += StamRegenRate * Time.deltaTime;
        }
        


        ///////////////////////////////////////////////////////
        ///////MENU
        ////////////////////////////////////////////////////////
        if (_playerInput.Land.Menu.WasPressedThisFrame()) //if the menu button gets pressed this frame
        {
            menu.SetActive(!menu.activeSelf); //toggle the menue
        }
    }


   

    //function used to get fire direction
    Vector3 GetFireDirection()
    {
        //Vector3 mouse_pos = Input.mousePosition; //get the mouse position
        Vector3 mouse_pos = Mouse.current.position.ReadValue(); //get the mouse position

        Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_pos); //use the current camera to cnver mouse position to a ray

        Plane player_plane = new Plane(Vector3.up, transform.position); //create a plane that faces the same position as the player

        float ray_distance = 0;
        player_plane.Raycast(mouse_ray, out ray_distance); //claculate the distance along the ray that the intersect occurs

        Vector3 cast_point = mouse_ray.GetPoint(ray_distance); //use the ray distance to calculate the point of collision

        Vector3 to_cast_point = cast_point - transform.position;
        to_cast_point.Normalize(); //get the vector in the direction to the point

        return to_cast_point;
    }

    void FireBall()
    {
        if (Mana > ManaConsumption) //check if the player has enough mana
        {
            Mana -= ManaConsumption; //reduce the players mana by the mana consumtion
            //Vector3 fire_direction = GetFireDirection(); //determin fire direction
            //Vector3 spawnLocation = this.transform.position + fire_direction; //get the spawn location
            Vector3 spawnLocation = this.transform.position + this.transform.forward; //get the spawn location to be infront of the player


            GameObject p = Instantiate(projectile, spawnLocation, Quaternion.LookRotation(this.transform.forward)) as GameObject;//spawn the projectile
        }

    } 

    void Slash()
    {
        if (Stamina > StamConsumption) //check if the player has enough mana
        {
            Stamina -= StamConsumption; //reduce the players mana by the mana consumtion
            //Vector3 fire_direction = GetFireDirection(); //determin fire direction
            //Vector3 spawnLocation = this.transform.position + fire_direction; //get the spawn location
            Vector3 spawnLocation = this.transform.position + this.transform.forward; //get the spawn location to be infront of the player


            GameObject s = Instantiate(slash, spawnLocation, Quaternion.LookRotation(this.transform.forward)) as GameObject;//spawn the projectile
        }
    }

    void Dodge(Vector3 move_direction)
    {
        if (Stamina > StamConsumption)
        {
            Stamina -= StamConsumption;
            controller.Move(move_direction * speed * Time.deltaTime * dodgeSpeed);
        }
    }


    void FireHitscan()
    {
        ////////////////////////////////////////////
        ///Destroying an enemy
        /////////////////////////////////////////////
       
           Vector3 fire_direction = GetFireDirection();
            Ray fire_ray = new Ray(transform.position, fire_direction); //cast a ray along that direction

            RaycastHit info;
            if (Physics.Raycast(fire_ray, out info)) //if the ray hits a target
            {
                if (info.collider.tag == "Enemy") //check that the ray hits an enemy
                    Destroy(info.collider.gameObject); //destroy the target
            }
        
    }

    public void onPlayerDamaged() //gets called by the enemyactor script when the player is damaged
    {
        Debug.Log("player Damaged");
        if(Health <= 0) //check if the player should die
        {
            Time.timeScale = 0; //pause the game
            _playerInput.Disable(); //disable the player input
            menu.SetActive(true); //bring up the menu
        }
    }
}
