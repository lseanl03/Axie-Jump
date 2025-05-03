using Spine.Unity;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool isSelected = false;
    private CharacterConfig characterConfig;
    private SkeletonAnimation anim;
    [SerializeField] private CharacterType characterType;

    public CharacterType CharacterType
    {
        get { return characterType; }
        set { characterType = value; }
    }

    public bool IsSelected
    {
        get { return isSelected; }
        set { isSelected = value; }
    }
    public CharacterConfig CharacterConfig
    {
        get { return characterConfig; }
        set { characterConfig = value; }
    }

    private void Awake()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }
    private void Start()
    {
        anim.skeletonDataAsset = characterConfig.skeletonDataAsset;
        anim.Initialize(true);
        Idle();
    }

    public void SetDataInit()
    {
        if (characterType != CharacterType.None)
        {
            var characterManager = CharacterManager.Instance;
            var config = characterManager.GetConfigWithType(characterType);
            if (config != null) characterConfig = config;
        }
    }

    public void TakeEffect()
    {
        anim.state.SetAnimation(0, characterConfig.collectItem, false);
        anim.state.AddAnimation(0, characterConfig.idle, true, 0);
    }

    public void Idle()
    {
        anim.state.SetAnimation(0, characterConfig.idle, true);
    }
}
