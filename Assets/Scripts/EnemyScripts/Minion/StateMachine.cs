using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    BaseState currentState;//Estado actual

    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {
            currentState.Enter();

        }
    }

   
    void Update()//Llamar a la logica del estado
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }

    void LateUpdate()//LLamar a las fisicas del estado
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public virtual void ChangeState(BaseState newState)//Para cambiar entre estados
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();

    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='white'><size=50>{content}</size></color>");
    }
}
