using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [Serializable]
    public class FriendsBuildView : BuildView
    {
        [SerializeField] public List<Transform> buildUnitPositions = new();
    }
}