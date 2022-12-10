using UnityEngine;

namespace GasStation.Config
{
    [CreateAssetMenu(fileName = "GasStationConfig", menuName = "Configs/GasStationConfig", order = 0)]
    public class GasStationConfig : ScriptableObject
    {
        [SerializeField] private float startUpgradeCost = 5f;
        [SerializeField] private float costMultiplier = 0.06f;
        [SerializeField] private float cost = 5f;
        [SerializeField] private float costFueling = 5f;
        [SerializeField] private float costSpawnDelay = 10f;
        [SerializeField] private float carSpeed = 8f;
        [SerializeField] private float spawnDelay = 5f;
        [SerializeField] private float fuelingTime = 5f;
        [SerializeField] private int level = 1;
        [SerializeField] private int levelFueling = 0;
        [SerializeField] private int levelSpawnDelay = 0;

        private float _decreaseFueling = 0.05f;
        private float _decreaseSpawnDelay = 0.05f;
        public float StartUpgradeCost => startUpgradeCost;
        public float CostMultiplier => costMultiplier;
        public float CarSpeed => carSpeed;
        public float DecreaseFueling => _decreaseFueling;

        public int Level
        {
            get => level;
            set => level = value;
        }

        public float SpawnDelay
        {
            get => spawnDelay;
            set => spawnDelay = value;
        }

        public float FuelingTime
        {
            get => fuelingTime;
            set => fuelingTime = value;
        }

        public float Cost
        {
            get => cost;
            set => cost = value;
        }

        public int LevelFueling
        {
            get => levelFueling;
            set => levelFueling = value;
        }

        public float CostFueling
        {
            get => costFueling;
            set => costFueling = value;
        }

        public int LevelSpawnDelay
        {
            get => levelSpawnDelay;
            set => levelSpawnDelay = value;
        }

        public float CostSpawnDelay
        {
            get => costSpawnDelay;
            set => costSpawnDelay = value;
        }

        public float DecreaseSpawnDelay => _decreaseSpawnDelay;
    }
}