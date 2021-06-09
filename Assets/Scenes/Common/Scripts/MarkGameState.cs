using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scenes.Common.Scripts
{
    public enum MarkRelic
    {
        PolaroidCamera, GoldenHeart, RedTruck
    }

    public static class MarkGameState
    {
        private static HashSet<MarkRelic> _relicsCollected = new HashSet<MarkRelic>();
        public static bool IsInDebugMode = false; // can be flipped on in pause menu (y button)
        public static bool IsShowingTimeTrial = false; 
        public static int PreviousSceneLoaded = 0;

        private static Dictionary<int, bool> startupSceneMessages = new Dictionary<int, bool>();

        public static void AddStartupMessageShownForScene(int sceneIndex)
        {
            if (startupSceneMessages.ContainsKey(sceneIndex))
            {
                startupSceneMessages[sceneIndex] = true;
            } else
            {
                startupSceneMessages.Add(sceneIndex, true);
            }
        }

        public static bool HasShownStartupMessageForScene(int sceneIndex)
        {
            if ( startupSceneMessages.ContainsKey(sceneIndex))
            {
                return startupSceneMessages[sceneIndex];
            } else
            {
                return false;
            }
        }

        public static void AddCollectedRelic(MarkRelic relic)
        {
            _relicsCollected.Add(relic);
            Debug.Log("Collected Relic " + relic +". Are all relics collected? "+AreAllRelicsCollected());
        }

        public static bool AreAllRelicsCollected()
        {
            foreach(MarkRelic relic in Enum.GetValues(typeof(MarkRelic)))
            {
                if ( !_relicsCollected.Contains(relic))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public static class MarkLevels
    {
        public static class ForrestLevel
        {
            public static bool IntroMessageShown = false;
        }
    }

}