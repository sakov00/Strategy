using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects.Info;
using _Project.Scripts.Interfaces;
using _Project.Scripts.Interfaces.View;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    public abstract class ObjectView : MonoBehaviour, IHealthView
    {
        [SerializeField] protected Renderer _objRenderer;
        [SerializeField] protected HealthBar _healthBar;
        [SerializeField] protected Tooltip _tooltip;
        
        public virtual void Initialize() 
        {
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth) =>
            _healthBar.UpdateHealthBar(currentHealth, maxHealth);
        
        public void UpdateTooltip(int currentLvl, int costUpgrade) =>
            _tooltip.UpdateTooltip(currentLvl, costUpgrade);
        
        public float GetHeightObject()
        {
            return _objRenderer.bounds.size.y;
        }
    }
}