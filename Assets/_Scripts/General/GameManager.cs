using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int points;
    [SerializeField] private int primogems;
    [SerializeField] private float playTime = GameConfig.initialPlayTime;
    [SerializeField] private bool gameGameOver = false;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private PlayerController player;

    #region Get Set

    public int Points
    {
        get { return points; }
    }
    public int Primogems
    {
        get { return primogems; }
    }

    public PlayerController Player
    {
        get { return player; }
    }

    public float PlayTime
    {
        get { return playTime; }
        set { playTime = value; } 
    }

    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }
    #endregion
    private void Update()
    {
        ProcessPlayTime();
    }
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
                playTime += 1;
                break;
            case Rate.Rare:
                points += GameConfig.rareItemPoint;
                playTime += 2;
                break;
            case Rate.Special:
                primogems += GameConfig.specialItemPoint;
                break;
        }
    }

    private void ProcessPlayTime()
    {
        if (!gameGameOver)
        {
            playTime -= Time.deltaTime;
            if(playTime <= 0)
            {
                playTime = 0;
                gameGameOver = true;
            }
            UIManager.Instance.GameCanvas.SetPlayTime(playTime);
        }
    }
}
