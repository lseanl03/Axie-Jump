using DG.Tweening;
using Spine.Unity;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = GameConfig.jumpForceInitial;
    [SerializeField] private bool canJump = false;
    [SerializeField] private bool isJuming = false;

    private PlayerAnim playerAnim;
    private Tween jumpTween;
    private void Awake()
    {
        playerAnim = GetComponent<PlayerAnim>();
    }
    private void Update()
    {
        HandleState();
        HandleAction();
    }

    private void HandleState()
    {
        if (!canJump && !isJuming)
        {
            if (IsGrounded())
            {
                canJump = true;
            }
        }
    }
    private void HandleAction()
    {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            JumpAction(true);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            JumpAction(false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.Instance.GameStarted) JumpToStartGame();
        }
    }

    private void JumpToStartGame()
    {
        var tween = transform.DOMoveY(GameConfig.jumpPosStartGame, GameConfig.jumpTime)
            .SetEase(Ease.InOutQuad);
    }

    private void JumpAction(bool isLeftDirection)
    {
        if (canJump && GameManager.Instance.GameStarted)
        {
            isJuming = true;
            canJump = false;
            jumpForce = !HasTrunkAbove() && CantMoveForward(isLeftDirection) ? 
                GameConfig.distanceTrunkSpawn : GameConfig.jumpForceInitial;
            Jump(isLeftDirection);
            Flip(isLeftDirection);
            jumpTween.OnComplete(() =>
            {
                isJuming = false;
                playerAnim.Idle();
            });
            Debug.Log(HasTrunkAbove() + " " + CantMoveForward(isLeftDirection));
            if (jumpForce == GameConfig.distanceTrunkSpawn) return;
            GameManager.Instance.TrunkController.TransitionTrunk();
        }
    }

    public void Jump(bool isLeftDirection)
    {
        playerAnim.Jump();
        float posX = isLeftDirection ? -GameConfig.jumpDistance : GameConfig.jumpDistance;
        if (CantMoveForward(isLeftDirection)) posX = 0;
        jumpTween = transform.DOMove(new Vector2(transform.position.x + posX, transform.position.y + jumpForce), GameConfig.jumpTime)
            .SetEase(Ease.InOutQuad);
    }

    public void Flip(bool isLeftDirection)
    {
        var x = isLeftDirection ? 1 : -1;
        transform.localScale = new Vector2(x, 1);
    }

    private bool CantMoveForward(bool isLeftDirection)
    {
        var cantMoveForward = isLeftDirection && transform.position.x <= GameConfig.minPos
            || !isLeftDirection && transform.position.x >= GameConfig.maxPos;
        return cantMoveForward;        
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Trunk"));
        return hit.collider != null;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * 0.1f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f), Vector2.up * GameConfig.distanceTrunkTransition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trunk"))
        {
            if (!GameManager.Instance.GameStarted)
            {
                GameManager.Instance.GameStarted = true;
            }
        }

    }

    private bool HasTrunkAbove()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.5f), 
            Vector2.up, GameConfig.distanceTrunkTransition, LayerMask.GetMask("Trunk"));
        return hit.collider != null;
    }
}
