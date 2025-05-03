using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public CharacterConfig[] characterConfigs;
}

[System.Serializable]
public class CharacterConfig
{
    public CharacterType characterType;
    public AnimationReferenceAsset idle;
    public AnimationReferenceAsset jump;
    public AnimationReferenceAsset die;
    public AnimationReferenceAsset collectItem;
    public AnimationReferenceAsset[] randomIdles;
    public SkeletonDataAsset skeletonDataAsset;
}
