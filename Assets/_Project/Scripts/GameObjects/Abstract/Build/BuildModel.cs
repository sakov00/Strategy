using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Concrete.FriendsBuild;
using _Project.Scripts.GameObjects.Concrete.MoneyBuild;
using _Project.Scripts.GameObjects.Concrete.TowerDefence;
using MemoryPack;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Abstract
{
    [Serializable]
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(MoneyBuildModel))]
    [MemoryPackUnion(1, typeof(FriendsBuildModel))]
    [MemoryPackUnion(2, typeof(TowerDefenceModel))]
    public abstract partial class BuildModel : ObjectModel
    {
        [field: Header("Build Data")]
        [field: SerializeField] public BuildType BuildType { get; set; }
        [field: SerializeField] public int BuildPrice { get; set; }
    }
}