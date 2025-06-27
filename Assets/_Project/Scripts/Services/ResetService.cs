using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;
using _Project.Scripts.Registries;
using VContainer;

namespace _Project.Scripts.Services
{
    public class ResetService
    {
        [Inject] private HealthRegistry healthRegistry;
        
        public void ResetRound()
        {
            foreach (var healthModel in healthRegistry.GetAll())
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