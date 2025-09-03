using _Project.Scripts.GameObjects._Object;
using _Project.Scripts.GameObjects._Object.BuildingZone;
using _Project.Scripts.GameObjects._Object.Characters.Character;
using _Project.Scripts.GameObjects._Object.Characters.Friends.Player;
using _Project.Scripts.GameObjects._Object.Characters.Unit;
using _Project.Scripts.GameObjects._Object.FriendsBuild;
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
    [MemoryPackUnion(2, typeof(MoneyBuildModel))]
    [MemoryPackUnion(3, typeof(FriendsBuildModel))]
    [MemoryPackUnion(4, typeof(TowerDefenceModel))]
    [MemoryPackUnion(5, typeof(CharacterModel))]
    [MemoryPackUnion(6, typeof(PlayerModel))]
    [MemoryPackUnion(7, typeof(UnitModel))]
    [MemoryPackUnion(8, typeof(BuildingZoneModel))]
    [MemoryPackUnion(9, typeof(EnemyRoadModel))]
    [MemoryPackUnion(10, typeof(EnvironmentModel))]
    [MemoryPackUnion(11, typeof(TerrainModel))]
    public partial interface ISavableModel
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}