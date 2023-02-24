using Godot;
using System.Collections;
using System.Collections.Generic;

namespace Sensor
{
    public class Sensor : Area2D
    {
        private string _targetGroup;
        private List<Node> _detected;
        
        [Signal] public delegate void OnDetect();

        public string TargetGroup { get => _targetGroup; set => _targetGroup = value; }
        public List<Node> Detected { get => _detected; }

        public override void _Ready()
        {
            Connect("body_entered", this, "OnBodyEntered");
            Connect("body_exited", this, "OnBodyExited");
        }

        public void OnBodyEntered(Node body)
        {
            if (body.IsInGroup(_targetGroup))
            {
                _detected.Add(body);
            }
        }

        public void OnBodyExited(Node body)
        {
            if (body.IsInGroup(_targetGroup))
            {
                _detected.Remove(body);
            }
        }
    }
}