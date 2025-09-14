using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects._Object.Characters.Unit;

namespace _Project.Scripts.GameObjects._Object.Characters.Enemies.WarriorEnemy
{
    public class WarriorEnemyController : UnitController
    {
        public override ISavableModel GetSavableModel()
        {
            Model.Position = transform.position;
            Model.Rotation = transform.rotation;
            return Model;
        }
        
        public override void SetSavableModel(ISavableModel savableModel)
        {
            if (savableModel is UnitModel unitModel)
            {
                Model = unitModel;
                Initialize();
            }
        }
    }
}