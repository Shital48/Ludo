using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ludoGame
{
    public class Constants : MonoBehaviour
    { 
        public enum PlayerType { LocalPlayer, Bot };
 
        public static int LastWaypointIndex = 56, diceRollShuffles = 8; 
        public static float diceRollTime = 0.6f, idleTimeAfterDiceRoll = 0.5f;  
        public GameObject DiceTurnHighlight; 
        public Transform[] Waypoints;
        public Transform[] SafeWaypoints; 
        public PlayerToken playerToken;
        public static float delayAfterTokenMoveComplete = 0.5f, delayBetweenTokenMoves = 0.15f;  
        public AudioClip sfxDiceRoll;
        public AudioClip sfxLocalPlayerTurn, sfxTokenHop, sfxTokenKill, sfxOtherPlayerTurn, sfxLocalPlayer6;

        public GameObject[] PlayerTurnHighlighters = new GameObject[4];

        [Header("Player 1")]
        public Color Player1TokenColor = Color.red;
        public Transform[] P1SpecialWaypoints;
        public Transform P1StartWaypoint, P1NormalPathEnd;
        public Transform[] P1Waypoints;
        public GameObject P1TurnHighlight;
        public Transform[] P1Bases = new Transform[4];

        [Header("Player 2")]
        public Color Player2TokenColor = Color.green;
        public Transform[] P2SpecialWaypoints;
        public Transform P2StartWaypoint, P2NormalPathEnd;
        public Transform[] P2Waypoints;
        public GameObject P2TurnHighlight;
        public Transform[] P2Bases = new Transform[4];

        [Header("Player 3")]
        public Color Player3TokenColor = Color.yellow;
        public Transform[] P3SpecialWaypoints;
        public Transform P3StartWaypoint, P3NormalPathEnd;
        public Transform[] P3Waypoints;
        public GameObject P3TurnHighlight;
        public Transform[] P3Bases = new Transform[4];

        [Header("Player 4")]
        public Color Player4TokenColor = Color.blue;
        public Transform[] P4SpecialWaypoints;
        public Transform P4StartWaypoint, P4NormalPathEnd;
        public Transform[] P4Waypoints;
        public GameObject P4TurnHighlight;
        public Transform[] P4Bases = new Transform[4];

        public static Constants Instance; 
        public Transform[] GetBases(byte playerIndex)
        {
            switch (playerIndex)
            {
                case 1:
                    return P1Bases;
                case 2:
                    return P2Bases;
                case 3:
                    return P3Bases;
                case 4:
                    return P4Bases;
                default:
                    return P1Bases;
            }
        }

        
        public Color GetTokenColor(byte playerIndex)
        {
            switch (playerIndex)
            {
                case 1:
                    return Player1TokenColor;
                case 2:
                    return Player2TokenColor;
                case 3:
                    return Player3TokenColor;
                case 4:
                    return Player4TokenColor;
                default:
                    return Player1TokenColor;
            }
        } 
        private void OnEnable()
        {
            Instance = this;
        }

        public Transform GetWaypoint(int playerIndex, int waypointIndex)
        {
            switch (playerIndex)
            {
                case 1:
                    return P1Waypoints[waypointIndex];
                case 2:
                    return P2Waypoints[waypointIndex];
                case 3:
                    return P3Waypoints[waypointIndex];
                case 4:
                    return P4Waypoints[waypointIndex];
                default:
                    return P1Waypoints[waypointIndex];
            }
        }
    }
}