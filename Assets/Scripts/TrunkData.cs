using UnityEngine;

[CreateAssetMenu(fileName = "TrunkData", menuName = "ScriptableObjects/TrunkData", order = 1)]
public class TrunkData : ScriptableObject
{
    public Trunk[] trunkPrefabs;
}
