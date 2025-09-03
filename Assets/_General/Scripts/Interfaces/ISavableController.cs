using UnityEngine;

namespace _General.Scripts.Interfaces
{
    public interface ISavableController
    {
        public ISavableModel GetSavableModel();
        public void SetSavableModel(ISavableModel savableModel);
    }
}