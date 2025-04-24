using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private TrunkManager trunkController;

    public TrunkManager TrunkController
    {
        get { return trunkController; }
    }

    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }
}
