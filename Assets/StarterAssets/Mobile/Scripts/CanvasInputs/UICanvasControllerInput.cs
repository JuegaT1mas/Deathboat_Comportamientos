using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public StarterAssetsInputs starterAssetsInputs;

        public FirstPersonController firstPersonController; //Referencia para el interact

        public GameObject[] buttonsInteractAndExit; //Para activar y desactivar el salir y entrar al puzzle

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
                buttonsInteractAndExit[0].SetActive(false);
                buttonsInteractAndExit[1].SetActive(true);
            }
        }
        public void VirtualExitInput()
        {
            firstPersonController.OnLeavePuzzle(); //llamadas directas a los métodos
            buttonsInteractAndExit[1].SetActive(false);
            buttonsInteractAndExit[0].SetActive(true);

        }

        public void VirtualPauseInput()
        {
            firstPersonController.OnPause();
        }
    }

}
