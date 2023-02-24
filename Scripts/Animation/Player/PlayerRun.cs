using Godot;
using States;
using Movement;
using Combat;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerRun : Node2D, IState
    {
        private Node _owner;
        
        private HealthPoints _hp;
        private PlayerCombat _combat;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;
        
        private PlayerHit _hp;
        private PlayerIdle _idle;
        private PlayerFall _fall;
        private PlayerDash _dash;
        private PlayerDeath _death;
        private PlayerAttack _attack;
        private PlayerJumpStart _jump;

        public override void _Ready()
        {
            _owner = GetParent();
            
            _hp = _owner.GetNode<HealthPoints>("HealthPoints");
            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            
            _hit = _owner.GetNode<PlayerHit>("PlayerHit");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
            _fall = _owner.GetNode<PlayerFall>("PlayerFall");
            _dash = _owner.GetNode<PlayerDash>("PlayerDash");
            _death = _owner.GetNode<PlayerDeath>("PlayerDeath");
            _attack = _owner.GetNode<PlayerAttack>("PlayerAttack");
            _jump = _owner.GetNode<PlayerJumpStart>("PlayerJumpStart");
        }

        void IState.Initialize()
        {
        }

        void IState.Execute()
        {
            _sprite.Play("run");
        }

        IState IState.NextState()
        {
            if (_hp.CurrentHP == 0)
            {
                return _death;
            }
            else if (_hp.CurrentHP < _hp.PrevHP)
            {
                return _hit;
            }
            else if (_combat.Attacking)
            {
                return _attack;
            }
            else if (_controller.Dashing)
            {
                return _dash;
            }
            else if (_controller.Grounded)
            {
                if (Input.IsActionJustPressed("jump"))
                {
                    return _jump;
                }
                else if (_controller.Direction.x == 0f)
                {
                    return _idle;
                }
                else
                {
                    return null;
                }
            }
            else if (_controller.Direction.y > 0f)
            {
                return _fall;
            }
            else if (_controller.Direction.y < 0f)
            {
                return _jump;
            }
            else return null;
        }
    }
}