using _Project.Scripts._GlobalLogic;
using _Project.Scripts.Enums;

namespace _Project.Scripts.Services
{
    public class ResetController
    {
        public void ResetRound()
        {
            foreach (var damagable in GlobalObjects.GameData.allDamagables)
            {
                if (damagable.WarSide == WarSide.Friend)
                {
                    damagable.CurrentHealth = damagable.MaxHealth;
                    damagable.Transform.position = damagable.NoAimPos;
                    damagable.Transform.gameObject.SetActive(true);
                }
            }
        }
    }
}