using System.Collections.Generic;
using _General.Scripts.Interfaces;
using MemoryPack;

namespace _General.Scripts.DTO
{
    [MemoryPackable]
    public partial class LevelModel
    {
        public List<ISavableModel> SavableModels { get; set; } = new();
        public int CurrentRound { get; set; }
        public int LevelMoney { get; set; }
        public bool IsFighting { get; set; }
    }
}