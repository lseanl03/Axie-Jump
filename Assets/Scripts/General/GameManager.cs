using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private TrunkManager trunkManager;

    #region Get Set

    public TrunkManager TrunkManager
    {
        get { return trunkManager; }
    }

    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }
    #endregion
}
