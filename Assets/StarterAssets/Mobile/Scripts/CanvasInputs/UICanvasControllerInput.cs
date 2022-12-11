using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public FirstPersonController firstPersonController; //Referencia para el interact

        public GameObject[] buttons; //Para activar y desactivar el salir y entrar al puzzle

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            starterAssetsInputs.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            starterAssetsInputs.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            starterAssetsInputs.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            starterAssetsInputs.SprintInput(virtualSprintState);
        }

        public void VirtualCrouchInput(bool virtualCrouchState)
        {
            starterAssetsInputs.CrouchInput(virtualCrouchState);
        }

        public void VirtualInteractInput(bool virtualInteractState)
        {
            starterAssetsInputs.InteractInput(virtualInteractState);
            bool activated = firstPersonController.OnInteract();
            if (activated)
            {
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
                buttons[2].SetActive(false);
                buttons[3].SetActive(false);
                buttons[4].SetActive(false);
                buttons[5].SetActive(false);
                buttons[6].SetActive(false);
            }
        }
        public void VirtualExitInput()
        {
            firstPersonController.OnLeavePuzzle(); //llamadas directas a los métodos
            buttons[1].SetActive(false);
            buttons[0].SetActive(true);
            buttons[2].SetActive(true);
            buttons[3].SetActive(true);
            buttons[4].SetActive(true);
            buttons[5].SetActive(true);
            buttons[6].SetActive(true);
        }

        public void VirtualPauseInput()
        {
            firstPersonController.OnPause();
        }
    }

}
