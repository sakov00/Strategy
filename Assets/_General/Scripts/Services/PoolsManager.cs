using System;
using _General.Scripts._VContainer;
using _Project.Scripts.Pools;
using UnityEngine;
using VContainer;

namespace _General.Scripts.Services
{
    public class PoolsManager : MonoBehaviour
    {
        [SerializeField] private Transform _buildPoolContainer;
        [SerializeField] private Transform _characterPoolContainer;
        [SerializeField] private Transform _projectilePoolContainer;
        
        [Inject] private BuildPool _buildPool;
        [Inject] private CharacterPool _characterPool;
        [Inject] private ProjectilePool _projectilePool;

        private void Start()
        {
            InjectManager.Inject(this);
            _buildPool.SetContainer(_buildPoolContainer);
            _characterPool.SetContainer(_characterPoolContainer);
            _projectilePool.SetContainer(_projectilePoolContainer);
        }
    }
}