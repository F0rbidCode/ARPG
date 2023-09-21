using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// controls the Porjectile actors
/// 
/// Controls the behaviour of the projectile game objects such as fireball and slash
public class ProjectileActor : MonoBehaviour
{
    ///sets the speed the projectile will travel at
    public float speed;
    ///sets the direction of travel
    public Vector3 direction;
    ///sets the desired life time of the projectile
    public float Lifetime; 

    /// Start is called before the first frame update
    void Start()
    {
        ///set the direction vector to the transforms forward vector
        direction = transform.forward;
    }

    /// Update is called once per frame
    void Update()
    {
        ///move the projectile allong its path at its set speed
        transform.position += direction * speed * Time.deltaTime;

        ///reduce the lifetime by the elapesed time
        Lifetime -= Time.deltaTime;
        ///when the lifetime reaches 0
        if (Lifetime < 0 ) 
        {
            ///destroy the projectile
            Destroy(gameObject); 
        }
    }

   
    /// to be called when projectile collides with an enemy
    /// 
    /// Gets called when a collision is dectected, checks if the collided object is an enemy then destroys that object
    /// <param name="Collision hit"></param>
    private void OnCollisionEnter(Collision hit)
    {
        ///check if the collision was with an enemy
        if (hit.collider.tag == "Enemy") 
        {
            ///destry the collided with object
            Destroy(hit.collider.gameObject); 
            
        }
        ///check if the collided with object was NOT the player
        if (hit.collider.tag != "Player")
        {
            ///Check if the object had a audio component
            if(GetComponent<AudioSource>())
            {
                ///Stop the audio when object is destroid
                GetComponent<AudioSource>().Stop(); 
            }
            ///destroy the projectile
            Destroy(gameObject); 
        }
    }
}
