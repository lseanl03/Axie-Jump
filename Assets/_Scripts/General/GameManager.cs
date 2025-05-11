using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private bool warning = false;
    [SerializeField] private bool gameGameOver = false;
    [SerializeField] private bool gameStarted = false;
    [SerializeField] private int points;
    [SerializeField] private int primogems;
    [SerializeField] private int highScrore;
    [SerializeField] private int primogemOwn;
    [SerializeField] private float playTime = GameConfig.initialPlayTime;
    [SerializeField] private int normalItemPoint = GameConfig.normalItemPoint;
    [SerializeField] private int rareItemPoint = GameConfig.rareItemPoint;
    [SerializeField] private int primogemPoint = GameConfig.primogemPoint;
    [SerializeField] private PlayerController player;
    [SerializeField] private SceneType sceneType;

    private Enemy currentRequestEnemy;
    private Request currentRequest;
    private RequestData requestData;

    protected override void Awake()
    {
        base.Awake();

        //PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        if (!requestData)
            requestData = Resources.Load<RequestData>("SOData/RequestData");
    }

    #region Get Set
    public SceneType SceneType
    {
        get { return sceneType; }
        set { sceneType = value; }
    }
    public int HighScore
    {
        get { return highScrore; }
        set { highScrore = value; }
    }
    public int PrimogemOwn
    {
        get { return primogemOwn; }
        set { primogemOwn = value; }
    }
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
        set { player = value; }
    }
    public float PlayTime
    {
        get { return playTime; }
        set { playTime = value; 
        if(playTime > 5){
                warning = false;
                AudioManager.Instance.StopSFX("Time");
            };
        }
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
        EventManager.onGameOver += OnGameOver;
        EventManager.onSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        EventManager.onCollectItem -= OnCollectItem;
        EventManager.onGameOver -= OnGameOver;
        EventManager.onSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(SceneType sceneType)
    {
        ResetData();
        AudioManager.Instance.PlayBGM(
            sceneType == SceneType.MainMenu ? "MainMenu" : "Game");
    }
    private void ResetData()
    {
        warning = false;
        gameGameOver = false;
        gameStarted = false;
        points = 0;
        primogems = 0;
        playTime = GameConfig.initialPlayTime;
    }

    private void OnCollectItem(Item item)
    {
        switch (item.Rate)
        {
            case Rate.Normal:
                UpdatePoint(normalItemPoint);
                playTime += GameConfig.normalItemTime;
                OnGetPoint(item.transform, normalItemPoint);

                AudioManager.Instance.PlayCollectItem();
                break;
            case Rate.Rare:
                UpdatePoint(rareItemPoint);
                playTime += GameConfig.rareItemTime;
                OnGetPoint(item.transform, rareItemPoint);

                AudioManager.Instance.PlayCollectItem();
                break;
            case Rate.Special:
                UpdatePrimogem(primogemPoint);
                OnGetPrimogem(item.transform, primogemPoint);

                AudioManager.Instance.PlayCollectPrimogem();
                break;
        }
    }

    public void UpdatePoint(int newPoint)
    {
        points += newPoint;
        if (points < 0)
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
    public void UpdateHighScore()
    {
        if (points > highScrore)
        {
            highScrore = points;
            PlayFabManager.Instance.SubmitHighScore(highScrore);
        }
    }
    public void UpdatePrimogemOwn()
    {
        primogemOwn += primogems;
        PlayFabManager.Instance.AddPrimogem(primogems);
    }

    private void ProcessPlayTime()
    {
        if (!gameGameOver && gameStarted)
        {
            playTime -= Time.deltaTime;
            if (playTime <= 0)
            {
                playTime = 0;
                EventManager.GameOverAction();
            }
            else if(!warning && playTime <= 5)
            {
                warning = true;
                TimeWarning();
            }
            UIManager.Instance.UICanvas.GamePanel.SetPlayTime(playTime);
        }
    }
    public void TimeWarning()
    {
        if (warning)
        {
            Debug.Log("Time Warning");
            AudioManager.Instance.PlayTime();
        }
    }
    public void OnGameOver()
    {
        if (!gameGameOver)
        {
            gameGameOver = true;
            player.CanDie = true;
            player.Die();

            UpdatePrimogemOwn();
            UpdateHighScore();

            AudioManager.Instance.StopSFX("Time");
        }
    }

    public void OnGetPoint(Transform transform, int point)
    {
        UIManager.Instance.UICanvas.GamePanel.SetPointText(points);
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
        UIManager.Instance.UICanvas.GamePanel.SetPrimogemText(primogems);
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

    public static void SaveBuffData(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
        PlayerPrefs.Save();
    }
    public static float LoadBuffData(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            float value = PlayerPrefs.GetFloat(name);
            return value;
        }
        return 0;
    }

    public static void SaveIntData(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }

    public static int LoadIntData(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            int value = PlayerPrefs.GetInt(name);
            return value;
        }
        return 0;
    }
}