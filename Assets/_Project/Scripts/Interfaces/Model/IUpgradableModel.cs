using System.Collections.Generic;

namespace _Project.Scripts.Interfaces
{
    public interface IUpgradableModel
    {
        public List<int> PriceList { get; set; }
        public int CurrentLevel { get; set; }
    }
}