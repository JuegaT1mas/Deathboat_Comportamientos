using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePadre : MonoBehaviour
{
 

    // Start is called before the first frame update
    public bool resuelto = false;
    public GameLoop gameLoop; //Referencia al ejemplo
    public GameObject prefabFinPuzzle;
    public bool hasBeenCreated = false; //Indica si el puzzle ha sido creado ya
    //public GameLoop gameLoop;
    public virtual void IniciarPuzzle()
    {

    }

    public virtual void Completed()
    {
        gameLoop.PuzzleCompleted();
    }
    
    public virtual void ShowInstructions()
    {
        gameObject.SetActive(false);
        GameObject prefabFin = Instantiate(prefabFinPuzzle,new Vector3(0, 0, 0), Quaternion.identity);
        prefabFin.transform.parent = gameObject.transform;

    }
}
