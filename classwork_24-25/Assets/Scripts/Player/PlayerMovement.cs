public class PlayerMovement
{
    private const float _PLAYER_MOVEMENT_SPEED = 6.0f;
    private const float _PLAYER_BOOST_SPEED = 10.0f;

    private UnityEngine.Transform _transform;
    private TurnSide _turnSide = TurnSide.downSide;

    public PlayerMovement(UnityEngine.Transform transform)
    {
        _transform = transform;
    }
    ~PlayerMovement()
    {
    }

    public void MovementInput(out UnityEngine.Vector2 dir, out TurnSide turnSide)
    {
        if (TryGetMovementDirectionByInput(out dir))
        {
            DetectTurnSide(dir);
            MoveByDirection(dir);
        }
        turnSide = _turnSide;
    }
    private bool TryGetMovementDirectionByInput(out UnityEngine.Vector2 dir)
    {
        float dirX = UnityEngine.Input.GetAxisRaw("Horizontal");
        float dirY = UnityEngine.Input.GetAxisRaw("Vertical");
        dir = new UnityEngine.Vector2(dirX, dirY);
        return dir != UnityEngine.Vector2.zero;
    }
    private void MoveByDirection(UnityEngine.Vector2 dir)
    {
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightShift))
            _transform.Translate(dir * _PLAYER_BOOST_SPEED * UnityEngine.Time.deltaTime);
        else
            _transform.Translate(dir * _PLAYER_MOVEMENT_SPEED * UnityEngine.Time.deltaTime);
    }

    private void DetectTurnSide(UnityEngine.Vector2 dir)
    {
        if (System.Math.Abs(dir.x) > System.Math.Abs(dir.y))
        {
            if (dir.x < 0) _turnSide = TurnSide.leftSide;
            else _turnSide = TurnSide.rightSide;
        }
        else
        {
            if (dir.y < 0) _turnSide = TurnSide.downSide;
            else _turnSide = TurnSide.upSide;
        }
    }
}
