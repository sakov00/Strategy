using _Project.Scripts.GameObjects._General;

namespace _Project.Scripts.Interfaces.Controller
{
    public interface IObjectController
    {
        ObjectModel ModelBase { get; }
        ObjectView ViewBase { get; }
    }
}