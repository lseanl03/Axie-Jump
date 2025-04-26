using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int points;
    [SerializeField] private int primogems;

    //[SerializeField] private bool gameOver = false;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private TrunkManager trunkManager;

    #region Get Set

    public int Points
    {
        get { return points; }
    }
    public int Primogems
    {
        get { return primogems; }
    }
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

    private void OnEnable()
    {
        EventManager.onCollectItem += OnCollectItem;
    }

    private void OnDisable()
    {
        EventManager.onCollectItem -= OnCollectItem;
    }

    private void OnCollectItem(Rate rate)
    {
        switch (rate)
        {
            case Rate.Normal:
                points += GameConfig.normalItemPoint;
                break;
            case Rate.Rare:
                points += GameConfig.rareItemPoint;
                break;
            case Rate.Special:
                primogems += GameConfig.specialItemPoint;
                break;
        }
    }

}
