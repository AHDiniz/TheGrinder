using Godot;
using System.Collections;
using System.Collections.Generic;

namespace States
{
    public class StateMachine : Node2D
    {
        private List<IState> _states;
        private Node2D _owner;
        private IState _currentState;

        public override void _Ready()
        {
            _states = new List<IState>();
            _owner = GetParent<Node2D>();

            Godot.Collections.Array children = _owner.GetChildren();

            for (int i = 0; i < children.Count; ++i)
            {
                if (children[i] is IState)
                {
                    if (_states.Count == 0)
                    {
                        _currentState = (IState)children[i];
                        _currentState.Initialize();
                    }
                    _states.Add((IState)children[i]);
                }
            }
        }

        public override void _Process(float delta)
        {
            _currentState.Execute();
            IState next = _currentState.NextState();
            if (next != null && _states.Contains(next))
            {
                _currentState = next;
                _currentState.Initialize();
            }
        }
    }
}