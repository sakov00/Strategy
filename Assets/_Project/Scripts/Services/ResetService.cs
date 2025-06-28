using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Registries;
using _Project.Scripts.Windows.Presenters;
using VContainer;

namespace _Project.Scripts.Services
{
    public class ResetService
    {
        [Inject] private HealthRegistry _healthRegistry;
        
        public void NewRound()
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
    }
}