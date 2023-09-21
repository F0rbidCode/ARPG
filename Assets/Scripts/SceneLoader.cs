using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// Loads the main game scene
/// 
/// Used to load the main game scene from the main menu or the game menu
public class SceneLoader : MonoBehaviour
{
    /////stores a refeance to the scene that will be loaded
    //public Object sceenToLoad; 

    /// Loads the main game scene
    /// 
    /// Used to load the main game scene from the main menu or the game menu
    public void LoadScene()
    {
        ///call UnityEngine.SceneManagement::LoadScene() to load the main game Scene
        SceneManager.LoadScene("LevelOne");
    }
}
