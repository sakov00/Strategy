using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Scripts._VContainer
{
    public class InjectManager
    {
        private static IObjectResolver container;
        
        public static void Initialize(IObjectResolver containerParam)
        {
            container = containerParam;
        }

        public static void Inject(object target)
        {
            if (container == null)
            {
                Debug.LogError("InjectManager is not initialized.");
                return;
            }

            container.Inject(target);
        }

        public static void InjectGameObject(GameObject gameObject)
        {
            if (container == null)
            {
                Debug.LogError("InjectManager is not initialized.");
                return;
            }

            container.InjectGameObject(gameObject);
        }
    }
}