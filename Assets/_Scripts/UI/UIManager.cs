using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private UICanvas uICanvas;

    public UICanvas UICanvas
    {
        get { return uICanvas; }
    }

    protected override void Awake()
    {
        base.Awake();
        if (!uICanvas)
            uICanvas = GetComponentInChildren<UICanvas>();
    }
}
