using System;
using UnityEngine;
using VContainer.Unity;

namespace _General.Scripts._GlobalLogic
{
    public class GameTimer : IInitializable, ITickable
    {
        public static GameTimer I { get; private set; }
        
        public event Action OnEverySecond;

        private float time;

        public void Initialize()
        {
            if (I != null)
            {
                return;
            }

            I = this;
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