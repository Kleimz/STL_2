using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public Interactable currentInteractable;
    public GameObject dialogueCanvas;
    
    // Start is called before the first frame update
    public void ActivateInteraction(InputAction.CallbackContext context)
    {
        if(currentInteractable != null && context.performed)
        {
            currentInteractable.Interact(this);
        }
    }
}
