using Godot;
using Godot.Collections;
using System.Collections;
using System.Collections.Generic;

namespace Combat
{
    public class DamageArea : Area2D
    {
        [Export] private string _targetGroup;
        [Export] private int _damage;
        [Export] private bool _active;

        private Array _targets;
        private CollisionShape2D _colShape;
        private List<HealthPoints> _hpsOnRange;

        public bool Active { get => _active; set => _active = value; }
        public string TargetGroup { get => _targetGroup; set => _targetGroup = value; }
        public int Damage { get => _damage; set => _damage = value; }

        public override void _Ready()
        {
            _hpsOnRange = new List<HealthPoints>();
            _targets = GetTree().GetNodesInGroup(_targetGroup);
            _colShape = GetNode<CollisionShape2D>("CollisionShape2D");

            Connect("body_entered", this, "OnBodyEntered");
            Connect("body_exited", this, "OnBodyExited");
        }

        public override void _Process(float delta)
        {
            if (_active)
            {
                foreach (HealthPoints hp in _hpsOnRange)
                {
                    hp.CurrentHP -= _damage;
                    GD.Print("A");
                }
            }
        }

        public void OnBodyEntered(Node body)
        {
            if (body.IsInGroup(_targetGroup))
            {
                HealthPoints hp = body.FindNode("HealthPoints") as HealthPoints;
                if (hp != null)
                {
                    _hpsOnRange.Add(hp);
                }
            }
        }

        public void OnBodyExited(Node body)
        {
            if (body.IsInGroup(_targetGroup))
            {
                HealthPoints hp = body.FindNode("HealthPoints") as HealthPoints;
                if (hp != null && _hpsOnRange.Contains(hp))
                {
                    _hpsOnRange.Remove(hp);
                }
            }
        }

        public void ResetTargets()
        {
            _targets = GetTree().GetNodesInGroup(_targetGroup);
        }
    }
}