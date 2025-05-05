using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private int selectedIndex = 0;
    [SerializeField] private GameObject characterListObj;
    [SerializeField] private GameObject characterInMainMenuObj;

    private CharacterData charactersData;
    private Character currentCharacter;
    private List<Character> characterList = new List<Character>();
    private List<CharacterInMainMenu> characterInMainMenuList 
        = new List<CharacterInMainMenu>();
    private UIManager uiManager => UIManager.Instance;
    protected override void Awake()
    {
        base.Awake();
        charactersData = Resources.Load<CharacterData>("SOData/CharacterData");
    }
    private void Start()
    {
        characterInMainMenuObj.SetActive(true);
        GetCharacterInMainMenuInit();
        GetCharacterListInit();
        currentCharacter = characterList[0];
        SetPlayer();
    }
    private void OnEnable()
    {
        EventManager.onSceneChanged += OnSceneChanged;
    }
    private void OnDisable()
    {
        EventManager.onSceneChanged -= OnSceneChanged;
    }
    public void OnSceneChanged(SceneType sceneType)
    {
        characterInMainMenuObj.SetActive(sceneType == SceneType.MainMenu);
        SetPlayer();
    }
    void GetCharacterListInit()
    {
        for (int i = 0; i < characterListObj.transform.childCount; i++)
        {
            var obj = characterListObj.transform.GetChild(i);
            var character = obj.GetComponent<Character>();
            if (character)
            {
                characterList.Add(character);
                character.SetDataInit();
            }
        }
    }
    void GetCharacterInMainMenuInit()
    {
        if(GameManager.Instance.SceneType != SceneType.MainMenu) return;
        for (int i = 0; i < characterInMainMenuObj.transform.childCount; i++)
        {
            var obj = characterInMainMenuObj.transform.GetChild(i);
            var character = obj.GetComponent<CharacterInMainMenu>();
            if (character)
            {
                characterInMainMenuList.Add(character);
                character.SetDataInit();
                character.RandomIdles();
            }
        }
    }


    public void OnBeforeArrowClick()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characterList.Count -1;
        CharacterViewChange();
    }
    public void OnAfterArrowClick()
    {
        currentIndex++;
        if (currentIndex >= characterList.Count) currentIndex = 0;
        CharacterViewChange();
    }

    public void CharacterViewChange()
    {
        var characterPanel = uiManager.UICanvas.CharacterPanel;
        for (int i = 0; i < characterList.Count; i++)
        {
            if (i == currentIndex)
            {
                currentCharacter = characterList[i];
                currentCharacter.gameObject.SetActive(true);
                characterPanel.SetName(currentCharacter);
            }
            else
            {
                characterList[i].gameObject.SetActive(false);
            }
        }
        characterPanel.SelectedButtonState(currentCharacter.IsSelected);
    }

    public void CharacterSelectedChange()
    {
        var characterPanel = uiManager.UICanvas.CharacterPanel;
        for (int i = 0; i < characterList.Count; i++)
        {
            if (i == selectedIndex)
            {
                currentIndex = i;
                currentCharacter = characterList[i];
                currentCharacter.gameObject.SetActive(true);
                characterPanel.SetName(currentCharacter);
            }
            else
            {
                characterList[i].gameObject.SetActive(false);
            }
        }
        characterPanel.SelectedButtonState(currentCharacter.IsSelected);
    }

    public void CharacterDisable()
    {
        if(currentCharacter) 
            currentCharacter.gameObject.SetActive(false);
    }

    public void SetPlayer()
    {
        PlayerController player = GameManager.Instance.Player;
        if (player)
        {
            var playerAnim = player.GetComponent<PlayerAnim>();
            playerAnim.IdleAnim = currentCharacter.CharacterConfig.idle;
            playerAnim.JumpAnim = currentCharacter.CharacterConfig.jump;
            playerAnim.DieAnim = currentCharacter.CharacterConfig.die;
            playerAnim.CollectItemAnim = currentCharacter.CharacterConfig.collectItem;
            playerAnim.RandomIdlesAnim = currentCharacter.CharacterConfig.randomIdles;
            playerAnim.Anim.skeletonDataAsset 
                = currentCharacter.CharacterConfig.skeletonDataAsset;

            playerAnim.Anim.Initialize(true);
            playerAnim.RandomIdles();

            SetCharacterSelected();
        }
    }
    public CharacterConfig GetConfigWithType(CharacterType type)
    {
        foreach (var character in charactersData.characterConfigs)
        {
            if (character.characterType == type) return character;
        }
        return null;
    }

    public bool IsCharacterSelected()
    {
        return currentCharacter.IsSelected;
    }

    public void SetCharacterSelected()
    {
        foreach (var character in characterList)
        {
            character.IsSelected = character == currentCharacter ? true : false;
        }
        selectedIndex = currentIndex;
        uiManager.UICanvas.CharacterPanel
            .SelectedButtonState(currentCharacter.IsSelected);
    }
}
