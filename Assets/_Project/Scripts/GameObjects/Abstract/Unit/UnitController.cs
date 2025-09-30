using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Pools;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract.Unit
{
    public abstract class UnitController : ObjectController
    {
        [Inject] protected CharacterPool CharacterPool;

        public Action<UnitController> OnKilled;
    }
}