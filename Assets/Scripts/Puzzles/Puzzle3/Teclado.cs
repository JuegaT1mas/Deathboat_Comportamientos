using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Teclado : MonoBehaviour
{
    GameObject puzzle3;
    public TextMeshProUGUI textoPantalla;//Referencia al TextMesh de la pantalla
    public TextMeshProUGUI textoNota;//Referencia al TextMesh de la nota

  
    // Start is called before the first frame update
    void Start()
    {
        puzzle3 = GameObject.Find("Puzzle3");
        GenerarContraseña();
    }

    public void TeclearNum(string num)
    {
        if (textoPantalla.text.Length >= 4)//Comprobar que no escriba mas de 4 caracteres
        {
            return;
        }
        textoPantalla.text += num;//Concatenamos el numero a la cadena 
    }

    public void BorrarPantalla()
    {
        textoPantalla.text = "";//se asigna una cadena vacía
    }

    public void GenerarContraseña()
    {
        for (int i = 0; i < 4; i++)
        {
            int random = Random.Range(0, 9);
            textoNota.text += random;
        }
    }

    public void CheckContraseña()
    {
        if (textoPantalla.text.Equals(textoNota.text))
        {

            textoPantalla.color = Color.green;
            textoPantalla.text = "Correcto";
            puzzle3.GetComponent<Puzzle3>().PuzzleAcabado();
            Destroy(gameObject,1.0f);
        }
        else
        {
          
            textoPantalla.text = "Error";
            Invoke("BorrarPantalla", 1.0f);
        }
    }
}
