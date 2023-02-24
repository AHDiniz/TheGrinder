using Godot;
using States;
using Combat;
using Movement;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerBlock : Node2D, IState
    {
        private Node _owner;

        private HealthPoints _hp;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;

        private PlayerRun _run;
        private PlayerIdle _idle;
        private PlayerFall _fall;

        public override void _Ready()
        {
            _owner = GetParent();

            _hp = _owner.GetNode<HealthPoints>("HealthPoints");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            
            _run = _owner.GetNode<PlayerRun>("PlayerRun");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
            _fall = _owner.GetNode<PlayerFall>("PlayerFall");
        }

        void IState.Initialize()
        {
            _controller.Direction = new Vector2(0f, _controller.Direction.y);
            _hp.CanGetHurt = false;
        }

        void IState.Execute()
        {
            _sprite.Play("block");
        }

        IState IState.NextState()
        {
            if (Input.IsActionPressed("block"))
            {
                return null;
            }
            
            if (Input.IsActionJustReleased("block"))
            {
                _hp.CanGetHurt = true;

                if (_controller.Grounded)
                {
                    if (_controller.Direction.x == 0f)
                        return _idle;
                    else return _run;
                }
                else return _fall;
            }

            return null;
        }
    }
}