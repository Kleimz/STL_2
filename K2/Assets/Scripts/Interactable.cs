using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact(InteractionManager interactionManager);

    public abstract void SetInteractable(InteractionManager interactionManager);

    public abstract void SetUnInteractable(InteractionManager interactionManager);
}
