using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects._Object.BuildingZone;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.GameObjects._Object.FriendsBuild;
using _Project.Scripts.GameObjects._Object.MainBuild;
using _Project.Scripts.GameObjects._Object.MoneyBuild;
using _Project.Scripts.GameObjects._Object.TowerDefence;
using _Project.Scripts.GameObjects.EnemyRoads;
using _Project.Scripts.GameObjects.LevelEnvironment.Environment;
using _Project.Scripts.GameObjects.LevelEnvironment.Terrain;
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
    [MemoryPackUnion(6, typeof(CharacterModel))]
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