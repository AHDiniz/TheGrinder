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

        private bool _ended;

        public override void _Ready()
        {
            _owner = GetParent();

            _combat = _owner.GetNode<PlayerCombat>("PlayerCombat");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
        }

        void IState.Initialize()
        {
            _ended = false;
        }

        void IState.Execute()
        {
            _sprite.Play("death");
        }

        IState IState.NextState()
        {
            return null;
        }
    }
}