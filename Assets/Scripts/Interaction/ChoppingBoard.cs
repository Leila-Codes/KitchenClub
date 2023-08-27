namespace Interaction
{

    public class ChoppingBoard : Interactable
    {
        private void Start()
        {
            InteractStart += OnInteractStarted;
        }

        private void OnInteractStarted(Interactable interactable)
        {
            // Does nothing - YET! :) 
        }
    }
}