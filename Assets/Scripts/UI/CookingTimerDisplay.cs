using Cooking;
using Interaction;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CookingTimerDisplay : MonoBehaviour
    {
        // [Tooltip(("UI Element to update"))]
        public TMP_Text readout;
        // [Tooltip("Cooking Appliance to monitor timing on.")]
        public CookingAppliance target;
    
        // Start is called before the first frame update
        void Start()
        {
            Timer timer = target.Timer();

            timer.TimerStopped += ResetText;
            
            timer.TimerStarted += TimerUpdate;
            timer.TimerTick += TimerUpdate;
            
            ResetText();
        }

        void ResetText()
        {
            readout.text = "?";
            readout.color = Color.white;
        }

        void TimerUpdate(float newTime)
        {
            readout.text = ((int)newTime).ToString();
            if (newTime < 0)
            {
                readout.color = Color.red;
            }
        }
    }
}
