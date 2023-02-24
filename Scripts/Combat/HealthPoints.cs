using Godot;

namespace Combat
{
    public class HealthPoints : Node2D
    {
        [Export] private int _maxHP = 3;
        [Export] private float _cooldown = 0.5f;

        [Signal] public delegate void OnDeath();
        [Signal] public delegate void OnHit();
        [Signal] public delegate void OnHeal();

        private bool _canGetHurt = true;
        private bool _invincible;
        private int _currentHP, _prevHP;
        private float _timer;

        public bool Invincible { get => _invincible; }
        public bool CanGetHurt { get => _canGetHurt; set => _canGetHurt = value; }
        public int MaxHP { get => _maxHP; set => _maxHP = value; }
        public int PrevHP { get => _prevHP; }
        public int CurrentHP
        {
            get => _currentHP;
            set
            {
                _prevHP = _currentHP;
                if (_invincible || !_canGetHurt)
                {
                    _currentHP = _currentHP;
                }
                else
                {
                    _currentHP = value;
                    if (_currentHP < _prevHP)
                    {
                        if (_currentHP <= 0f)
                            EmitSignal(nameof(OnDeath));
                        else EmitSignal(nameof(OnHit));
                    }
                    if (_currentHP > _prevHP)
                        EmitSignal(nameof(OnHeal));
                    _invincible = true;
                    _timer = 0f;
                }
            }
        }

        public override void _Ready()
        {
            _currentHP = _maxHP;
        }

        public override void _Process(float delta)
        {
            if (_invincible)
            {
                _timer += delta;
                if (_timer >= _cooldown)
                {
                    _invincible = false;
                }
            }
        }
    }
}