using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selected : MonoBehaviour
{
    LayerMask mask;//la layer en la que van a estar todos los objetos interactivos
    public float distancia = 1.5f;//distancia del raycast
    private GameObject _player;
    public bool rayCastActivo = false;//variable usada en el FPC para saber si esta activo a la hora de llamar al OnInteract
    //public GameObject TextDetect;
    GameObject ultimoReconocido = null;//variable para cambiar de color al acercarse
    public GameObject puzzleActual;//recoge el puzle que estamos selecionando

    //Variable para comprobar el raycast
    private bool estaCerca;//variable que comprueba el raycast
    [HideInInspector]
    public RaycastHit hit;
    Color aux;


    //public bool puzzleActivado = false;

    private void Awake()
    {
        _player = GameObject.Find("PlayerCapsule");
    }
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        //TextDetect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Raycast(origen,dirección,out hit, distancia, mascara)
        estaCerca = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask);
        if (estaCerca)
        {
            puzzleActual = hit.collider.gameObject;//almacena el gameobject que al interactuar con el sale el puzzle
            rayCastActivo = true;
            Deselect();
            SelectedObject(hit.transform);
        }
        else
        {
            rayCastActivo = false;
            Deselect();
        }

    }

 

    void SelectedObject(Transform transform)//Metodo para cambiar el color del gameobject cuando detecte un raycast
    {
        aux = transform.GetComponent<MeshRenderer>().material.color;
        transform.GetComponent<MeshRenderer>().material.color = Color.white;//cuando el rayo impacte con el objeto se cambiará el color del objeto
        ultimoReconocido = transform.gameObject;

    }

    public void CrearPuzzle(RaycastHit hit)//metodo llamado desde el FPC en el OnInteract
    {
        hit.collider.transform.GetComponent<InteractiveObject>().Decide();
        Deselect();
    }
    void Deselect()//metodo para volver a cambiar el color del gameobject
    {
        if (ultimoReconocido)
        {
            ultimoReconocido.GetComponent<Renderer>().material.color = aux;
            ultimoReconocido = null;
        }
    }

    void OnGUI()
    {

        //if (ultimoReconocido != null)
        //{
        //    TextDetect.SetActive(true);
        //}
        //else
        //{
        //    TextDetect.SetActive(false);
        //}
    }
}
