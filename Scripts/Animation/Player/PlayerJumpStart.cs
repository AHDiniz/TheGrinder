using Godot;
using States;
using Movement;
using Combat;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerJumpStart : Node2D, IState
    {
        private bool _falling;
        
        private Node _owner;
        
        private HealthPoints _hp;
        private PlayerCombat _combat;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;
        
        private PlayerRun _run;
        private PlayerHit _hit;
        private PlayerIdle _idle;
        private PlayerFall _fall;
        private PlayerDash _dash;
        private PlayerDeath _death;
        private PlayerAttack _attack;

        public override void _Ready()
        {
            _owner = GetParent();
            
            _hp = _owner.GetNode<HealthPoints>("HealthPoints");
            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _sprite.Connect("animation_finished", this, "CheckAnimationFinished");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            
            _run = _owner.GetNode<PlayerRun>("PlayerRun");
            _hit = _owner.GetNode<PlayerHit>("PlayerHit");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
            _fall = _owner.GetNode<PlayerFall>("PlayerFall");
            _dash = _owner.GetNode<PlayerDash>("PlayerDash");
            _death = _owner.GetNode<PlayerDeath>("PlayerDeath");
            _attack = _owner.GetNode<PlayerAttack>("PlayerAttack");
        }

        void IState.Initialize()
        {
            _falling = false;
        }

        void IState.Execute()
        {
            _sprite.Play("jump_start");
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
                if (_controller.Direction.x != 0f)
                {
                    return _run;
                }
                else
                {
                    return _idle;
                }
            }
            else
            {
                return _falling ? _fall : null;
            }
        }

        public void CheckAnimationFinished()
        {
            if (_sprite.Animation == "jump_start")
            {
                _falling = true;
            }
        }
    }
}