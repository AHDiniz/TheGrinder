using Godot;
using States;
using Movement;

namespace Animation.Player
{
    public class PlayerLand : Node2D, IState
    {
        private bool _ended;
        private Node _owner;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;
        private PlayerIdle _idle;
        private PlayerRun _run;

        public override void _Ready()
        {
            _owner = GetParent();
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _sprite.Connect("animation_finished", this, "CheckAnimationFinished");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
            _run = _owner.GetNode<PlayerRun>("PlayerRun");
        }

        void IState.Initialize()
        {
            _ended = false;
        }

        void IState.Execute()
        {
            _sprite.Play("jump_land");
        }

        IState IState.NextState()
        {
            if (_controller.Direction.x == 0f)
                return _ended ? _idle : null;
            else return _run;
        }
        
        public void CheckAnimationFinished()
        {
            if (_sprite.Animation == "jump_land")
            {
                _ended = true;
            }
        }
    }
}