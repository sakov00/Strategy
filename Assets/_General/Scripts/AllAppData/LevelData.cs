using System;
using System.Collections.Generic;
using _General.Scripts.DTO;
using _General.Scripts.Interfaces;
using _Project.Scripts.GameObjects.Abstract.BaseObject;
using MemoryPack;
using UniRx;
using UnityEngine;

namespace _General.Scripts.AllAppData
{
    [MemoryPackable]
    public partial class LevelData
    {
        public List<ISavableModel> SavableModels { get; set; } = new();
        public List<ISavableModel> ObjectsForRestoring { get; set; } = new();

        [MemoryPackIgnore] public readonly IntReactiveProperty CurrentRoundReactive;
        [MemoryPackIgnore] public readonly IntReactiveProperty LevelMoneyReactive;
        [MemoryPackIgnore] public readonly BoolReactiveProperty IsFightingReactive;
        
        public int CurrentRound
        {
            get => CurrentRoundReactive.Value;
            set => CurrentRoundReactive.Value = value;
        }

        public int LevelMoney
        {
            get => LevelMoneyReactive.Value;
            set => LevelMoneyReactive.Value = value;
        }

        public bool IsFighting
        {
            get => IsFightingReactive.Value;
            set => IsFightingReactive.Value = value;
        }
        
        [MemoryPackIgnore] public Vector3 MoveDirection { get; set; }

        public LevelData()
        {
            CurrentRoundReactive = new IntReactiveProperty(0);
            LevelMoneyReactive = new IntReactiveProperty(0);
            IsFightingReactive = new BoolReactiveProperty(false);
        }

        public void SetData(LevelData levelData)
        {
            SavableModels = levelData.SavableModels;
            ObjectsForRestoring = levelData.ObjectsForRestoring;
            CurrentRoundReactive.Value = levelData.CurrentRound;
            LevelMoneyReactive.Value = levelData.LevelMoney;
            IsFightingReactive.Value = levelData.IsFighting;
        }
    }
}