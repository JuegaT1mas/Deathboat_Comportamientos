using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 1.5f;
    private GameObject _player;
    public bool rayCastActivo = false;
    public GameObject TextDetect;
    GameObject ultimoReconocido = null;
    public GameObject puzzleActual;

    //Variable para comprobar el raycast
    private bool estaCerca;
    [HideInInspector]
    public RaycastHit hit;
 

    public bool puzzleActivado = false;

    private void Awake()
    {
        _player = GameObject.Find("PlayerCapsule");
    }
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");
        puzzleActual = GameObject.Find("Puzzle1");
        //TextDetect.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


        //Raycast(origen,dirección,out hit, distancia, máscara)

        estaCerca = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask);
        if (estaCerca)
        {
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

 

    void SelectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.green;//cuando el rayo impacte con el objeto se cambiará el color del objeto
        ultimoReconocido = transform.gameObject;

    }

    public void CrearPuzzle(RaycastHit hit)
    {
        hit.collider.transform.GetComponent<InteractiveObject>().ActivarObjeto();
        Deselect();
    }
    void Deselect()
    {
        if (ultimoReconocido)
        {
            ultimoReconocido.GetComponent<Renderer>().material.color = Color.white;
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
