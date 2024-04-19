using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] Dialogue thisDialogue;

    [SerializeField] LayerMask targetLayer;
    [SerializeField] float interactionRange = 10;
    void Update()
    {

        Collider[] hitColliders  =  Physics.OverlapSphere(transform.position, interactionRange, targetLayer);
        foreach (Collider collider in hitColliders)
        {
            SetInteractable(collider.GetComponent<InteractionManager>());
        }
    }

    public override void Interact(InteractionManager i)
    {
        print("Starting Dialogue");
        thisDialogue.SetInteractable(i);
        thisDialogue.StartDialogue();
        this.enabled = false;
    }

    public override void SetInteractable(InteractionManager interactionManager)
    {
        interactionManager.currentInteractable = this;
    }

    public override void SetUnInteractable(InteractionManager interactionManager)
    {
        interactionManager = null;
    }

}
