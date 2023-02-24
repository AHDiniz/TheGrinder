using Godot;
using States;
using Movement;
using Combat;
using Combat.Player;

namespace Animation.Player
{
    public class PlayerAttack : Node2D, IState
    {
        private bool _ended;
        private Node _owner;
        private PlayerCombat _combat;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;
        private PlayerIdle _idle;
        private PlayerRun _run;
        private PlayerFall _fall;

        public override void _Ready()
        {
            _owner = GetParent();
            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _sprite.Connect("animation_finished", this, "CheckAnimationFinished");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
            _idle = _owner.GetNode<PlayerIdle>("PlayerIdle");
            _fall = _owner.GetNode<PlayerFall>("PlayerFall");
        }

        void IState.Initialize()
        {
            _ended = false;
            _controller.Direction = new Vector2(_controller.Direction.x, _controller.Direction.y * .25f);
        }

        void IState.Execute()
        {
            _controller.Direction = new Vector2(0f, _controller.Direction.y);
            _sprite.Play("light_attack");
        }

        IState IState.NextState()
        {
            if (!_ended)
            {
                return null;
            }
            else
            {
                _combat.Attacking = false;
                _controller.Direction = new Vector2(_controller.Direction.x, _controller.Direction.y * 4f);
                if (_controller.Grounded)
                {
                    return _idle;
                }
                else
                {
                    return _fall;
                }
            }
        }

        public void CheckAnimationFinished()
        {
            if (_sprite.Animation == "light_attack")
            {
                _ended = true;
            }
        }
    }
}