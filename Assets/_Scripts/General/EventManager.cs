using UnityEngine;

public class EventManager
{
    public delegate void OnClickJump(bool isLeft);
    public static event OnClickJump onClickJump;

    public static void ClickJumpAction(bool isLeft)
    {
        onClickJump?.Invoke(isLeft);
    }

    public delegate void TransitionTrunk();
    public static event TransitionTrunk onTransitionTrunk;

    public static void TransitionTrunkAction()
    {
        onTransitionTrunk?.Invoke();
    }

    public delegate void OnGameStart();
    public static event OnGameStart onGameStart;

    public static void OnGameStartAction()
    {
        onGameStart?.Invoke();
    }

    public delegate void OnNormalJump();
    public static event OnNormalJump onNormalJump;

    public static void NormalJumpAction()
    {
        onNormalJump?.Invoke();
    }

    public delegate void OnGameOver();
    public static event OnGameOver onGameOver;

    public static void GameOverAction()
    {
        onGameOver?.Invoke();
    }
}
