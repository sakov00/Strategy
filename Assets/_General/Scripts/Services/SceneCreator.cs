using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _Project.Scripts.Factories;
using _Project.Scripts.GameObjects.Abstract;
using _Project.Scripts.GameObjects.Abstract.Unit;
using _Project.Scripts.GameObjects.Additional.EnemyRoads;
using _Project.Scripts.GameObjects.Additional.LevelEnvironment.Terrain;
using _Project.Scripts.GameObjects.Concrete.BuildingZone;
using _Project.Scripts.Pools;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _General.Scripts.Services
{
    public class SceneCreator 
    {
        [Inject] private OthersFactory _othersFactory;
        [Inject] private BuildPool _buildPool;
        [Inject] private UnitPool _unitPool;
        [Inject] private EnvironmentFactory _environmentFactory;
        
        public async UniTask InstantiateLoadedObjects(LevelModel levelModel)
        {
            foreach (var model in levelModel.SavableModels)
            {
                ISavableController savableController = model switch
                {
                    BuildModel buildModel => 
                        _buildPool.Get(buildModel.BuildType, buildModel.SavePosition, buildModel.SaveRotation),
                    UnitModel characterModel => 
                        _unitPool.Get(characterModel.UnitType, characterModel.SavePosition, characterModel.SaveRotation),
                    TerrainModel terrainModel => 
                        _environmentFactory.CreateTerrain(0),
                    EnemyRoadModel enemyRoadModel => 
                        _environmentFactory.CreateRoads(enemyRoadModel.SavePosition, enemyRoadModel.SaveRotation),
                    BuildingZoneModel buildingZoneModel => 
                        _othersFactory.CreateBuildingZone(buildingZoneModel.SavePosition, buildingZoneModel.SaveRotation),
                    _ => null
                };

                savableController?.SetSavableModel(model);

                await UniTask.Yield();
            }
        }
    }
}