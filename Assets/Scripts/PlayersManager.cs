using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ludoGame
{ 
    public class PlayersManager : MonoBehaviour
    {
        public static PlayersManager Instance;
        public static List<Player> Players = new List<Player>();

        private void OnEnable()
        {
            Instance = this;
        }
 
        public void Initialize(int numberOfPlayers)
        {
            Players.Clear(); 
            Players.Add(new Player(1, Constants.PlayerType.LocalPlayer)); 

            Constants.PlayerType restPlayerTypes;
            restPlayerTypes = Constants.PlayerType.Bot;

            if(numberOfPlayers==2)
            Players.Add(new Player(2, restPlayerTypes));
            if (numberOfPlayers == 3)
            {
                Players.Add(new Player(2, restPlayerTypes));
                Players.Add(new Player(3, restPlayerTypes));
            }
            if (numberOfPlayers == 4)
            {
                Players.Add(new Player(2, restPlayerTypes));
                Players.Add(new Player(3, restPlayerTypes));
                Players.Add(new Player(4, restPlayerTypes));
            } 
        }

        public static Player GetPlayer(byte playerIndex)
        {
            foreach (Player onePlayer in Players)
            {
                if (onePlayer.playerIndex == playerIndex)
                    return onePlayer;
            }
            return null;
        }

        static List<PlayerToken> tokensAt = new List<PlayerToken>();
        
        public static List<PlayerToken> GetTokensAt(int givenWaypointIndex, Player withRespectTo)
        {
            tokensAt.Clear();
            int waypointDifference;
            foreach (Player onePlayer in Players)
            {
                waypointDifference = withRespectTo.playerIndex - onePlayer.playerIndex;
                if (waypointDifference < 0)
                    waypointDifference = 4 + waypointDifference;
                waypointDifference = waypointDifference * 13 + givenWaypointIndex;
                if (waypointDifference > 51)
                    waypointDifference = waypointDifference - 52;
                foreach (PlayerToken oneToken in onePlayer.playerTokens)
                {
                    if (oneToken.localWaypointIndex == waypointDifference)
                        tokensAt.Add(oneToken);
                }
            }
            return tokensAt;
        }
    }

  
}