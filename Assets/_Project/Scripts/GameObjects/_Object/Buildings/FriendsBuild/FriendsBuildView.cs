using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameObjects._Object.FriendsBuild
{
    [Serializable]
    public class FriendsBuildView : BuildView
    {
        [SerializeField] public List<Transform> _buildUnitPositions = new();
    }
}