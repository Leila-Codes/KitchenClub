using System.Collections;
using UI;
using UnityEngine;

namespace Cooking
{
    public class CookingTimer : MonoBehaviour
    {
        public float timeToCook;
        public GameObject progressPrefab;
        public GameObject uiContainer;
        private float _elapsed;
        private ProgressIndicator _progressIndicator;

        private void Start()
        {
            GameObject newBar = Instantiate(progressPrefab, uiContainer.transform);
            _progressIndicator = newBar.GetComponent<ProgressIndicator>();

            StartCoroutine(KitchenTimer());
        }

        private IEnumerator KitchenTimer()
        {
            while (_elapsed < timeToCook)
            {
                yield return new WaitForSeconds(1f);
                _elapsed += 1;

                _progressIndicator.value = _elapsed / timeToCook;
            }
        }
    }
}