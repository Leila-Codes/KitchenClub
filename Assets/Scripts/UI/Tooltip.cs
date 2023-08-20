using System;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Transform parent;
    public Vector2 tooltipOffset = new(0, 100f);

    private RectTransform _tooltip;
    private readonly CanvasRenderer[] _renderers = new CanvasRenderer[2];
    private Camera _mainCamera;

    public enum Icon
    {
        Onion,
        Knife,
        Spoon,
        Salt,
        Pepper,
        Flame
    }

    [Header("Sprite References")] public Sprite onionIcon;
    public Sprite knifeIcon;
    public Sprite spoonIcon;
    public Sprite saltIcon;
    public Sprite pepperIcon;
    public Sprite flameIcon;

    private void Awake()
    {
        _tooltip = GetComponent<RectTransform>();

        Transform rendererChild = transform.GetChild(0);
        _renderers[0] = rendererChild.GetComponent<CanvasRenderer>();
        _renderers[1] = rendererChild.GetChild(0).GetComponent<CanvasRenderer>();
        _mainCamera = Camera.main;
        
        Hide();
    }

    private void Hide()
    {
        foreach (CanvasRenderer canvasRenderer in _renderers)
        {
            canvasRenderer.SetAlpha(0f);
            canvasRenderer.cull = true;
        }
    }

    private void Show()
    {
        foreach (CanvasRenderer canvasRenderer in _renderers)
        {
            canvasRenderer.SetAlpha(1f);
            canvasRenderer.cull = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!parent || !_tooltip) return;

        Vector3 target = parent.transform.position;
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(target);
        
        screenPos.x += tooltipOffset.x;
        screenPos.y += tooltipOffset.y;
        
        if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
        {
            // Don't render.
            Hide();
            return;
        }

        Show();
        
        _tooltip.position = screenPos;
    }

    public void SetIcon(Icon icon)
    {
        Image rendererChild = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        switch (icon)
        {
            case Icon.Flame:
                rendererChild.sprite = flameIcon;
                break;
            case Icon.Knife:
                rendererChild.sprite = knifeIcon;
                break;
            case Icon.Onion:
                rendererChild.sprite = onionIcon;
                break;
            case Icon.Pepper:
                rendererChild.sprite = pepperIcon;
                break;
            case Icon.Salt:
                rendererChild.sprite = saltIcon;
                break;
            case Icon.Spoon:
                rendererChild.sprite = spoonIcon;
                break;
            default:
                Debug.LogError("Unrecognised icon type" + icon);
                break;
        }
    }
}