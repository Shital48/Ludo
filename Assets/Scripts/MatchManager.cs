using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ludoGame
{
    public class MatchManager : MonoBehaviour
    {  
        public AudioSource audioSource; 
        List<PlayerToken> tokensWeCanMove = new List<PlayerToken>(4);
        public static int DiceResult = 1;
        public Player whoseTurn;
        public int diceRollsRemaining = 0, numSixes = 0; 
        public static MatchManager Instance;
        int NumberOfPlayers;
 
        public void StartMatch(int NoOfPlayer)
        {
            NumberOfPlayers = NoOfPlayer;
            //Keep highlighters off on start
            foreach (GameObject oneTurnHighlighter in Constants.Instance.PlayerTurnHighlighters)
            {
                oneTurnHighlighter.SetActive(false);
            } 
            PlayersManager.Instance.Initialize(NoOfPlayer); 
            NextTurn(); 
        }

        private void OnEnable()
        {
            Instance = this;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
 
        public void InitiatePlayerTurn(Player player)
        {  
                player.turnHighlighter.SetActive(true); 

                    if (player.IsLocal)
                    {
                        Dice.Highlight(true);
                        Sound.PlaySound(Constants.Instance.sfxLocalPlayerTurn);
                        Dice.RollAllowed = true;
                    }
                    else if(player != null)
                    {
                        //  bot
                        Sound.PlaySound(Constants.Instance.sfxOtherPlayerTurn);
                        LudoAI.Instance.PlayTurn();
                    } 
 
        }
    
        public void NextTurn()
        { 
            if (whoseTurn == null)
            { 
                diceRollsRemaining = 1;
                whoseTurn = PlayersManager.Players[0];

                InitiatePlayerTurn(whoseTurn);
                return;
            }

            diceRollsRemaining--;
            if (diceRollsRemaining < 1)
            { 
                numSixes = 0;
                diceRollsRemaining = 1;
                whoseTurn = NextPlayer(); 
                InitiatePlayerTurn(whoseTurn);
            }
            else
            {
                InitiatePlayerTurn(whoseTurn);
            } 
        }

       
        public void OnDiceRolledLocally(int diceResult)
        { 
            Constants.Instance.P1TurnHighlight.SetActive(false);
            Constants.Instance.P2TurnHighlight.SetActive(false);
            Constants.Instance.P3TurnHighlight.SetActive(false);
            Constants.Instance.P4TurnHighlight.SetActive(false);
 
            tokensWeCanMove.Clear(); 
            DiceResult = diceResult;

            if (DiceResult == 6)
            {
                numSixes++;
                //No. of sixes should not 3 or greater
                if (numSixes > 2)
                {
                    numSixes = 0;
                    NextTurn();
                }
            }
 
            foreach (PlayerToken oneToken in whoseTurn.playerTokens)
            {
                if (oneToken.CanMove(diceResult))
                    tokensWeCanMove.Add(oneToken);
            }

            if (tokensWeCanMove.Count == 0)
            {
                NextTurn();
            }
            else
            { 
                if (DiceResult == 6)
                    diceRollsRemaining++;

                if (tokensWeCanMove.Count == 1)
                { 
                    tokensWeCanMove[0].Move(diceResult);
                }
                else
                { 
                    if (whoseTurn.IsLocal)
                        foreach (PlayerToken item in tokensWeCanMove)
                        {
                            item.Highlight(true);
                        }
                    else if (whoseTurn.type == Constants.PlayerType.Bot)
                        LudoAI.Instance.ChooseToken(tokensWeCanMove, diceResult);
                }
            }
        }

        public void OnTokenTouchUserInput(PlayerToken token)
        { 
            if (tokensWeCanMove.Contains(token))
            { 
                foreach (PlayerToken item in tokensWeCanMove)
                {
                    item.Highlight(false);
                }
                token.Move(DiceResult); 
                tokensWeCanMove.Clear();
            }

        }

        public Player NextPlayer()
        {
            return PlayersManager.GetPlayer(NextPlayerIndex(whoseTurn.playerIndex,NumberOfPlayers));
        }
 



        public byte NextPlayerIndex(byte currentIndex,int numPlayers)
        {
            switch (currentIndex)
            {
                case 1:
                    if (numPlayers > 1)
                        return 2;
                    else return 1;
                case 2:
                    if (numPlayers > 2)
                        return 3;
                    else return 1;
                case 3:
                    if (numPlayers > 3)
                        return 4;
                    else return 1;
                case 4:
                    return 1;
                default:
                    if (numPlayers > 1)
                        return 2;
                    else return 1;
            }
        }
         
    }
}