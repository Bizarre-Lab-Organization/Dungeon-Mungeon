using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonMungeon
{
    public static class SceneLoader
    {
        public static void LoadLevel(int sceneBuildIndex) => SceneManager.LoadScene(sceneBuildIndex);
    }
}
