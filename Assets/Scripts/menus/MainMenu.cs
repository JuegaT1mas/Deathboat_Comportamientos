using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource click;
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

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }

    public void ClickSound()
    {
        click.Play();
    }
}
