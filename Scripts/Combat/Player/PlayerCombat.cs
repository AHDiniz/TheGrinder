using Godot;
using Movement;

namespace Combat.Player
{
    public class PlayerCombat : Node2D
    {
        [Export] private int _lightDamage;
        [Export] private int _heavyDamage;

        private Node _owner;
        private DamageArea _damageAreaRight;
        private DamageArea _damageAreaLeft;
        private PlatformerController _controller;

        private bool _attacking;
        private bool _lightAttack;
        private bool _heavyAttack;
        private Vector2 _lastDir;

        public bool Attacking { get => _attacking; set => _attacking = value; }
        public DamageArea RightDamageArea { get => _damageAreaRight; }
        public DamageArea LeftDamageArea { get => _damageAreaLeft; }

        public override void _Ready()
        {
            _owner = GetParent();
            _damageAreaRight = _owner.GetNode<DamageArea>("DamageAreaRight");
            _damageAreaLeft = _owner.GetNode<DamageArea>("DamageAreaLeft");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");

            _lastDir = new Vector2(1f, 0f);
        }

        public override void _Process(float delta)
        {
            _lastDir = _controller.Direction.x != 0f ? _controller.Direction : _lastDir;

            if (Input.IsActionJustPressed("melee") && !_controller.Dashing)
            {
                _attacking = true;
            }

            if (_lastDir.x > 0f && _attacking)
            {
                _damageAreaRight.Active = true;
                _damageAreaLeft.Active = false;
                GD.Print("Right");
            }

            if (_lastDir.x < 0f && _attacking)
            {
                _damageAreaLeft.Active = true;
                _damageAreaRight.Active = false;
                GD.Print("Left");
            }

            if (!_attacking)
            {
                _damageAreaLeft.Active = _damageAreaRight.Active = false;
            }
        }
    }
}