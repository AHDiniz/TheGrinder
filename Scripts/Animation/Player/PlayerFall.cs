using Godot;
using States;
using Movement;
using Combat;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerFall : Node2D, IState
    {
        private Node _owner;
        
        private HealthPoints _hp;
        private PlayerCombat _combat;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;
        
        private PlayerRun _run;
        private PlayerHit _hit;
        private PlayerLand _land;
        private PlayerDash _dash;
        private PlayerDeath _death;
        private PlayerAttack _attack;

        public override void _Ready()
        {
            _owner = GetParent();
            
            _hp = _owner.GetNode<HealthPoints>("HealthPoints");
            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            
            _run = _owner.GetNode<PlayerRun>("PlayerRun");
            _hit = _owner.GetNode<PlayerHit>("PlayerHit");
            _land = _owner.GetNode<PlayerLand>("PlayerLand");
            _dash = _owner.GetNode<PlayerDash>("PlayerDash");
            _death = _owner.GetNode<PlayerDeath>("PlayerDeath");
            _attack = _owner.GetNode<PlayerAttack>("PlayerAttack");
        }

        void IState.Initialize()
        {
        }

        void IState.Execute()
        {
            _sprite.Play("jump_air");
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
                return _land;
            }
            return null;
        }
    }
}