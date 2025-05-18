using System;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts._GlobalLogic
{
    public class GameTimer : IInitializable, ITickable
    {
        public static GameTimer Instance { get; private set; }
        
        public event Action OnEverySecond;

        private float time;

        public void Initialize()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;
        }

        public void Tick()
        {
            time += Time.deltaTime;
            if (time >= 1f)
            {
                time -= 1f;
                OnEverySecond?.Invoke();
            }
        }
    }
}