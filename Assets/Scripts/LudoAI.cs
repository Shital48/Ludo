using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ludoGame
{ 
    public class LudoAI : MonoBehaviour
    {
        public static LudoAI Instance; 
        private void OnEnable()
        {
            Instance = this;
        }

        public static float delayBeforeRollingDice = 0.5f;
        public static float delayInChoosingToken = 0.5f; 

        public void PlayTurn()
        {
            StartCoroutine(PlayAITurn());
        }

        IEnumerator PlayAITurn()
        { 
            yield return new WaitForSeconds(delayBeforeRollingDice + Random.Range(-0.5f, 0.5f));
             
            Dice.Instance.BotDiceRoll(); 
            yield return null;
        }

        public void ChooseToken(List<PlayerToken> tokensWeCanMove, int diceResult)
        {
            StartCoroutine(AIChooseToken(tokensWeCanMove, diceResult));
        }

        IEnumerator AIChooseToken(List<PlayerToken> tokensWeCanMove, int diceResult)
        { 
            yield return new WaitForSeconds(delayInChoosingToken + Random.Range(-0.5f, 0.5f)); 
            tokensWeCanMove[Random.Range(0, tokensWeCanMove.Count)].Move(diceResult); 
            yield return null;
        }
         
    }
}