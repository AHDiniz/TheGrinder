using Godot;

namespace Movement
{
    public class PlatformerController : Node2D
    {
        [Export] private float _movementSpeed = 500;

        [Export] private float _gravity = 90;
        [Export] private float _maxFallSpeed = 1000;
        [Export] private float _minFallSpeed = 50;

        [Export] private float _jumpForce = 1500;

        [Export] private bool _canDash = true;
        [Export] private float _dashDuration = .5f;
        [Export] private float _dashSpeed = 1000;

        private bool _dashing;
        private float _dashTimer, _lastDir = 1f;
        private KinematicBody2D _body;
        private Vector2 _direction;

        public Vector2 Direction { get => _direction; set => _direction = value; }
        public bool Grounded { get => _body.IsOnFloor(); }
        public bool Dashing { get => _dashing; }
        public bool CanDash { get => _canDash; set => _canDash = value; }

        public override void _Ready()
        {
            _body = GetParent<KinematicBody2D>();
        }

        public override void _PhysicsProcess(float delta)
        {
            _direction.y += _gravity;

            if (_direction.y > _maxFallSpeed)
                _direction.y = _maxFallSpeed;
            
            if (_body.IsOnFloor())
                _direction.y = _minFallSpeed;

            if (Input.IsActionJustPressed("dash") && _canDash)
            {
                _dashing = true;
                _dashTimer = 0f;
            }

            if (!_dashing)
            {
                _direction.x = (Input.GetActionStrength("right") - Input.GetActionStrength("left")) * _movementSpeed;
                _lastDir = _direction.x != 0f ? Mathf.Sign(_direction.x) : _lastDir;
            }
            else
            {
                _dashTimer += delta;
                _direction.x = _lastDir * _dashSpeed;
                _direction.y = 0f;
                if (_dashTimer >= _dashDuration)
                {
                    _dashing = false;
                }
            }

            if (_body.IsOnFloor() && Input.IsActionJustPressed("jump"))
            {
                _direction.y -= _jumpForce;
            }

            _direction = _body.MoveAndSlide(_direction, Vector2.Up);
        }
    }
}