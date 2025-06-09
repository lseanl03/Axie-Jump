using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI primogemText;
    [SerializeField] private Button clickToStartButton;
    [SerializeField] private GameObject mainMenuMenu;

    private void Awake()
    {
        mainMenuMenu.SetActive(true);
        clickToStartButton.onClick.AddListener(OnClickToStartClick);
    }

    private void Start()
    {
        SetHighScore(GameManager.Instance.HighScore);
        SetPrimogem(GameManager.Instance.PrimogemOwn);
    }

    private void OnEnable()
    {
        EventManager.onSceneChanged += OnSceneChanged;
    }
    private void OnDisable()
    {
        EventManager.onSceneChanged -= OnSceneChanged;
    }

    public void SetHighScore(int score)
    {
        highScoreText.text = score.ToString();
    }

    public void SetPrimogem(int primogem)
    {
        primogemText.text = primogem.ToString();
    }

    public void OnClickToStartClick()
    {
        LoadingManager.Instance.TransitionLevel(SceneType.Game);
    }

    private void OnSceneChanged(SceneType sceneType)
    {
        mainMenuMenu.SetActive(sceneType == SceneType.MainMenu);
        if (sceneType == SceneType.MainMenu)
        {
            SetHighScore(GameManager.Instance.HighScore);
            SetPrimogem(GameManager.Instance.PrimogemOwn);
        }
    }
}
