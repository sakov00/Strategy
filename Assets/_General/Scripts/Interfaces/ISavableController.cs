using UnityEngine;
using VContainer.Unity;

namespace _General.Scripts.Interfaces
{
    public interface ISavableController : IInitializableAsync
    {
        public ISavableModel GetSavableModel();
        public void SetSavableModel(ISavableModel savableModel);
    }
}