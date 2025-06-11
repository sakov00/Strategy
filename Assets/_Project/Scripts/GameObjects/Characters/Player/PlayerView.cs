using System;
using UnityEngine;

namespace _Project.Scripts.GameObjects.Characters.Player
{
    [Serializable]
    public class PlayerView : CharacterView
    {
        [SerializeField] public CharacterController characterController;
    }
}