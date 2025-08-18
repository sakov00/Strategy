using _Project.Scripts._GlobalData;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Registries;
using _Project.Scripts.UI.Windows;
using _Project.Scripts.UI.Windows.GameWindow;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace _Project.Scripts.Services
{
    public class ResetService : IInitializable
    {
        [Inject] private ClearDataRegistry _clearDataRegistry;
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private EnemyRoadRegistry _enemyRoadRegistry;
        [Inject] private SaveRegistry _saveRegistry;
        [Inject] private WindowsManager _windowsManager;
        
        public void Initialize()
        {
            _healthRegistry
                .GetAll()
                .ObserveRemove()
                .Subscribe(_ => AllEnemiesDestroyed());
        }
        
        private void NewRound()
        {
            foreach (var healthModel in _healthRegistry.GetAll())
            {
                if (healthModel.WarSide == WarSide.Friend)
                {
                    healthModel.CurrentHealth = healthModel.MaxHealth;
                    healthModel.Transform.position = healthModel.NoAimPos;
                    healthModel.Transform.gameObject.SetActive(true);
                }
            }
            foreach (var spawn in _enemyRoadRegistry.GetAll())
                spawn.RefreshInfoRound();
        }
        
        private void AllEnemiesDestroyed()
        {
            if (_healthRegistry.HasEnemies()) 
                return;

            AppData.LevelData.CurrentRound++;
            NewRound();
        }
        
        public void ResetLevel()
        {
            _clearDataRegistry.Clear();
            _saveRegistry.Clear();
            _enemyRoadRegistry.Clear();
            _healthRegistry.Clear();
            foreach (var spawn in _enemyRoadRegistry.GetAll())
                spawn.RefreshInfoRound();
            _windowsManager.GetWindow<GameWindowView>().Reset();
        }
    }
}