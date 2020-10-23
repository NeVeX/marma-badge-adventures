using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scenes.Common.Scripts
{
    public enum MarkRelic
    {
        PolaroidCamera, GoldenHeart
    }

    public static class MarkGameState
    {
        private static HashSet<MarkRelic> _relicsCollected = new HashSet<MarkRelic>();
        public static bool IsInDebugMode = true;

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