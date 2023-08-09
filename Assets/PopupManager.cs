using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject popupPrefab;
    
    [Header("Anchor Points around the world")]
    public GameObject fridgeFreezer;
    public GameObject hobCounter;
    public GameObject sinkCounter;
    public GameObject ovenCounter;
    public GameObject choppingCounter;

    [Header("Icon Sprites")] 
    public Sprite onionSprite;
    public Sprite cutSprite;
    public Sprite stirSprite;
    public Sprite saltSprite;
    public Sprite pepperSprite;
    public Sprite flameSprite;
    
    private readonly Dictionary<Popup.Type, GameObject> _dictPopups = new ();

    // private readonly Dictionary<GUID, GameObject> _dictPopups = new();

    public void ShowPopup(Popup.Type type)
    {
        if (_dictPopups.ContainsKey(type)) return;
        
        GameObject target;
        Sprite icon;
        
        switch (type)
        {
            case Popup.Type.Onion:
                target = fridgeFreezer;
                icon = onionSprite;
                break;
            case Popup.Type.Pepper:
                target = choppingCounter;
                icon = pepperSprite;
                break;
            case Popup.Type.Cut:
                target = choppingCounter;
                icon = cutSprite;
                break;
            case Popup.Type.Flame:
                target = hobCounter;
                icon = flameSprite;
                break;
            default:
                throw new NotImplementedException();
        }

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(target.transform.position);
        Instantiate(popupPrefab);
        GameObject newPopup = Instantiate(popupPrefab, transform);
        newPopup.transform.position = screenPoint;
        newPopup.GetComponent<Popup>().SetIcon(icon);
        
        _dictPopups.Add(type, newPopup);
    }

    public void ClearPopup(GUID popupID)
    {
        if (!_dictPopups.ContainsKey(popupID)) return;
        
        DestroyImmediate(_dictPopups[popupID]);
        _dictPopups.Remove(popupID);
    }

    private float _timePassed = 0f;

    private void FixedUpdate()
    {
        _timePassed += Time.fixedDeltaTime;
        if (_timePassed >= 5f)
        {
            _timePassed = 0;
            ShowPopup(Popup.Type.Onion);
        }

        foreach (var popup in _dictPopups)
        {
            popup.Value.transform
        }
    }
}
