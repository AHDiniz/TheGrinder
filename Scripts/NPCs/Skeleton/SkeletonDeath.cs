using Godot;
using States;
using Combat;

namespace NPCs.Skeleton
{
    public class SkeletonDeath : Node2D, IState
    {
        private Node _owner;

        private AnimatedSprite _sprite;
        private KinematicBody2D _body;
        private HealthPoints _hp;

        public override void _Ready()
        {
            _owner = GetParent();

            _body = (KinematicBody2D)_owner;
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _hp = _owner.GetNode<HealthPoints>("HealthPoints");
        }

        void IState.Initialize()
        {

        }

        void IState.Execute()
        {

        }

        IState IState.NextState()
        {
            return null;
        }
    }
}