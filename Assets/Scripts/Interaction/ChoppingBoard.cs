namespace Interaction
{

    public class ChoppingBoard : Interactable
    {
        private void Start()
        {
            InteractStart += OnInteractStarted;
        }

        private void OnInteractStarted()
        {
            // Does nothing - YET! :) 
        }
    }
}