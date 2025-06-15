using System.Collections.Generic;
using _Project.Scripts.Interfaces;
using _Project.Scripts.SpawnPoints;
using UniRx;
using UnityEngine;

namespace _Project.Scripts._GlobalLogic
{
    public class GameData : MonoBehaviour
    {
        [SerializeField] public ReactiveCollection<IHealthModel> allDamagables = new();
        [SerializeField] public List<SpawnPoint> spawnPoints;
    }
}