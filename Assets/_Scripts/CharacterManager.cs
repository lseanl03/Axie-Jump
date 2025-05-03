using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : Singleton<CharacterManager>
{
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private int selectedIndex = 0;
    [SerializeField] private GameObject characterListObj;
    [SerializeField] private CharacterPanel characterPanel;

    private CharacterData charactersData;
    private Character currentCharacter;
    private List<Character> characterList = new List<Character>();
    protected override void Awake()
    {
        base.Awake();
        charactersData = Resources.Load<CharacterData>("SOData/CharacterData");
    }
    private void Start()
    {
        GetCharacterListInit();
        
        currentCharacter = characterList[0];
        SetPlayer();
    }

    public CharacterPanel CharacterPanel
    {
        get { return characterPanel; }
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
        characterPanel.SelectedButtonState(currentCharacter.IsSelected);
    }
}
