using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationFocus : MonoBehaviour
{
    public GameLoop gameLoop;
    //public MainMenu mainMenu;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (gameLoop.isPaused)
            {
                gameLoop.ActivateMouse();
            }
        }
    }
}
