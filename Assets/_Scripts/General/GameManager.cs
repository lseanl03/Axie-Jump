using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool gameGameOver = false;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private int points;
    [SerializeField] private int primogems;
    [SerializeField] private float playTime = GameConfig.initialPlayTime;
    [SerializeField] private int normalItemPoint = GameConfig.normalItemPoint;
    [SerializeField] private int rareItemPoint = GameConfig.rareItemPoint;
    [SerializeField] private int primogemPoint = GameConfig.primogemPoint;
    [SerializeField] private PlayerController player;

    private Enemy currentRequestEnemy;
    private Request currentRequest;
    private RequestData requestData;

    protected override void Awake()
    {
        base.Awake();

        if(!requestData)
        requestData = Resources.Load<RequestData>("SOData/RequestData");
    }

    #region Get Set
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    public int Primogems
    {
        get { return primogems; }
        set { primogems = value; }
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
    public int NormalItemPoint
    {
        get { return normalItemPoint; }
        set { normalItemPoint = value; }
    }
    public int RareItemPoint
    {
        get { return rareItemPoint; }
        set { rareItemPoint = value; }
    }
    public int PrimogemPoint
    {
        get { return primogemPoint; }
        set { primogemPoint = value; }
    }
    public bool GameGameOver
    {
        get { return gameGameOver; }
        set { gameGameOver = value; }
    }
    public RequestData RequestData
    {
        get { return requestData; }
        set { requestData = value; }
    }
    public Request CurrentRequest
    {
        get { return currentRequest; }
        set { currentRequest = value; }
    }
    public Enemy CurrentRequestEnemy
    {
        get { return currentRequestEnemy; }
        set { currentRequestEnemy = value; }
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

    private void OnCollectItem(Item item)
    {
        switch (item.Rate)
        {
            case Rate.Normal:
                UpdatePoint(normalItemPoint);
                playTime += GameConfig.normalItemTime;
                OnGetPoint(item.transform, normalItemPoint);
                break;
            case Rate.Rare:
                OnGetPoint(item.transform, rareItemPoint);
                playTime += GameConfig.rareItemTime;
                OnGetPoint(item.transform, rareItemPoint);
                break;
            case Rate.Special:
                primogems += primogemPoint;
                OnGetPrimogem(item.transform, primogemPoint);
                break;
        }
    }

    public void UpdatePoint(int newPoint)
    {
        points += newPoint;
        if(points < 0)
        {
            points = 0;
        }
    }

    public void UpdatePrimogem(int newPoint)
    {
        primogems += newPoint;
        if (primogems < 0)
        {
            primogems = 0;
        }
    }

    private void ProcessPlayTime()
    {
        if (!gameGameOver || gameStarted)
        {
            playTime -= Time.deltaTime;
            if(playTime <= 0)
            {
                playTime = 0;
                OnGameOver();
            }
            GamePlayUIManager.Instance.GameCanvas.SetPlayTime(playTime);
        }
    }

    public void OnGameOver()
    {
        gameGameOver = true;
        player.CanDie = true;
        player.Die();
    }

    public void OnGetPoint(Transform transform, int point)
    {
        GamePlayUIManager.Instance.GameCanvas.SetPointText(points);
        var prefab = PoolManager.Instance.GetObj(
            PoolType.GetPointPopup, transform.position, null);
        var popup = prefab.GetComponent<GetPointPopup>();
        if (popup != null)
        {
            popup.SetText(point);
        }
    }

    public void OnGetPrimogem(Transform transform, int primogem)
    {
        GamePlayUIManager.Instance.GameCanvas.SetPrimogemText(primogems);
        var prefab = PoolManager.Instance.GetObj(
            PoolType.GetPrimogemPopup, transform.position, null);
        var popup = prefab.GetComponent<GetPrimogemPopup>();
        if (popup != null)
        {
            popup.SetText(primogem);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public bool AgreeRequet()
    {
        var state = false;
        if (currentRequest != null)
        {
            state = requestData.AgreeRequestWithType(
                currentRequest, currentRequestEnemy.transform);
            if (state)
            {
                currentRequestEnemy.StartAction();
                currentRequest = null;
                currentRequestEnemy = null;
            }
        }
        return state;
    }

    public void RejectRequest()
    {
        currentRequest = null;
        currentRequestEnemy.EndAction();
    }
}
