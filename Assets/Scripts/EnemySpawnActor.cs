using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Instantiates EnemtActor prefabs into scene
/// 
/// At set intervals instantitas an EnemyActor prefab into the scene within a set readious of the PlayerActor
public class EnemySpawnActor : MonoBehaviour
{
    ///holds the prefeab for the enemy we want to spwan
    public GameObject enemy_prefab;
    ///seconds between spawns
    public float spawn_time;
    ///distance from the player to spawn
    public float spawn_radius;

    ///referance to the player
    private PlayerActor player;
    ///the timer that counts down to controle spawning
    private float spawn_timer; 

    /// Start is called before the first frame update
    void Start()
    {
        ///set the spawn timer to equal the desired spawn time
        spawn_timer = spawn_time;
        ///set the referance to the player to be the player object in scene
        player = GameObject.FindObjectOfType<PlayerActor>();
        
    }

    /// Update is called once per frame
    void Update()
    {
        ///count down the timer each frame
        spawn_timer -= Time.deltaTime;

        ///when the spawn tiemr reaches 0
        if (spawn_timer < 0) 
        {
            ///reset the spawn timer
            spawn_timer = spawn_time;

            ///Pick a random angle in radians to set a spawn point
            float spawn_angle = Random.Range(0, 2 * Mathf.PI);
            ///calculate the direction to the spawn poitn as a vector
            Vector3 spawn_direction = new Vector3(Mathf.Sin(spawn_angle), 0, Mathf.Cos(spawn_angle));
            ///Multiply the spawn_direction by the spawn_radius
            spawn_direction *= spawn_radius;

            ///set the spawn point
            Vector3 spawn_point = player.transform.position + spawn_direction;
            ///spawn the enemy at the location
            Instantiate(enemy_prefab, spawn_point, Quaternion.identity); 
        }       

    }
}
