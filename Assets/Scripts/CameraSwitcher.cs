using System;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera source;
    public Camera target;

    // private AudioListener _sourceListener;
    // private AudioListener _targetListener;

    private void Start()
    {
        // _sourceListener = source.transform.GetComponent<AudioListener>();
        // _targetListener = target.transform.GetComponent<AudioListener>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Disable the source (current)
            source.enabled = false;
            // _sourceListener.enabled = false;
            source.tag = "Untagged";
            
            // Enable the target (next)
            target.enabled = true;
            // _targetListener.enabled = true;
            target.tag = "MainCamera";
        }
    }
}
