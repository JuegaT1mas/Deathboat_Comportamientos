using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Teclado : MonoBehaviour
{
    GameObject puzzle3;
    public TextMeshProUGUI textoPantalla;//Referencia al TextMesh de la pantalla
    public TextMeshProUGUI textoNota;//Referencia al TextMesh de la nota

    public string num;
  
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
        textoPantalla.color = Color.white;
    }

    public void GenerarContraseña()
    {
        int numero = Random.Range(1000, 9999);

        num = numero.ToString();

        //que operacion hacer
        int operacion = Random.Range(0, 1);

        //lo que restar/sumar
        int numOperacion = Random.Range(1, 1000);
        

        if(operacion == 0)
        {
            float res = numero + numOperacion;
            textoNota.text = "" + res + " - " + numOperacion;
        }
        else
        {
            float res = numero - numOperacion;
            textoNota.text = "" + res + " + " + numOperacion;
        }
    }
    void DesactivarTodo()
    {
        gameObject.SetActive(false);
    }
    public void CheckContraseña()
    {
        if (textoPantalla.text.Equals(num))
        {

            textoPantalla.color = Color.green;
            textoPantalla.text = "Correct";
            Invoke("DesactivarTodo", 1.5f);
            puzzle3.GetComponent<Puzzle3>().PuzzleAcabado();
           
        }
        else
        {
            textoPantalla.color = Color.red;
            textoPantalla.text = "Error";
            Invoke("BorrarPantalla", 1.0f);
            
        }
    }
}
