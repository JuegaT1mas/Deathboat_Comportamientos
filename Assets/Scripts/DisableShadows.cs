using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableShadows : MonoBehaviour
{

    public GameObject[] luces; //lista de las luces

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < luces.Length; i++)
        {
             if(Mathf.Abs(luces[i].transform.position.y - transform.position.y) > 3.5)
            {
                luces[i].GetComponentInChildren<Light>().shadows = LightShadows.None;
            }
            else
            {
                luces[i].GetComponentInChildren<Light>().shadows = LightShadows.Hard;
            }
        }
    }
}
