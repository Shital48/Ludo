using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ludoGame
{
    public class GameManager : MonoBehaviour
    {
        public GameObject MainMenu,Game;


        public static GameManager Instance;
         
          public int DiceRollShuffles = 8; 
          public float AIDelayBeforeRollingDice = 1f, 
            AIDelayInChoosingToken = 1f, 
            DiceRollTime = 0.6f,  
            DelayAfterTokenMoveComplete = 1f, 
            DelayBetweenTokenMoves = 0.5f;
  
        private void OnEnable()
        {
            Instance = this;
        } 
        
        public void TwoPlayers()
        {
            GameView();
            MatchManager.Instance.StartMatch(2);
        }
        public void ThreePlayers()
        {
            GameView();
            MatchManager.Instance.StartMatch(3);
        }

        public void FourPlayers()
        {
            GameView();
            MatchManager.Instance.StartMatch(4);
        }

        void GameView()
        {
            MainMenu.SetActive(false);
            Game.SetActive(true);
        }
         

        public byte waypointIndexToSearch = 0;  
    }
}