using UnityEngine;

public class GamePlayUIManager : Singleton<GamePlayUIManager>
{
    private GameCanvas gameCanvas;

    public GameCanvas GameCanvas
    {
        get { return gameCanvas; }
    }

    protected override void Awake()
    {
        base.Awake();
        if (!gameCanvas)
            gameCanvas = GetComponentInChildren<GameCanvas>();
    }
}
