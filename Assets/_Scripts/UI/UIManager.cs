using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private GameCanvas gameCanvas;

    protected override void Awake()
    {
        base.Awake();
        if (!gameCanvas)
            gameCanvas = GetComponentInChildren<GameCanvas>();
    }
}
