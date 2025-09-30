using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
using _Project.Scripts.GameObjects.Additional.LevelEnvironment.Environment;
using _Project.Scripts.GameObjects.Additional.LevelEnvironment.Terrain;
using _Project.Scripts.GameObjects.Concrete.BuildingZone;
using _Project.Scripts.GameObjects.Concrete.FriendsBuild;
using _Project.Scripts.GameObjects.Concrete.MainBuild;
using _Project.Scripts.GameObjects.Concrete.MoneyBuild;
using _Project.Scripts.GameObjects.Concrete.Player;
using _Project.Scripts.GameObjects.Concrete.TowerDefence;
using MemoryPack;
using UnityEngine;

namespace _General.Scripts.Interfaces
{
    [MemoryPackable]
    [MemoryPackUnion(0, typeof(ObjectModel))]
    [MemoryPackUnion(1, typeof(BuildModel))]
    [MemoryPackUnion(2, typeof(MainBuildModel))]
    [MemoryPackUnion(3, typeof(MoneyBuildModel))]
    [MemoryPackUnion(4, typeof(FriendsBuildModel))]
    [MemoryPackUnion(5, typeof(TowerDefenceModel))]
    [MemoryPackUnion(6, typeof(UnitModel))]
    [MemoryPackUnion(7, typeof(PlayerModel))]
    [MemoryPackUnion(8, typeof(UnitModel))]
    [MemoryPackUnion(9, typeof(BuildingZoneModel))]
    [MemoryPackUnion(10, typeof(EnemyRoadModel))]
    [MemoryPackUnion(11, typeof(EnvironmentModel))]
    [MemoryPackUnion(12, typeof(TerrainModel))]
    public partial interface ISavableModel
    {
        public Vector3 SavePosition { get; set; }
        public Quaternion SaveRotation { get; set; }
    }
}