using Godot;

namespace States
{
    public interface IState
    {
        void Initialize();
        void Execute();
        IState NextState();
    }
}