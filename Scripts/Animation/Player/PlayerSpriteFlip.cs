using Godot;
using Movement;

namespace Animation.Player
{
    public class PlayerSpriteFlip : Node2D
    {
        private Node _owner;
        private AnimatedSprite _sprite;
        private PlatformerController _controller;

        public override void _Ready()
        {
            _owner = GetParent();
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _controller = _owner.GetNode<PlatformerController>("PlatformerController");
        }

        public override void _Process(float delta)
        {
            if (_controller.Direction.x > 0)
                _sprite.FlipH = false;
            
            if (_controller.Direction.x < 0)
                _sprite.FlipH = true;
        }
    }
}