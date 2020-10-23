using System.Collections;
using System.Collections.Generic;
using Characters.Player;
using Scripts.MiniMap;
using UnityEngine;

namespace Scripts.UI
{
    public class GameSceneManager : Singleton<GameSceneManager>
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private GameSceneUI gameSceneUI;
        [SerializeField] private Terrain terrain;
        [SerializeField] private Transform targetTransform;
        [SerializeField, Range(10, 100)] private float randomCircleMinRadius;
        [SerializeField, Range(10, 100)] private float randomCircleMaxRadius;

        public GameSceneUI GameSceneUI => gameSceneUI;
        private void Start()
        {
            GameSceneUI.Init(player.transform, player.MiniMapCamera);
            GameSceneUI.ShowActiveTex(false);
            ChangeObjectPosition();
        }

        public void ChangeObjectPosition()
        {
            // var randomPosition = (Vector3)Random.insideUnitCircle * randomCircleRadius;
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