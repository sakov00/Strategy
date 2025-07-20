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
        [Inject] private HealthRegistry _healthRegistry;
        [Inject] private SpawnRegistry _spawnRegistry;
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
        }
        
        private void AllEnemiesDestroyed()
        {
            if (_healthRegistry.HasEnemies()) 
                return;

            NewRound();
            AppData.LevelData.CurrentRound++;
        }
        
        public void ResetLevel()
        {
            _saveRegistry.Clear();
            _spawnRegistry.Clear();
            _healthRegistry.Clear();
            _windowsManager.GetWindow<GameWindowView>().Reset();
        }
    }
}