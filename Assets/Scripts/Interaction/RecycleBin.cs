namespace Interaction
{
    public class RecycleBin : Interactable
    {
        public PlayerCharacter player;
    
        // Start is called before the first frame update
        void Start()
        {
            InteractComplete += Discard;
        }

        void Discard(Interactable interactable)
        {
            if (player is { carrying: not null })
            {
                player.carrying = null;
            }
        }
    }
}
