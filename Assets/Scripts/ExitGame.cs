using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Quit the game
/// 
/// Called by a UI button to quit the game
public class ExitGame : MonoBehaviour
{
    /// Quit the game
    /// 
    /// Called by a UI button to quit the game
    public void QuitGame()
    {
        ///calls Application.Quit() fron the unity engine
        Application.Quit();
    }
}
