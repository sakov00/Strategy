using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IPositionedModel
    {
        Transform Transform { get; set; }
        float HeightObject { get; set; }
        Vector3 NoAimPos { get; set; }
    }
}