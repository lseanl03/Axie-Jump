using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Button beforeArrow;
    [SerializeField] private Button afterArrow;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private CanvasGroup bgCanvasGroup;
    [SerializeField] private GameObject characterMenu;
    private void Awake()
    {
        bgCanvasGroup.gameObject.SetActive(false);
        characterMenu.SetActive(false);
    }
    public void ShowCharacterPanel()
    {
        characterMenu.SetActive(true);
        bgCanvasGroup.gameObject.SetActive(true);
        bgCanvasGroup.alpha = 0;
        bgCanvasGroup.DOFade(1, 0.5f).SetUpdate(true);

        CharacterManager.Instance.CharacterSelectedChange();
    }
    public void HideSettingPanel()
    {
        characterMenu.SetActive(false);
        bgCanvasGroup.DOFade(0, 0.5f).SetUpdate(true);
        bgCanvasGroup.gameObject.SetActive(false);

        CharacterManager.Instance.CharacterDisable();

    }
    public void SetName(Character character)
    {
        characterNameText.text = character.CharacterType.ToString();
    }
    public void SelectedButtonState(bool state)
    {
        var text = selectButton.GetComponentInChildren<TextMeshProUGUI>();
        text.text = state ? "Đã chọn" : "Chọn" ;
        selectButton.interactable = state ? false : true;
    }
}
