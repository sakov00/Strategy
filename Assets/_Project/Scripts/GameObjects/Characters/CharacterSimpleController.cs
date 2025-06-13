using System;
using _Project.Scripts.Enums;
using _Project.Scripts.GameObjects.Characters;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts.GameObjects
{
    public class CharacterSimpleController : ObjectController
    {
        protected CharacterModel CharacterModel;
        protected CharacterView CharacterView;

        protected override void Initialize()
        {
            Model = CharacterModel;
            View = CharacterView;
            CharacterView?.SetWalking(true);
            base.Initialize();
        }
    }
}