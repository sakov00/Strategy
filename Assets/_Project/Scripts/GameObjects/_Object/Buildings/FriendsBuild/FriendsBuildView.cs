using System;
using System.Collections.Generic;
using _Project.Scripts.GameObjects.Info;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.FriendsBuild
{
    [Serializable]
    public class FriendsBuildView : BuildView
    {
        [SerializeField] public List<Transform> _buildUnitPositions = new();
        [SerializeField] private UniversalBar _loadBar;
        
        public void UpdateLoadBar(float currentValue, float maxValue) =>
            _loadBar.UpdateBar(currentValue, maxValue);
    }
}