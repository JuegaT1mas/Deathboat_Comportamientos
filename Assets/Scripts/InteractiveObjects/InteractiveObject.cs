using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject puzzle1;
    public GameObject[] listaCamaras;

   public void ActivarObjeto()
    {
        puzzle1.GetComponent<Puzzle>().IniciarPuzzle();
       
        listaCamaras[0].gameObject.SetActive(false);
        listaCamaras[1].gameObject.SetActive(true);
        
    }
}
