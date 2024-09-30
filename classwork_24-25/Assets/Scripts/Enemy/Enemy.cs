using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float _HIT_RANGE = 1.0f;

    [SerializeField] private Animator _animator;
    private Transform _target;
    private GameManager _gameManager;

    private EnemyMovement _enemyMovement;
    private EnemyAnimation _enemyAnimation;


    private Vector2 _hitOriginPos;
    private Vector2 _hitDir;
    private bool _isAttacking;
    private bool _isDead;

    void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        _enemyMovement = new EnemyMovement(this.transform, _target);
        _enemyAnimation = new EnemyAnimation(_animator, this.transform);

        _gameManager.OnEnemyTakeDamage += TakeDamage;
    }

    void Update()
    {
        if (_isAttacking || _isDead || _target == null)
            return;

        _isAttacking = _enemyMovement.MovementOrAttack(out _hitDir);
        _enemyAnimation.SetAnimationByDirection(_hitDir);

        if (_isAttacking)
            _enemyAnimation.SetAttackAnimation();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_hitOriginPos, _HIT_RANGE);
    }
    private void Attack()
    {
        _hitOriginPos = (Vector2)transform.position + _hitDir;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(_hitOriginPos, _HIT_RANGE, _hitDir, 0.0f);

        string res = Time.time + " => ";
        foreach (RaycastHit2D hit in hits)
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                res += hit.transform.name + " ";
                _gameManager.PlayerTakeDamage();
            }

        Debug.Log(res);
    }
    public void OnAttackAnimationStart(float time)
    {
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
        _enemyAnimation.ResetAttackAnimation();
        _isAttacking = false;
    }
    public void OnDeathAnimationStart(float time)
    {
        _gameManager.OnEnemyTakeDamage -= TakeDamage;
        Destroy(this.gameObject.GetComponent<Collider2D>());
        StartCoroutine(Wait(time));

        IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage()
    {
        _isDead = true;
        StopAllCoroutines();
        _enemyAnimation.PlayDeathAnimation();
    }
}
