using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Object sceenToLoad; //stores a refeance to the scene that will be loaded
    
    public void LoadScene()
    {
        SceneManager.LoadScene(sceenToLoad.name);
    }
}
