using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Popup : MonoBehaviour
    {

        [HideInInspector] public Sprite icon;
        public Image iconOut;

        private void Start()
        {
            iconOut.sprite = icon;
        }
    }
}
