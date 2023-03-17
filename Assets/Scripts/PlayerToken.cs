using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace com.ludoGame
{
    public class PlayerToken : MonoBehaviour
    {
        static int[] safeWaypoints = { 0, 8, 13, 21, 26, 34, 39, 47 };
        
        public int tokenIndex = 1; 
        public int localWaypointIndex = -1; 
        public GameObject InputHighlight; 
        public Transform Base;

        private void OnMouseDown()
        {
            MatchManager.Instance.OnTokenTouchUserInput(this); 
        }

        public void Highlight(bool on)
        {
            InputHighlight.SetActive(on);
        }

        public Player player;
 
        public PlayerToken Initialize(Player player, Transform Base, Color tokenColor)
        {
            this.Base = Base;
            transform.position = Base.position;
            localWaypointIndex = -1;

            this.player = player;

            GetComponent<SpriteRenderer>().color = tokenColor;

            return this;
        }
 
        public void Move(int diceResult)
        {
            StartCoroutine(MoveCoroutine(diceResult, localWaypointIndex));
        }
        public void MoveNonLocalPlayer(int diceResult, int currentWaypointIndex)
        {
            StartCoroutine(MoveCoroutine(diceResult, currentWaypointIndex));
        }

        IEnumerator MoveCoroutine(int diceResult, int currentWaypointIndex)
        { 
            if (!player.IsLocal && localWaypointIndex != currentWaypointIndex)
            { 
                transform.position = Constants.Instance.GetWaypoint(player.playerIndex, currentWaypointIndex).position;
                localWaypointIndex = currentWaypointIndex;
            }

            if (localWaypointIndex == -1 && diceResult == 6)
                diceResult = 1;

            for (int i = 0; i < diceResult; i++)
            {
                yield return new WaitForSeconds(Constants.delayBetweenTokenMoves);
                localWaypointIndex++;
                Sound.PlaySound(Constants.Instance.sfxTokenHop, MatchManager.Instance.audioSource);
                transform.position = Constants.Instance.GetWaypoint(player.playerIndex, localWaypointIndex).position;
            }

            yield return new WaitForSeconds(Constants.delayAfterTokenMoveComplete); 

            if (localWaypointIndex == Constants.LastWaypointIndex)
            { 
                MatchManager.Instance.diceRollsRemaining++;
            } 
            else if (!safeWaypoints.Contains(localWaypointIndex))
            {
                foreach (PlayerToken oneToken in PlayersManager.GetTokensAt(localWaypointIndex, player))
                {
                    // If this token is not our player token
                    if (oneToken.player != player)
                    { 
                        //for getting an Extra turn
                        yield return new WaitForSeconds(Constants.delayBetweenTokenMoves);
                        oneToken.localWaypointIndex = -1;
                       Sound.PlaySound(Constants.Instance.sfxTokenKill);
                        oneToken.transform.position = oneToken.Base.position;
                        yield return new WaitForSeconds(Constants.delayAfterTokenMoveComplete);

                        MatchManager.Instance.diceRollsRemaining++;
                    }
                }
            }

            MatchManager.Instance.NextTurn();
            yield return null;
        }
        public bool CanMove(int stepsToTake)
        {
            if ((Constants.LastWaypointIndex - localWaypointIndex) >= stepsToTake)
            {
                // if player is at base
                if (localWaypointIndex == -1)
                {
                    // if player is at base then it can move only if diceResult is 6
                    if (stepsToTake == 6)
                        return true;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
    }
}