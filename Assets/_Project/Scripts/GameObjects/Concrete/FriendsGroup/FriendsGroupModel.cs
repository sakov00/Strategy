using System;
using System.Collections.Generic;
using _General.Scripts.Interfaces;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.Unit;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.FriendsGroup
{
    [Serializable]
    [MemoryPackable]
    public partial class FriendsGroupModel : ISavableModel
    {
        [field:SerializeField] public int Id { get; set; }
        [field:SerializeField] public HashSet<int> UnitIds { get; set; } = new ();
        [field:SerializeField] public UnitType UnitType { get; set; }
        [field:SerializeField] public float GroupRadius { get; set; }
        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
        public ISavableModel DeepClone()
        {
            return new FriendsGroupModel
            {
                Id = Id,
                UnitIds = new HashSet<int>(UnitIds),
                UnitType = UnitType,
                GroupRadius = GroupRadius,
                SavePosition = SavePosition,
                SaveRotation = SaveRotation
            };
        }
    }
}