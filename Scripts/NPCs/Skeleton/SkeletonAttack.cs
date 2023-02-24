using Godot;
using States;
using Combat;

namespace NPCs.Skeleton
{
    public class SkeletonAttack : Node2D, IState
    {
        private Node _owner;

        private AnimatedSprite _sprite;
        private DamageArea _damageAreaLeft;
        private DamageArea _damageAreaRight;

        private SkeletonIdle _idle;

        private bool _ended;

        public override void _Ready()
        {
            _owner = GetParent();

            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");
            _sprite.Connect("animation_finished", this, "CheckAnimationFinished");

            _idle = _owner.GetNode<SkeletonIdle>("SkeletonIdle");
        }

        void IState.Initialize()
        {
            _ended = false;
        }

        void IState.Execute()
        {
            _sprite.Play("attack");
        }

        IState IState.NextState()
        {
            
            return null;
        }

        public void CheckAnimationEnded()
        {
            if (_sprite.Animation == "attack")
            {
                _ended = true;
            }
        }
    }
}