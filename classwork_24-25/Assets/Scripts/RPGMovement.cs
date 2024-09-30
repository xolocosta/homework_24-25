using UnityEngine;

public class RPGMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _left, _right, _up, _down;
    void Start()
    {
    }

    void Update()
    {
        MovementLogic();
    }
    private void MovementLogic()
    {
        Inputs();

        if (_left && !_right)
        {
            _spriteRenderer.sprite = _sprites[0];
            transform.localScale = new Vector3(-1f, 1f, 1f);
            //_spriteRenderer.flipY = true;
            Move(Vector2.left);
        } else if (!_left && _right)
        {
            _spriteRenderer.sprite = _sprites[0];
            transform.localScale = new Vector3(1f, 1f, 1f);
            //_spriteRenderer.flipY = false;
            Move(Vector2.right);
        }

        if (_up && !_down) 
        {
            _spriteRenderer.sprite = _sprites[1];
            Move(Vector2.up); 
        } else if (!_up && _down)
        {
            _spriteRenderer.sprite = _sprites[2];
            Move(Vector2.down);
        }

        if (!_left && !_right && !_up && !_down) _rb.velocity = Vector2.zero;

        if (_rb.velocity.magnitude >= _maxSpeed)
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
    }
    private void Inputs()
    {
        _left = Input.GetKey(KeyCode.A);
        _right = Input.GetKey(KeyCode.D);
        _up = Input.GetKey(KeyCode.W);
        _down = Input.GetKey(KeyCode.S);
    }
    private void Move(Vector2 direction)
        => _rb.AddForce(direction* _speed * Time.deltaTime);
}
