using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private GameCanvas gameCanvas;

    protected override void Awake()
    {
        base.Awake();
        gameCanvas = GetComponentInChildren<GameCanvas>();
    }
}
