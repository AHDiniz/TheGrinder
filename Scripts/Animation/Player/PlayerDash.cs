using Godot;
using States;
using Movement;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerDash : Node2D, IState
    {
        private Node _owner;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;
        private PlayerCombat _combat;
        private PlayerRun _run;
        private PlayerIdle _idle;

        public override void _Ready()
        {
            _owner = GetParent();
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _run = _owner.GetNode<PlayerRun>("PlayerRun");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
        }

        void IState.Initialize()
        {
        }

        void IState.Execute()
        {
            _sprite.Play("dash");
            if (_combat.Attacking)
                _combat.Attacking = false;
        }

        IState IState.NextState()
        {
            if (!_controller.Dashing)
            {
                if (_controller.Direction.x != 0f)
                    return _run;
                else return _idle;
            }
            return null;
        }
    }
}