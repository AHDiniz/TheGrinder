using Godot;
using States;
using Movement;

namespace NPCs.Skeleton
{
    public class SkeletonIdle : Node2D, IState
    {
        [Export] private float _attackRange;

        private Node _owner;

        private Sensor2D _sensor;
        private HealthPoints _hp;
        private AnimatedSprite _sprite;
        private KinematicBody2D _body;

        private SkeletonHit _hit;
        private SkeletonWalk _walk;
        private SkeletonAttack _attack;

        private bool _detected;
        private float _distance;

        public override void _Ready()
        {
            _owner = GetParent();

            _body = (KinematicBody2D)_owner;
            _sensor = _owner.GetNode<Sensor2D>("Sensor2D");
            _sensor.TargetGroup = "player";
            _hp = _owner.GetNode<HealthPoints>("HealthPoints");
            _sprite = _owner.GetNode<AnimatedSprite>("AnimatedSprite");

            _hit = _owner.GetNode<SkeletonHit>("SkeletonHit");
            _walk = _owner.GetNode<SkeletonWalk>("SkeletonWalk");
            _attack = _owner.GetNode<SkeletonAttack>("SkeletonAttack");
        }

        void IState.Initialize()
        {

        }

        void IState.Execute()
        {
            _sprite.Play("idle");
            _detected = _sensor.Detected.Count != 0;

            _distance = GetPosition().Distance(_sensor.Detected[0].GetPosition());
        }

        IState IState.NextState()
        {
            if (_detected)
            {
                if (_hp.CurrentHP < _hp.PrevHP)
                    return _hp;
                if (_distance > _attackRange)
                    return _walk;
                else return _attack;
            }
            return null;
        }
    }
}