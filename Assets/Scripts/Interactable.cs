using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionDuration = 1f;

    public abstract void Interact();

    public abstract void Cancel();
}
