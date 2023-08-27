using UnityEngine;

namespace UI
{
    public class AttachTo : MonoBehaviour
    {
        public Transform parent;
        public Vector2 screenOffset = new(0, 100f);

        private RectTransform _content;
        private Vector3 _initScale;

        // Start is called before the first frame update
        void Start()
        {
            _content = GetComponent<RectTransform>();
            _initScale = _content.localScale;
        }

        // Update is called once per frame
        void Update()
        {
            if (parent != null)
            {
                Vector3 target = parent.transform.position;
                Vector3 screenPos = Camera.main.WorldToScreenPoint(target);

                screenPos.x += screenOffset.x;
                screenPos.y += screenOffset.y;
                screenPos.z = 0;

                if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
                {
                    // Don't render.
                    Hide();
                    return;
                }

                Show();

                _content.position = screenPos;
            }
        }

        private void Hide()
        {
            _content.localScale = Vector3.zero;
        }

        private void Show()
        {
            _content.localScale = _initScale;
        }
    }
}