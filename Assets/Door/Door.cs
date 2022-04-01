using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonMungeon
{
    [RequireComponent(typeof(Collider2D))]
    public class Door : MonoBehaviour
    {
        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (_levelManager) _levelManager.LoadNextLevel();
            }
        }
    }
}
