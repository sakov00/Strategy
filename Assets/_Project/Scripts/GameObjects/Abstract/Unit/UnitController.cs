using System;
using System.Collections.Generic;
using _General.Scripts._VContainer;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.Interfaces;
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
            ObjectsRegistry.Register(this);
            IdsRegistry.Register(this);
            Dispose(false,false);
        }

        public void SetWayToPoint(List<Vector3> waypoints)
        {
            UnitModel.WayToAim = waypoints;
        }
        
        public void Select()
        {
            UnitView.EnableOutline(true);
        }

        public void Deselect()
        {
            UnitView.EnableOutline(false);
        }

        public void MoveTo(Vector3 position)
        {
            UnitView.Agent.enabled = false;
            transform.position = position;
            UnitView.Agent.enabled = true;
        }

        public override void Dispose(bool returnToPool = true, bool clearFromRegistry = true)
        {
            if (returnToPool)
            {
                UnitPool.Return(this);
                OnKilled?.Invoke(this);
                OnKilled = null;
            }
            if (clearFromRegistry)
            {
                IdsRegistry.Unregister(this);
                ObjectsRegistry.Unregister(this);
            }
        }
    }
}