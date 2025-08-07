using System.Linq;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Json;
using _Project.Scripts.Registries;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts.Services
{
    public class LevelController
    {
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private SaveRegistry _saveRegistry;
        
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildFactory _buildFactory;
        [Inject] private FriendFactory _friendFactory;
        [Inject] private LevelFactory _levelFactory;
        [Inject] private JsonLoader _jsonLoader;

        public void LoadLevel(int index)
        {
            var levelJson = _jsonLoader.Load<LevelJson>(index);
            if (levelJson != null)
            {
                //TODO add player and unit
                _levelFactory.CreateLevel(index);
                _othersFactory.CreateBuildingZones(levelJson.buildingZoneJsons);
                _buildFactory.CreateMoneyBuildings(levelJson.moneyBuildJsons);
                _buildFactory.CreateMeleeFriendBuildings(levelJson.friendsBuildJsons
                    .Where(x => x.friendsBuildModel.unitType == UnitType.SimpleMelee));
                _buildFactory.CreateDistanceFriendBuildings(levelJson.friendsBuildJsons
                    .Where(x => x.friendsBuildModel.unitType == UnitType.SimpleDistance));
                _buildFactory.CreateTowerDefenseBuildings(levelJson.towerDefenceBuildJsons);
                _friendFactory.CreatePlayers(levelJson.playerJsons);
            }
        }
        
        public void SaveLevel(int index)
        {
            var environmentJsons = _saveRegistry.GetAll<EnvironmentJson>().Select(x => x.GetJsonData()).ToList();
            var buildingZoneJsons = _saveRegistry.GetAll<BuildingZoneJson>().Select(x => x.GetJsonData()).ToList();
            var moneyBuildJsons = _saveRegistry.GetAll<MoneyBuildJson>().Select(x => x.GetJsonData()).ToList();
            var friendsBuildJsons = _saveRegistry.GetAll<FriendsBuildJson>().Select(x => x.GetJsonData()).ToList();
            var towerDefenceBuildJsons = _saveRegistry.GetAll<TowerDefenceBuildJson>().Select(x => x.GetJsonData()).ToList();
            var playerJsons = _saveRegistry.GetAll<PlayerJson>().Select(x => x.GetJsonData()).ToList();
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