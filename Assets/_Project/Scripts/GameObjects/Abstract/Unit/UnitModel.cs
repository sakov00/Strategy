using System;
using System.Collections.Generic;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Concrete.ArcherEnemy;
using _Project.Scripts.GameObjects.Concrete.ArcherFriend;
using _Project.Scripts.GameObjects.Concrete.FlyingEnemy;
using _Project.Scripts.GameObjects.Concrete.Player;
using _Project.Scripts.GameObjects.Concrete.WarriorEnemy;
using _Project.Scripts.GameObjects.Concrete.WarriorFriend;
using _Project.Scripts.Interfaces.Model;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Abstract.Unit
{
    [Serializable]
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(PlayerModel))]
    [MemoryPackUnion(1, typeof(WarriorEnemyModel))]
    [MemoryPackUnion(2, typeof(WarriorFriendModel))]
    [MemoryPackUnion(3, typeof(ArcherEnemyModel))]
    [MemoryPackUnion(4, typeof(ArcherFriendModel))]
    [MemoryPackUnion(5, typeof(FlyingEnemyModel))]
    public abstract partial class UnitModel : ObjectModel, IFightObjectModel
    {
        [field: Header("Unit Type")]
        [MemoryPackInclude][field: SerializeField] public UnitType UnitType { get; set; }

        [field: Header("Movement")] 
        [MemoryPackIgnore][field: SerializeField] public float MoveSpeed { get; set; } = 10f;
        [MemoryPackIgnore][field: SerializeField] public float RotationSpeed { get; set; } = 10f;
        [MemoryPackIgnore][field: SerializeField] public float Gravity { get; set; } = -20f;

        [field: Header("Attack")] 
        [MemoryPackIgnore][field: SerializeField] public float AttackRange { get; set; } = 10f;
        [MemoryPackIgnore][field: SerializeField] public int DamageAmount { get; set; } = 10;
        [MemoryPackIgnore][field: SerializeField] public float AllAnimAttackTime { get; set; } = 1f;
        [MemoryPackIgnore][field: SerializeField] public float AnimAttackTime { get; set; } = 1f;
        [MemoryPackIgnore][field: SerializeField] public float DetectionRadius { get; set; } = 20f;
        [MemoryPackIgnore][field: SerializeField] public TypeAttack TypeAttack { get; set; } = TypeAttack.Distance;
        [MemoryPackIgnore][field: SerializeField] public ObjectController AimObject { get; set; }
        
        [field: Header("Way Info")] 
        [MemoryPackInclude][field: SerializeField] public int CurrentWaypointIndex { get; set; }
        [MemoryPackInclude][field: SerializeField] public List<Vector3> WayToAim { get; set; }
        
        public override void LoadFrom(ISavableModel model)
        {
            base.LoadFrom(model);
            if (model is not UnitModel objectModel) return;
            UnitType = objectModel.UnitType;
            CurrentWaypointIndex = objectModel.CurrentWaypointIndex;
            WayToAim = objectModel.WayToAim;
        }
    }
}