using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))] //Solo se activa cuando funciona FieldOfView
public class FieldOfViewEditor : Editor //Editor para mostrar Gizmos en Unity
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target; //Pillamos el destino
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);
        //Mostrar un arco de 360 de color blanco con posicion en el enemigo y de radio el radio de visión del enemigo

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees) //Devuelve la dirección del vector para construir el campo de visión
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
