using System.Linq;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.BuildingZone;
using _Project.Scripts.GameObjects.Characters.Player;
using _Project.Scripts.GameObjects.Environment;
using _Project.Scripts.GameObjects.FriendsBuild;
using _Project.Scripts.GameObjects.MoneyBuild;
using _Project.Scripts.GameObjects.TowerDefence;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _General.Scripts.Services
{
    public class LevelSaveLoadService
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildFactory _buildFactory;
        [Inject] private FriendFactory _friendFactory;
        [Inject] private LevelFactory _levelFactory;
        [Inject] private JsonLoader _jsonLoader;
        [Inject] private ResetLevelService _resetLevelService;

        public async UniTask LoadLevel(int index)
        {
            _resetLevelService.ResetLevel();
            var levelJson = await _jsonLoader.Load<LevelJson>(index);
            if (levelJson != null)
            {
                //TODO add player and unit
                _levelFactory.CreateLevel(index);
                _othersFactory.CreateBuildingZones(levelJson.buildingZoneJsons);
                _buildFactory.CreateMoneyBuildings(levelJson.moneyBuildJsons);
                _buildFactory.CreateMeleeFriendBuildings(levelJson.friendsBuildJsons
                    .Where(x => x.friendsBuildModel.UnitType == UnitType.SimpleMelee));
                _buildFactory.CreateDistanceFriendBuildings(levelJson.friendsBuildJsons
                    .Where(x => x.friendsBuildModel.UnitType == UnitType.SimpleDistance));
                _buildFactory.CreateTowerDefenseBuildings(levelJson.towerDefenceBuildJsons);
                _friendFactory.CreatePlayers(levelJson.playerJsons);
            }
        }
        
        public void SaveLevel(int index)
        {
            var environmentJsons = _objectsRegistry.GetAll<EnvironmentController>().Select(x => x.GetJsonData()).ToList();
            var buildingZoneJsons = _objectsRegistry.GetAll<BuildingZoneController>().Select(x => x.GetJsonData()).ToList();
            var moneyBuildJsons = _objectsRegistry.GetAll<MoneyBuildController>().Select(x => x.GetJsonData()).ToList();
            var friendsBuildJsons = _objectsRegistry.GetAll<FriendsBuildController>().Select(x => x.GetJsonData()).ToList();
            var towerDefenceBuildJsons = _objectsRegistry.GetAll<TowerDefenceController>().Select(x => x.GetJsonData()).ToList();
            var playerJsons = _objectsRegistry.GetAll<PlayerController>().Select(x => x.GetJsonData()).ToList();
            var levelJson = new LevelJson
            {
                environments = environmentJsons,
                buildingZoneJsons = buildingZoneJsons,
                moneyBuildJsons = moneyBuildJsons,
                friendsBuildJsons = friendsBuildJsons,
                towerDefenceBuildJsons = towerDefenceBuildJsons,
                playerJsons = playerJsons,
            };
            _jsonLoader.Save(levelJson, index);
        }
    }
}