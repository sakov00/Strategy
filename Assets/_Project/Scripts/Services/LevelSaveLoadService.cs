using System.Linq;
using _General.Scripts.Json;
using _General.Scripts.Registries;
using _General.Scripts.Services;
using _Project.Scripts.Enums;
using _Project.Scripts.Factories;
using _Project.Scripts.Interfaces;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Scripts.Services
{
    public class LevelSaveLoadService
    {
        [Inject] private ObjectsRegistry _objectsRegistry;
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildFactory _buildFactory;
        [Inject] private CharacterFactory _characterFactory;
        [Inject] private LevelFactory _levelFactory;
        [Inject] private JsonLoader _jsonLoader;
        [Inject] private ResetLevelService _resetLevelService;

        public async UniTask LoadLevel(int index)
        {
            _resetLevelService.ResetLevel();

            var levelJson = await _jsonLoader.Load<LevelJson>(index);
            if (levelJson == null || levelJson.objects == null) return;

            _levelFactory.CreateLevel(index);

            foreach (var objJson in levelJson.objects)
            {
                IJsonSerializable controller = null;

                // Создаём объект в зависимости от objectType
                switch (objJson.objectType)
                {
                    case "PlayerController":
                        controller = _characterFactory.CreateCharacter(CharacterType.Player, objJson.position, objJson.rotation);
                        break;

                    case "FriendsBuildController":
                        controller = _buildFactory.CreateBuild(BuildType.FriendMeleeBuilding, objJson.position, objJson.rotation);
                        break;

                    case "MoneyBuildController":
                        controller = _buildFactory.CreateBuild(BuildType.MoneyBuilding, objJson.position, objJson.rotation);
                        break;

                    case "TowerDefenceController":
                        controller = _buildFactory.CreateBuild(BuildType.TowerDefenseBuilding, objJson.position, objJson.rotation);
                        break;

                    case "EnvironmentController":
                        controller = _levelFactory.CreateEnvironment(BuildType.FriendMeleeBuilding, objJson.position, objJson.rotation);
                        break;

                    case "BuildingZoneController":
                        controller = _othersFactory.CreateBuildingZone(objJson.position, objJson.rotation);
                        break;

                    // добавляй новые типы по мере необходимости
                    default:
                        UnityEngine.Debug.LogWarning($"Неизвестный тип объекта: {objJson.objectType}");
                        break;
                }

                if (controller != null)
                {
                    controller.SetJsonData(objJson);
                }
            }
        }


        public async UniTask SaveLevel(int index)
        {
            var allObjects = _objectsRegistry.GetAllByInterface<IJsonSerializable>()
                 .Select(c => c.GetJsonData())
                 .ToList();

            var levelJson = new LevelJson
            {
                objects = allObjects
            };

           await _jsonLoader.Save(levelJson, index);
        }
    }
}
