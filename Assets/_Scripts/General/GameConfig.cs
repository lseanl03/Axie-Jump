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
    public static float jumpPosStartGame = -2.5f;

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

    [Header("Item")]
    public static int normalItemRate = 70;
    public static int rareItemRate = 20;
    public static int specialItemRate = 10;
    public static float itemSpawnPosInTrunk = 1.5f;
    public static int itemSpawnRate = 30;

    [Header("Enemy")]
    public static int enemySpawnRate = 40;

    [Header("Buff")]
    public static int buffSpawnRate = 5;

    [Header("Game")]
    public static float initialPlayTime = 60f;
    public static int normalItemPoint = 10;
    public static int rareItemPoint = 20;
    public static int primogemPoint = 100;
    public static int normalItemTime = 1;
    public static int rareItemTime = 2;
    public static float waitContinueGameTime = 3f;

    //<color=#FFFF00>1 thuốc tăng nguyên thạch lấy x5 Ramen (100 điểm)</color>
}