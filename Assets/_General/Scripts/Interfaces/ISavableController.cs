using UnityEngine;
using VContainer.Unity;

namespace _General.Scripts.Interfaces
{
    public interface ISavableController : IInitializable
    {
        public ISavableModel GetSavableModel();
        public void SetSavableModel(ISavableModel savableModel);
    }
}