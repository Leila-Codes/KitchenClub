using UnityEngine;
using UnityEngine.UI;

public class Popup
{

    private Transform _parent;
    private Sprite _icon;
    private Type _type;
    private GameObject _ref;

    public Popup(Transform parent, Sprite icon, Type type, GameObject prefab)
    {
        _parent = parent;
        _icon = icon;
        _type = type;
    }

    public enum Type
    {
        Onion,
        Cut,
        Stir,
        Salt,
        Pepper,
        Flame
    }

    private Image _sprite;

    private void Awake()
    {
        _sprite = transform.Find("Content").GetComponent<Image>();
    }

    public void SetIcon(Sprite icon)
    {
        _sprite.sprite = icon;
    }
}
