using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }//Crea Nodo vac�o
        public Sequence(List<Node> children) : base(children) { }//Crea Nodo con la lista de hijos

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false; //Si hay alg�n hijo que est� ejecut�ndose

            foreach (Node node in children)//Por cada jiho
            {
                switch (node.Evaluate())//Lo evaluamos
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state; //Si falla sale
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS; //si un hijo est� ejecut�ndose se marca como running
            return state;
        }

    }
}