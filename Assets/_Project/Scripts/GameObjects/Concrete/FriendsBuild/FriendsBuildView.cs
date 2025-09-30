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
        [SerializeField] public List<Transform> _buildUnitPositions = new();
        [SerializeField] private UniversalBar _loadBar;

        public override void Initialize()
        {
            
        }

        public void UpdateLoadBar(float currentValue, float maxValue)
        {
            _loadBar.UpdateBar(currentValue, maxValue);
        }
    }
}