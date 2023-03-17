using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.ludoGame
{
    public class Player
    {
        public Constants.PlayerType type;
        public byte playerIndex = 1; 
        public PlayerToken[] playerTokens = new PlayerToken[4];
        public GameObject turnHighlighter
        {
            get
            {
                return Constants.Instance.PlayerTurnHighlighters[playerIndex - 1]; 
            }
        }
        public bool IsLocal
        {
            get
            {
                return type == Constants.PlayerType.LocalPlayer;
            }
        }

        /// <summary>
        /// JKL
        /// </summary>
        /// <param name="playerIndex"> GG </param>
        /// <param name="playerType"></param>
        public Player(byte playerIndex, Constants.PlayerType playerType)
        {
            this.playerIndex = playerIndex;
            type = playerType;

            int i = 0;
            foreach (Transform oneBase in Constants.Instance.GetBases(playerIndex))
            {
                playerTokens[i] = GameObject.Instantiate(Constants.Instance.playerToken).GetComponent<PlayerToken>().Initialize(this, oneBase, Constants.Instance.GetTokenColor(playerIndex));
                i++;
            }

        }
    }
}