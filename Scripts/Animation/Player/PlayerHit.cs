using Godot;
using States;
using Movement;
using Combat;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerHit : Node2D, IState
    {
        private Node _owner;

        private PlayerCombat _combat;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;

        private PlayerRun _run;
        private PlayerIdle _idle;
        private PlayerFall _fall;

        private bool _ended;

        public override void _Ready()
        {
            _owner = GetParent();

            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _sprite.Connect("animation_finished", this, "CheckAnimationFinished");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");

            _run = _owner.GetNode<PlayerRun>("PlayerRun");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
            _fall = _owner.GetNode<PlayerFall>("PlayerFall");
        }

        void IState.Initialize()
        {
            _ended = false;
        }

        void IState.Execute()
        {
            _sprite.Play("hit");
        }

        IState IState.NextState()
        {
            if (!_ended)
                return null;
            if (_controller.Grounded)
            {
                if (_controller.Direction.x != 0)
                    return _idle;
                else return _run;
            }
            else return _fall;
        }

        public void CheckAnimationFinished()
        {
            if (_sprite.Animation == "hit")
            {
                _ended = true;
            }
        }
    }
}