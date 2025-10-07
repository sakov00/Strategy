using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface ISelectableUnit
    {
        void Select();
        void Deselect();
        void MoveTo(Vector3 position);
    }
}