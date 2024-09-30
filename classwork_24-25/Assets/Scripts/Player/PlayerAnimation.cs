public class PlayerAnimation
{
    private const string _WALK_FRONT_ANIMATION_NAME = "player_walk_front";
    private const string _WALK_BACK_ANIMATION_NAME = "player_walk_back";
    private const string _WALK_SIDE_ANIMATION_NAME = "player_walk_side";

    private const string _IDLE_FRONT_ANIMATION_NAME = "player_idle_front";
    private const string _IDLE_BACK_ANIMATION_NAME = "player_idle_back";
    private const string _IDLE_SIDE_ANIMATION_NAME = "player_idle_side";

    private const string _ATTACK_FRONT_ANIMATION_NAME = "player_attack_front";
    private const string _ATTACK_BACK_ANIMATION_NAME = "player_attack_back";
    private const string _ATTACK_SIDE_ANIMATION_NAME = "player_attack_side";

    private const string _DEATH_ANIMATION_NAME = "player_death";

    private UnityEngine.Animator _animator;
    private UnityEngine.Transform _transform;

    private string _lastAnimation = "";
    private bool _left;
    private bool _isAttacking;

    public PlayerAnimation(UnityEngine.Animator animator, UnityEngine.Transform transform)
    {
        _animator = animator;
        _transform = transform;
    }
    ~PlayerAnimation()
    {
    }

    public void SetAttackAnimation()
    {
        _isAttacking = true;
        SetAttackAnimationByTurn();
    }
    public void ResetAttackAnimation()
    {
        SetIdleAnimation();
        _isAttacking = false;
    }

    public void SetAnimationByDirection(UnityEngine.Vector2 dir)
    {
        if (_isAttacking)
            return;

        if (dir != UnityEngine.Vector2.zero)
        {
            if (System.Math.Abs(dir.x) > System.Math.Abs(dir.y))
                SetHorAnimation(dir);
            else
                SetVertAnimation(dir);
        } else
        {
            SetIdleAnimation();
        }
    }
    private void SetHorAnimation(UnityEngine.Vector2 dir)
    {
        if (dir.x < 0)
        {
            _transform.localScale = new UnityEngine.Vector3(-1, 1);
            _left = true;
        }
        else
        {
            _transform.localScale = new UnityEngine.Vector3(1, 1);
            _left = false;
        }

        ChangeAnimation(_WALK_SIDE_ANIMATION_NAME);        
    }
    private void SetVertAnimation(UnityEngine.Vector2 dir)
    {
        if (_left)
        {
            _transform.localScale = new UnityEngine.Vector3(1, 1);
            _left = false;
        }

        if (dir.y < 0)
        {
            ChangeAnimation(_WALK_FRONT_ANIMATION_NAME);
        }
        else
        {
            ChangeAnimation(_WALK_BACK_ANIMATION_NAME);
        }
    }
    private void SetIdleAnimation()
    {
        string animation = _lastAnimation;

        switch (_lastAnimation)
        {
            case _WALK_FRONT_ANIMATION_NAME:
                animation = _IDLE_FRONT_ANIMATION_NAME;
                break;
            case _WALK_BACK_ANIMATION_NAME:
                animation = _IDLE_BACK_ANIMATION_NAME;
                break;
            case _WALK_SIDE_ANIMATION_NAME:
                animation = _IDLE_SIDE_ANIMATION_NAME;
                break;
        }

        _lastAnimation = animation;
        _animator.Play(animation);
    }
    private void SetAttackAnimationByTurn()
    {
        string animation = "";

        switch (_lastAnimation)
        {
            case _WALK_FRONT_ANIMATION_NAME:
                animation = _ATTACK_FRONT_ANIMATION_NAME;
                break;
            case _WALK_BACK_ANIMATION_NAME:
                animation = _ATTACK_BACK_ANIMATION_NAME;
                break;
            case _WALK_SIDE_ANIMATION_NAME:
                animation = _ATTACK_SIDE_ANIMATION_NAME;
                break;

            case _IDLE_FRONT_ANIMATION_NAME:
                animation = _ATTACK_FRONT_ANIMATION_NAME;
                break;
            case _IDLE_BACK_ANIMATION_NAME:
                animation = _ATTACK_BACK_ANIMATION_NAME;
                break;
            case _IDLE_SIDE_ANIMATION_NAME:
                animation = _ATTACK_SIDE_ANIMATION_NAME;
                break;
                
        }

        _animator.Play(animation);
    }
    public void PlayDeathAnimation()
    {
        _animator.Play(_DEATH_ANIMATION_NAME);
    }
    public void ChangeAnimation(string animation)
    {
        if (_lastAnimation != animation)
        {
            _animator.Play(animation);
            _lastAnimation = animation;
        }
    }
}
