using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Properties
    [SerializeField] private float jumpForce = GameConfig.normalJumpForce;

    [Header("State")]
    [SerializeField] private bool canJump = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isDied = false;

    private PlayerAnim playerAnim;
    private Tween jumpTween;
    private BoxCollider2D boxCollider2D;
    private Coroutine diedCoroutine;
    #endregion

    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnim>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        EventManager.onClickJump += OnJumpInput;
        EventManager.onGameStart += JumpToStartGame;
    }

    private void OnDisable()
    {
        EventManager.onClickJump -= OnJumpInput;
        EventManager.onGameStart -= JumpToStartGame;
    }
    private void Update()
    {
        if (!isDied)
        {
            HandleState();
            HandleInput();
        }
        
    }

    private void OnJumpInput(bool isLeftDir)
    {
        if (!canJump || !GameManager.Instance.GameStarted) return;

        isJumping = true;
        canJump = false;

        jumpForce = ShouldJumpHigher(isLeftDir) ?
            GameConfig.highJumpForce : GameConfig.normalJumpForce;

        Jump(isLeftDir);
        Flip(isLeftDir);

        jumpTween.OnComplete(() => {
            isJumping = false;
                playerAnim.Idle();
        });

        if (jumpForce != GameConfig.highJumpForce)
        {
            EventManager.NormalJumpAction();
        }
    }

    private void HandleState()
    {
        if (!canJump && !isJumping && IsGrounded())
        {
            canJump = true;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventManager.ClickJumpAction(true);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventManager.ClickJumpAction(false);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.GameStarted)
        {
            EventManager.OnGameStartAction();
        }
    }

    #region State

    /// <summary>
    /// Trạng thái nhảy
    /// </summary>
    /// <param name="isLeftDir"></param>
    private void Jump(bool isLeftDir)
    {
        playerAnim.Jump();

        float posX = isLeftDir ? -GameConfig.jumpXDistance : 
            GameConfig.jumpXDistance;
        if (CantMoveForward(isLeftDir)) posX = 0;

        jumpTween = transform.DOMove(new Vector2(
            transform.position.x + posX, transform.position.y + jumpForce),
            GameConfig.jumpTime
        ).SetEase(Ease.InOutQuad);
    }

    /// <summary>
    /// Trạng thái chết
    /// </summary>
    private void Die()
    {
        isDied = true;
        boxCollider2D.enabled = false;

        jumpTween.Kill();
        playerAnim.Die();

        if (diedCoroutine != null) StopCoroutine(diedCoroutine);
        diedCoroutine = StartCoroutine(DiedCoroutine());
    }
    #endregion

    /// <summary>
    /// Lật nhân vật theo hướng di chuyển
    /// </summary>
    /// <param name="isLeftDirection"></param>
    public void Flip(bool isLeftDirection)
    {
        float scaleX = isLeftDirection ? 1 : -1;
        transform.localScale = new Vector2(scaleX, 1);
    }

    /// <summary>
    /// Nhảy lần đầu để bắt đầu game
    /// </summary>
    private void JumpToStartGame()
    {
        transform.DOMoveY(GameConfig.jumpPosStartGame, GameConfig.jumpTime)
            .SetEase(Ease.InOutQuad);
    }

    #region Bool Check
    /// <summary>
    /// Trả về true nếu đang đứng trên gỗ
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            0.1f,
            LayerMask.GetMask("Trunk")
        );
        return hit.collider != null;
    }

    /// <summary>
    /// Trả về true nếu không thể di chuyển tiếp theo hướng hiện tại
    /// </summary>
    /// <param name="isLeftDirection"></param>
    /// <returns></returns>
    private bool CantMoveForward(bool isLeftDirection)
    {
        return (isLeftDirection && transform.position.x <= GameConfig.minPos) ||
               (!isLeftDirection && transform.position.x >= GameConfig.maxPos);
    }

    /// <summary>
    /// Trả về true nếu có gỗ phía trên đầu
    /// </summary>
    /// <returns></returns>
    private bool HasTrunkAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y + 0.5f),
            Vector2.up,
            GameConfig.distanceTrunkTransition,
            LayerMask.GetMask("Trunk")
        );
        return hit.collider != null;
    }

    /// <summary>
    /// Trả về true nếu phía trên đầu không có gỗ trên đầu, 
    /// Có thể nhảy cao hơn
    /// </summary>
    /// <param name="isLeftDirection"></param>
    /// <returns></returns>
    private bool ShouldJumpHigher(bool isLeftDirection)
    {
        return !HasTrunkAbove() && CantMoveForward(isLeftDirection);
    }
    #endregion

    #region Debug

    /// <summary>
    /// Vẽ đường kiểm tra trong editor
    /// </summary>
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(
            new Vector2(transform.position.x, transform.position.y + 0.5f),
            Vector2.up * GameConfig.distanceTrunkTransition
        );
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trunk") &&
            !GameManager.Instance.GameStarted)
        {
            GameManager.Instance.GameStarted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HiddenBox"))
        {
            Die();
        }
        else if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                EventManager.CollectItemAction(item.Rate);
                item.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Coroutine
    private IEnumerator DiedCoroutine()
    {
        yield return new WaitForSeconds(1);
        EventManager.GameOverAction();
    }
    #endregion
}
