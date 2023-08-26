using System.Collections.Generic;
using UnityEngine;

namespace Cooking
{
    public class Recipe : MonoBehaviour
    {
        public GameObject popupCanvas;
        public List<CookingStep> steps;
        private CookingStep _currentStep;
        private int _currentStepId;

        private GameObject _stepTooltip;


        private void RenderStep()
        {
            if (_currentStep.target)
            {
                // Create new Tooltip
                _stepTooltip = Instantiate(_currentStep.popup, popupCanvas.transform);
                
                // Attach the popup to the parent/target object of this step.
                UI.AttachTo content = _stepTooltip.GetComponent<UI.AttachTo>();
                content.parent = _currentStep.target.gameObject.transform;
                
                // configure listener for step completion
                if (_currentStep.action == CookingStep.ActionType.Cook && _currentStep.cookingTime > 0f)
                {
                    _currentStep.target.interactionDuration = _currentStep.cookingTime;
                }
                _currentStep.target.InteractComplete += StepComplete;
            }
        }

        void StepComplete()
        {
            Debug.Log("Congratulations! You just completed a step in the Kitchen!");
            Destroy(_stepTooltip);
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
