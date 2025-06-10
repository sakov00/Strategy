using System;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters
{
    [Serializable]
    public class CharacterView
    { 
        public GameObject projectilePrefab;
        public Transform firePoint;
        public float projectileSpeed = 10f;
    }
}