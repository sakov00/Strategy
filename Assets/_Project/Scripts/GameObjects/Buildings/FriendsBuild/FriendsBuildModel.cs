using System;
using System.Collections.Generic;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters.Unit;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects.FriendsBuild
{
    [Serializable]
    public class FriendsBuildModel : BuildModel
    {
        [Header("Units")] 
        [SerializeField] public FriendUnitType unitType;
        [SerializeField] public int countUnits;
        [SerializeField] public List<UnitController> buildUnits = new();
    }
}