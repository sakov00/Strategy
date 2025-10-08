using System;
using System.Collections.Generic;
using _General.Scripts.UI.Info;
using _Project.Scripts.GameObjects.Abstract;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Concrete.FriendsBuild
{
    [Serializable]
    public class FriendsBuildView : BuildView
    {
        [SerializeField] private UniversalBar _loadBar;
        [field:SerializeField] public Transform SpawnPoint { get; set; }
        [field:SerializeField] public Transform GroupPoint { get; set; }

        public override void Initialize()
        {
            _loadBar.UpdateBar(1, 1);
        }

        public void UpdateLoadBar(float currentValue, float maxValue)
        {
            _loadBar.UpdateBar(currentValue, maxValue);
        }
    }
}