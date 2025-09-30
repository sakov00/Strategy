using System;
using System.Collections.Generic;
using _General.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.Abstract.Unit
{
    public abstract class UnitController : ObjectController
    {
        [Inject] protected UnitPool UnitPool;

        protected abstract UnitModel UnitModel { get; }
        protected abstract UnitView UnitView { get; }
        protected override ObjectModel ObjectModel => UnitModel;
        protected override ObjectView ObjectView => UnitView;


        public Action<UnitController> OnKilled;
        public UnitType UnitType => UnitModel.UnitType;

        public override void Initialize()
        {
            InjectManager.Inject(this);
            base.Initialize();
            ObjectsRegistry.Register(this);
        }

        public void SetWayToPoint(List<Vector3> waypoints)
        {
            UnitModel.WayToAim = waypoints;
        }

        public override void DeleteFromScene(bool realDelete = false)
        {
            if(realDelete)
                ObjectsRegistry.Unregister(this);
            else
            {
                UnitPool.Return(this);
                OnKilled?.Invoke(this);
                OnKilled = null;
            }
        }
    }
}