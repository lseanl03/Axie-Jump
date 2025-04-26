using UnityEngine;

public class GameConfig
{
    [Header("Player")]
    public static float minPos = -4f;
    public static float maxPos = 4f;
    public static float jumpXDistance = 4f;
    public static float normalJumpForce = 0.5f;
    public static float highJumpForce = 5f;
    public static float jumpTime = 0.4f;
    public static float jumpPosStartGame = 0.5f;

    [Header("Trunk")]
    public static float distanceTrunkSpawn = 5f;
    public static float distanceTrunkTransition = 5f;
    public static float trunkTransitionTime = 0.4f;

    [Header("Ground")]
    public static float groundTransitionDistance = 5f;
    public static float groundTransitionTime = 0.4f;

    [Header("BG")]
    public static float bgTransitionDistance = 0.02f;
    public static float bgTransitionTime= 0.4f;

    [Header("Game")]
    public static float closeOverlay = 1f;
    public static float openOverlay = 1f;
}