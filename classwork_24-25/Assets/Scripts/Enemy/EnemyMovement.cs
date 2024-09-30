public class EnemyMovement
{
    private const float _ENEMY_MOVEMENT_SPEED = 3.0f;

    private UnityEngine.Transform _transform;
    private UnityEngine.Transform _target;
    private UnityEngine.Transform _moveTarget;

    private TurnSide _turnSide = TurnSide.downSide;

    public EnemyMovement(UnityEngine.Transform transform, UnityEngine.Transform target)
    {
        _transform = transform;
        _target = target;
    }
    ~EnemyMovement()
    {
    }
    public bool MovementOrAttack(out UnityEngine.Vector2 dir)
    {
        if (_moveTarget == null)
        {
            GetMovementDirection(_target, out dir);
            
            if (UnityEngine.Vector2.Distance(_transform.position, _target.position) >= 1.5f)
                MoveByDirection(dir);
            else
                return true;
        } 
        else
        {
            GetMovementDirection(_moveTarget, out dir);

            if (UnityEngine.Vector2.Distance(_transform.position, _moveTarget.position) >= 0.2f)
                MoveByDirection(dir);
            else
                _moveTarget = null;
        }

        return false;
    }
    private void GetMovementDirection(UnityEngine.Transform target, out UnityEngine.Vector2 dir)
    {
        dir = (target.position - _transform.position).normalized;
    }
    private void MoveByDirection(UnityEngine.Vector2 dir)
    {
        _transform.Translate(dir * _ENEMY_MOVEMENT_SPEED * UnityEngine.Time.deltaTime);
    }
    public void SetMoveTarget(UnityEngine.Transform target)
    {
        _moveTarget = target;
    }
}
