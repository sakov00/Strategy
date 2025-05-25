using _Project.Scripts.Factories;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [RequireComponent(typeof(FriendsBuildModel))]
    public class FriendsBuildSystem : MonoBehaviour
    {
        [Inject] private FriendFactory friendFactory;
        [SerializeField, HideInInspector] private FriendsBuildModel model;

        private void OnValidate()
        {
            model ??= GetComponent<FriendsBuildModel>();
        }

        private void Start()
        {
            for (int i = 0; i < model.countUnits; i++)
            {
                model.buildUnits.Add(friendFactory.CreateFriendUnit(model.unitType, model.buildUnitPositions[i].position));
            }
        }
    }
}