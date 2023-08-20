using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    public class Recipe : MonoBehaviour
    {
        public GameObject tooltipPrefab;
        public GameObject canvas;
        public List<CookingStep> steps;
        private CookingStep _currentStep;
        private int _currentStepId;

        private GameObject stepTooltip;


        private void RenderStep()
        {
            if (_currentStep.target)
            {
                // Create new Tooltip
                stepTooltip = Instantiate(tooltipPrefab, canvas.transform);
                Tooltip content = stepTooltip.GetComponent<Tooltip>();
                content.parent = _currentStep.target.gameObject.transform;

                switch (_currentStep.action)
                {
                    case CookingStep.ActionType.Collect:
                        content.SetIcon(Tooltip.Icon.Onion);
                        break;
                    case CookingStep.ActionType.Cook:
                        content.SetIcon(Tooltip.Icon.Flame);
                        break;
                    case CookingStep.ActionType.Cut:
                        content.SetIcon(Tooltip.Icon.Knife);
                        break;
                    case CookingStep.ActionType.Stir:
                        content.SetIcon(Tooltip.Icon.Spoon);
                        break;
                }
                
                // configure listener for step completion
                _currentStep.target.InteractComplete += StepComplete;
            }
        }

        void StepComplete()
        {
            Debug.Log("Congratulations! You just completed a step in the Kitchen!");
            Destroy(stepTooltip);
            _currentStep.target.InteractComplete -= StepComplete;
            
            _currentStep.SetCompleted(true);
            _currentStepId++;

            if (_currentStepId < steps.Count)
            {
                _currentStep = steps[_currentStepId];
                RenderStep();
            }

        }

        private void Start()
        {
            _currentStep = steps[_currentStepId];
            
            if (_currentStep != null)
            {
                RenderStep();
            }
        }
    }
}
