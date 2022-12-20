using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShadows : MonoBehaviour
{

    public GameObject[] luces; //lista de las luces

    public float distanciaApagarLuces;

    void Update()
    {
        for (int i = 0; i < luces.Length; i++)
        {
            if (Vector3.Distance(luces[i].transform.position,transform.position) < distanciaApagarLuces)
            {
                luces[i].gameObject.SetActive(false);
            }
            else
            {
                luces[i].gameObject.SetActive(true);
            }
        }
    }
}
