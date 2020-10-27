using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using Scripts.MapSystem;
using UnityEngine;

namespace Scripts.UI
{
    public class GameSceneManager : Singleton<GameSceneManager>
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private GameSceneUI gameSceneUI;
        [SerializeField] private Terrain terrain;
        [SerializeField, Range(10, 100)] private float randomCircleMinRadius;
        [SerializeField, Range(10, 100)] private float randomCircleMaxRadius;
        [SerializeField, Space] private int maxTargets;
        [SerializeField] private GameObject targetPrefab;

        public GameSceneUI GameSceneUI => gameSceneUI;
        public PlayerController Player => player;

        private void Start()
        {
            GameSceneUI.Init(player.transform, player.MiniMapCamera);
            var rndTargets = Random.Range(0,maxTargets+1);
            for (int i = 0; i < rndTargets; i++)
            {
                var target = Instantiate(targetPrefab);
                target.GetComponent<MarkedObject>().Init();
            }
        }

        public void ChangeObjectPosition(Transform targetTransform)
        {
            Vector3 randomPosition = MathUtilities.RandomPointInAnnulus(player.transform.position, randomCircleMinRadius, randomCircleMaxRadius);
            Debug.Log(randomPosition);
            var minterrainX = terrain.terrainData.bounds.min.x + 50;
            var maxterrainX = terrain.terrainData.bounds.max.x - 50;

            var minterrainZ = terrain.terrainData.bounds.min.z + 50;
            var maxterrainZ = terrain.terrainData.bounds.max.z - 50;

            randomPosition.x = Mathf.Clamp(randomPosition.x, minterrainX, maxterrainX);
            randomPosition.y = 0.5f;
            randomPosition.z = Mathf.Clamp(randomPosition.z, minterrainZ, maxterrainZ);
            Debug.Log(randomPosition);

            targetTransform.position = randomPosition;
        }

    }
}