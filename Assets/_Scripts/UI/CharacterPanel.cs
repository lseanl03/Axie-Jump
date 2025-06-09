using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : PanelBase
{
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Button beforeArrow;
    [SerializeField] private Button afterArrow;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button selectButton;

    protected override void Awake()
    {
        base.Awake();
        closeButton.onClick.AddListener(HidePanel);
        selectButton.onClick.AddListener(OnSelectClick);
        beforeArrow.onClick.AddListener(BeforeArrowClick);
        afterArrow.onClick.AddListener(AfterArrowClick);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        CharacterManager.Instance.CharacterSelectedChange();
    }
    public override void HidePanel()
    {
        base.HidePanel();
        CharacterManager.Instance.CharacterDisable();
    }
    private void BeforeArrowClick()
    {
        CharacterManager.Instance.OnBeforeArrowClick();
    }
    private void AfterArrowClick()
    {
        CharacterManager.Instance.OnAfterArrowClick();
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
    public void OnSelectClick()
    {
        CharacterManager.Instance.SetCurrentCharacter();
        AudioManager.Instance.PlaySFX(AudioType.SelectClick);
    }
}
