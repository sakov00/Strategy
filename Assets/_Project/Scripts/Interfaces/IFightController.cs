using _Project.Scripts.Interfaces.Model;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.Interfaces
{
    public interface IFightController
    {
        public float HeightObject { get; set; }
        IFightModel FightModel { get; }
        IFightView FightView { get; }
        void Killed();
    }
}