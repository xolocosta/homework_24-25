using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float _HIT_RANGE = 1.0f;

    private System.Collections.Generic.Dictionary<TurnSide, Vector2> _dirByTurn =
        new System.Collections.Generic.Dictionary<TurnSide, Vector2>() {
            { TurnSide.leftSide, Vector2.left },
            { TurnSide.rightSide, Vector2.right },
            { TurnSide.upSide, Vector2.up },
            { TurnSide.downSide, Vector2.down },
        };

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private GameManager _gameManager;

    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerHPManager _playerHPManager;

    private TurnSide _turnSide = default;
    private Vector2 _hitOriginPos;

    private bool _isAttacking;
    private bool _isDead;
    private void Start()
    {
        _playerAnimation = new PlayerAnimation(_animator, this.transform);
        _playerMovement = new PlayerMovement(this.transform);
        _playerHPManager = new PlayerHPManager(_hearts);

        _gameManager.OnPlayerTakeDamage += TakeDamage;
    }
    private void Update()
    {
        if (_isAttacking || _isDead)
            return;

        _playerMovement.MovementInput(out Vector2 dir, out _turnSide);
        _playerAnimation.SetAnimationByDirection(dir);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerAnimation.SetAttackAnimation();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_hitOriginPos, _HIT_RANGE);
    }
    private void Attack()
    {
        Vector2 hitDir = _dirByTurn[_turnSide];
        _hitOriginPos = (Vector2)transform.position + hitDir;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_hitOriginPos, _HIT_RANGE, hitDir, 0.0f);

        string res = Time.time + " => ";
        foreach (RaycastHit2D hit in hits)
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                res += hit.transform.name + " ";
                _gameManager.EnemyTakeDamage();
            }

        Debug.Log(res);
    }
    public void OnAttackAnimationStart(float time)
    {
        _isAttacking = true;
        _playerAnimation.SetAttackAnimation();
        StartCoroutine(Wait(time));

        IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            Attack();
            OnAttackAnimationFinish();
        }
    }
    public void OnAttackAnimationFinish()
    {
        _playerAnimation.ResetAttackAnimation();
        _isAttacking = false;
    }
    public void OnDeathAnimationStart(float time)
    {
        _gameManager.OnPlayerTakeDamage -= TakeDamage;
        StartCoroutine(Wait(time));
        
        IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage()
    {
        if (_playerHPManager.LoseHP())
        {
            _isDead = true;
            StopAllCoroutines();
            _playerAnimation.PlayDeathAnimation();
        }
    }
}


public enum TurnSide {
    rightSide = 0,
    leftSide = 1,
    upSide = 2,
    downSide = 3
}