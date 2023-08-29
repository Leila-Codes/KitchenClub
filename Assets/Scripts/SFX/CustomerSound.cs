using Dining;
using UnityEngine;

namespace SFX
{
    public class CustomerSound : MonoBehaviour
    {
        public Dining.Customer customer;
        public AudioSource audioSource;

        public AudioClip[] positive;
        public AudioClip[] negative;
        public AudioClip veryBad;

        private void Start()
        {
            customer.MoodChanged += OnMoodChange;
        }

        private void PlaySound(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        private void PlaySoundFrom(AudioClip[] clips)
        {
            AudioClip selected = clips[Random.Range(0, clips.Length)]; 
            PlaySound(selected);
        }

        private void OnMoodChange(Dining.Customer.Mood newMood)
        {

            if (newMood == Dining.Customer.Mood.Happy)
            {
                PlaySoundFrom(positive);
            } else if (newMood == Customer.Mood.Annoyed)
            {
                PlaySound(veryBad);
            }
            else
            {
                PlaySoundFrom(negative);
            }
        }
    }
}
