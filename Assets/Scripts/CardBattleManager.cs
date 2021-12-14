using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBattleManager : MonoBehaviour {

    public enum phases {Begin, PlayerPlay, EnemyPlay, Battle, Victory, GameOver};
    public phases gamePhase;

    [SerializeField] private GameObject playerHand;
    [SerializeField] private GameObject enemyHand;
    [SerializeField] private Image playerPlayzone;
    [SerializeField] private Image enemyPlayzone;

    private List<Sprite> playerCards = new List<Sprite>();
    private List<Sprite> enemyCards = new List<Sprite>();

    [SerializeField] private Sprite[] cardSprites;
    // 0 = Slave, 1 = Merchant, 2 = Pharaoh, 3 = Card Back

    public Image[] cardObjects;

    void Start() {
        for (int i = 0; i < 4; i++) {
            playerCards.Add(cardSprites[1]);
            enemyCards.Add(cardSprites[1]);
        }
        int ranPharaoh = Random.Range(0, 2);
        if (ranPharaoh == 0) {
            playerCards.Add(cardSprites[2]);
            enemyCards.Add(cardSprites[0]);
        } else {
            playerCards.Add(cardSprites[0]);
            enemyCards.Add(cardSprites[2]);
        }
    }

    void Update() {
        if (gamePhase == phases.Begin) {
            ShuffleHand();
            cardObjects[0].sprite = playerCards[0];
            cardObjects[1].sprite = playerCards[1];
            cardObjects[2].sprite = playerCards[2];
            cardObjects[3].sprite = playerCards[3];
            cardObjects[4].sprite = playerCards[4];
            gamePhase = phases.EnemyPlay;
        }

        if (gamePhase == phases.EnemyPlay) {
            int ranCard = Random.Range(0, enemyCards.Count);
            enemyPlayzone.sprite = enemyCards[ranCard];
            enemyCards.Remove(enemyCards[ranCard]);
            EnemyPlaysCard();
            gamePhase = phases.Battle;
        }
    }

    void ShuffleHand() {
         for (int i = 0; i < playerCards.Count; i++) {
            Sprite temp = playerCards[i];
            int randomIndex = Random.Range(i, playerCards.Count);
            playerCards[i] = playerCards[randomIndex];
            playerCards[randomIndex] = temp;
        }
    }

    void EnemyPlaysCard() {
        int ranCard = Random.Range(0, enemyHand.transform.childCount);
        Transform cardToDiscard = enemyHand.transform.GetChild(ranCard);
        cardToDiscard.gameObject.SetActive(false);
    }

}
