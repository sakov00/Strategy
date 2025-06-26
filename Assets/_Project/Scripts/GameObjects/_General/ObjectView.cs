using System;
using _Project.Scripts._GlobalLogic;
using _Project.Scripts.GameObjects.UI;
using UnityEngine;

namespace _Project.Scripts.GameObjects._General
{
    public abstract class ObjectView : MonoBehaviour
    {
        [SerializeField] protected Renderer objRenderer;
        [SerializeField] protected HealthBar healthBar;
        [SerializeField] protected Tooltip tooltip;
        
        public virtual void Initialize() 
        {
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth) =>
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        
        public void UpdateTooltip(int currentLvl, int costUpgrade) =>
            tooltip.UpdateTooltip(currentLvl, costUpgrade);
        
        public float GetHeightObject()
        {
            return objRenderer.bounds.size.y;
        }
    }
}