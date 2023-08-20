using UnityEngine;

namespace Dining
{
    public class Table : MonoBehaviour
    {
        private Chair[] _chairs = { };

        // Start is called before the first frame update
        void Start()
        {
            _chairs = GetComponentsInChildren<Chair>();
        }

        public Chair NextAvailableSeat()
        {
            foreach (Chair chair in _chairs)
            {
                if (chair.IsAvailable()) return chair;
            }

            return null;
        }
    }
}

