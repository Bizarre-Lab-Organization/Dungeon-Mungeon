using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonMungeon
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private int _transitionDuration;

        public void LoadNextLevel() => StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
        public void LoadLevel(int buildIndex) => StartCoroutine(LoadLevelCoroutine(buildIndex));

        private IEnumerator LoadLevelCoroutine(int levelIndex)
        {
            _animator.SetTrigger("StartFade");

            yield return new WaitForSeconds(_transitionDuration);

            SceneManager.LoadScene(levelIndex);
            _animator.Rebind();
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
