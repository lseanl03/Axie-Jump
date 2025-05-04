using Spine;
using Spine.Unity;
using UnityEngine;

public class CharacterInMainMenu : MonoBehaviour
{
    private SkeletonAnimation anim;
    private CharacterConfig characterConfig;
    [SerializeField] private CharacterType characterType;

    private void Awake()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }
    public void RandomIdles()
    {
        if(characterConfig == null) return;
        int randomIndex = Random.Range(0, characterConfig.randomIdles.Length);
        anim.state.SetAnimation(0, characterConfig.randomIdles[randomIndex], true);
    }

    public void SetDataInit()
    {
        if (characterType != CharacterType.None)
        {
            var characterManager = CharacterManager.Instance;
            var config = characterManager.GetConfigWithType(characterType);
            if (config != null) characterConfig = config;

            anim.skeletonDataAsset = characterConfig.skeletonDataAsset;
            anim.Initialize(true);
        }
    }

    public CharacterConfig CharacterConfig
    {
        get { return characterConfig; }
        set { characterConfig = value; }
    }
}
