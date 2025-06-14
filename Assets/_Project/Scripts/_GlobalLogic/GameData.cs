using System.Collections.Generic;
using _Project.Scripts.Data;
using _Project.Scripts.GameObjects._General;
using _Project.Scripts.Interfaces;
using _Project.Scripts.SpawnPoints;
using _Project.Scripts.Windows;
using LunarConsolePlugin;
using UniRx;
using UnityEngine;

namespace _Project.Scripts._GlobalLogic
{
    public class GameData : MonoBehaviour
    {
        [SerializeField] public ReactiveCollection<IHealthModel> allDamagables = new();
        [SerializeField] public LevelData levelData = new();
        
        [SerializeField] public List<SpawnPoint> spawnPoints;
        
        public GameWindow gameWindow;
    }
}