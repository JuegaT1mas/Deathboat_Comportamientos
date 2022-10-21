using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(int scene) //Carga la escena indicada por el int (representa el orden de la escena en BuildSettings)
    {
        SceneManager.LoadScene(scene);
    }
    public void QuitGame() //Cierra el juego
    {
        Application.Quit(); 
    }

    public void OpenURL(string url) //Abre una url en el navegador
    {
        Application.OpenURL(url);
    }
}
