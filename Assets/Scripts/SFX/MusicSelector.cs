using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource _audioSource;
    private int _currentIndex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentIndex = Random.Range(0, musicTracks.Length);
    }

    private void Start()
    {
       NextTrack(); 
    }

    private void NextTrack()
    {
        if (++_currentIndex >= musicTracks.Length)
        {
            _currentIndex = 0;
        }

        _audioSource.clip = musicTracks[_currentIndex];
        _audioSource.Play();
    }

    private void Update()
    {
        // It stopped!? Maybe the track has finished.
        if (!_audioSource.isPlaying)
        {
            NextTrack();
        }
    }
}
