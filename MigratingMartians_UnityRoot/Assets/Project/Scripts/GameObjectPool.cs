﻿using System.Collections.Generic;
using UnityEngine;

namespace KyleConibear
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab = null;
        private List<GameObject> pool = new List<GameObject>();
        // Used for performance efficiency. Instead of using "this.pool.Count"
        private uint poolCount = 0;

        [SerializeField] private Transform spawnPoint;

        [SerializeField] private Transform poolContainer = null;
        [Range(0, 10)]
        [SerializeField] private uint poolStartSize = 0;

        private void Start()
        {
            this.CreateStartingPool();
        }

        public int ActiveObjectsInPool()
        {
            int count = 0;
            for (int i = 0; i < this.poolContainer.childCount; i++)
            {
                count++;
            }

            return count;
        }

        public GameObject GetObject<T>(bool isActive, Vector3 spawnPosition)
        {
            GameObject gameObject = this.GetObject<T>(isActive);
            gameObject.transform.position = spawnPosition;
            return gameObject;
        }

        public GameObject GetObject<T>(bool isActive)
        {
            GameObject returnObject = null;

            for (int i = 0; i < poolCount; i++)
            {
                // Searching pool for inactive object
                if (this.pool[i].activeInHierarchy == false)
                {
                    returnObject = this.pool[i];                    
                }
            }

            // Check if an object was found
            if (returnObject == null)
            {
                returnObject = this.AddObjectToPool(true);
            }

            returnObject.transform.position = this.spawnPoint.position;
            returnObject.transform.rotation = this.spawnPoint.rotation;

            // Activate object
            returnObject.SetActive(isActive);

            return returnObject;
        }

        private GameObject AddObjectToPool(bool isActive)
        {
            // No object was found. Create a new object.
            GameObject returnObject = Instantiate(this.prefab, this.poolContainer);

            returnObject.SetActive(isActive);

            // Add new object to pool
            this.pool.Add(returnObject);

            // Increase poolCount
            this.poolCount++;

            return returnObject;
        }

        private void CreateStartingPool()
        {
            for (int i = 0; i < this.poolStartSize; i++)
            {
                this.AddObjectToPool(false);
            }
        }
    }
}