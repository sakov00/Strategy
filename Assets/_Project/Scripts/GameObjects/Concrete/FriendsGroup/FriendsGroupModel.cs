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
        [MemoryPackInclude][field:SerializeField] public int Id { get; set; }
        [MemoryPackInclude][field:SerializeField] public HashSet<int> UnitIds { get; set; } = new ();
        [MemoryPackInclude][field:SerializeField] public UnitType UnitType { get; set; }
        [MemoryPackIgnore][field:SerializeField] public float GroupRadius { get; set; }
        [MemoryPackInclude]public Vector3 SavePosition { get; set; }
        [MemoryPackInclude]public Quaternion SaveRotation { get; set; }
        
        public virtual void LoadFrom(ISavableModel model)
        {
            if (model is FriendsGroupModel objectModel)
            {
                Id = objectModel.Id;
                UnitIds = objectModel.UnitIds;
                UnitType = objectModel.UnitType;
                SavePosition = objectModel.SavePosition;
                SaveRotation = objectModel.SaveRotation;
            }
        }
        
        public ISavableModel GetSaveData()
        {
            return new FriendsGroupModel
            {
                Id = Id,
                UnitIds = new HashSet<int>(UnitIds),
                UnitType = UnitType,
                SavePosition = SavePosition,
                SaveRotation = SaveRotation
            };
        }
    }
}