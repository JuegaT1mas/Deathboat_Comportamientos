using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public FieldOfView fov; //El script del cono de visión

    public float chaseSpeed; //La velocidad a la que te sigue
    public float minimumDistance; //La distancia mínima a la que queremos que este el monstruo
    //public float rotateSpeed;

    private Vector3 lastPlayerPosition; //La última posición del jugador

    private void Update()
    {
        if (fov.canSeePlayer) //Si se puede ver al jugador
        {
            lastPlayerPosition = fov.playerRef.transform.position; //Actualizamos la posición  
            LookAt();
            if (Vector3.Distance(transform.position, lastPlayerPosition) > minimumDistance) //Comprobamos que no se acerque demasiado
            {
                //Moverse a la última posición actualizada.
                transform.position = Vector3.MoveTowards(transform.position, lastPlayerPosition, chaseSpeed * Time.deltaTime);
            }
        }
    }

    private void LookAt()//Función para mirar hacia el jugador
    {
        Vector3 direction = fov.targetDirection; 
        direction.y = 0;  //Para poder mirar al jugador sin que el monstruo se incline bloqueamos la y de la rotación.
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}