using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardBattleManager : MonoBehaviour {

    private enum results {draw, lose, win};
    private enum players {player, enemy}
    private results matchResult;
    private players pharaohHolder;

    public GameObject playerHand, playzone;
    [SerializeField] private GameObject enemyHand;

    [HideInInspector] public List<GameObject> playerCards = new List<GameObject>();
    private List<GameObject> enemyCards = new List<GameObject>();
    [SerializeField] private Image breakAnimObj;

    public GameObject[] cards;
    // 0 = Slave, 1 = Merchant, 2 = Pharaoh

    [HideInInspector] public int cardPlayerPlayed, cardEnemyPlayed;
    private int round, playerPoints, enemyPoints;
    // Match Result: 0 = Draw, 1 = Lose, 2 = Victory
    [HideInInspector] public bool playerTurn;
    private bool nextMatch;

    [SerializeField] private TextMeshProUGUI enemyPointsText, playerPointsText;

    void Awake() {
        Vector3 cardPos = new Vector3(0, 0, 0);
        for (int i = 0; i < 4; i++) {
            GameObject tempPC = Instantiate(cards[1], cardPos, Quaternion.identity);
            tempPC.transform.GetChild(1).gameObject.SetActive(false);
            tempPC.transform.SetParent(playerHand.transform);
            playerCards.Add(tempPC);
            GameObject tempEC = Instantiate(cards[1], cardPos, Quaternion.identity);
            tempEC.transform.SetParent(enemyHand.transform);
            enemyCards.Add(tempEC);
        }
        int ranPharaoh = Random.Range(0, 2);
        if (ranPharaoh == 0) {
            GameObject tempPC = Instantiate(cards[2], cardPos, Quaternion.identity);
            tempPC.transform.GetChild(1).gameObject.SetActive(false);
            tempPC.transform.SetParent(playerHand.transform);
            playerCards.Add(tempPC);
            GameObject tempEC = Instantiate(cards[0], cardPos, Quaternion.identity);
            tempEC.transform.SetParent(enemyHand.transform);
            enemyCards.Add(tempEC);
            pharaohHolder = players.player;
        } else {
            GameObject tempPC = Instantiate(cards[0], cardPos, Quaternion.identity);
            tempPC.transform.GetChild(1).gameObject.SetActive(false);
            tempPC.transform.SetParent(playerHand.transform);
            playerCards.Add(tempPC);
            GameObject tempEC = Instantiate(cards[2], cardPos, Quaternion.identity);
            tempEC.transform.SetParent(enemyHand.transform);
            enemyCards.Add(tempEC);
            pharaohHolder = players.enemy;
        }

        StartPhase();
    }

    void StartPhase() {
        cardPlayerPlayed = 0;
        cardEnemyPlayed = 0;

        if (nextMatch) {
            playerCards.Clear();
            enemyCards.Clear();
            foreach (Transform child in playerHand.transform) {
                    GameObject.Destroy(child.gameObject);
            }
            foreach (Transform child in enemyHand.transform) {
                    GameObject.Destroy(child.gameObject);
            }
            Vector3 cardPos = new Vector3(0, 0, 0);
            for (int i = 0; i < 4; i++) {
                GameObject tempPC = Instantiate(cards[1], cardPos, Quaternion.identity);
                tempPC.transform.GetChild(1).gameObject.SetActive(false);
                tempPC.transform.SetParent(playerHand.transform);
                playerCards.Add(tempPC);
                GameObject tempEC = Instantiate(cards[1], cardPos, Quaternion.identity);
                tempEC.transform.SetParent(enemyHand.transform);
                enemyCards.Add(tempEC);
            }
            if (pharaohHolder == players.enemy) {
                GameObject tempPC = Instantiate(cards[2], cardPos, Quaternion.identity);
                tempPC.transform.GetChild(1).gameObject.SetActive(false);
                tempPC.transform.SetParent(playerHand.transform);
                playerCards.Add(tempPC);
                GameObject tempEC = Instantiate(cards[0], cardPos, Quaternion.identity);
                tempEC.transform.SetParent(enemyHand.transform);
                enemyCards.Add(tempEC);
                pharaohHolder = players.player;
            } else if (pharaohHolder == players.player) {
                GameObject tempPC = Instantiate(cards[0], cardPos, Quaternion.identity);
                tempPC.transform.GetChild(1).gameObject.SetActive(false);
                tempPC.transform.SetParent(playerHand.transform);
                playerCards.Add(tempPC);
                GameObject tempEC = Instantiate(cards[2], cardPos, Quaternion.identity);
                tempEC.transform.SetParent(enemyHand.transform);
                enemyCards.Add(tempEC);
                pharaohHolder = players.enemy;
            }
        }

        ShuffleHand();
        PlayerPlay();
    }

    void PlayerPlay() {
        playerTurn = true;
    }

    public void EnemyPlay() {
        int ranCard = Random.Range(0, enemyCards.Count - 1);
        enemyCards[ranCard].transform.SetParent(playzone.transform);
        enemyCards[ranCard].transform.GetChild(1).gameObject.SetActive(false);
        cardEnemyPlayed = enemyCards[ranCard].GetComponent<CardFeatures>().cardID;
        enemyCards.Remove(enemyCards[ranCard]);
        BattlePhase();
    }

    void BattlePhase() {
        switch (cardPlayerPlayed) {
            case 0:
                switch (cardEnemyPlayed) {
                    case 1:
                        // Lose
                        Transform parentTransform = playzone.transform.GetChild(0).transform;
                        RectTransform parentRectTransform = parentTransform.GetComponent<RectTransform>();
                        matchResult = results.lose;
                        breakAnimObj.transform.SetParent(parentTransform);
                        StartCoroutine(CardBreakAnimation(parentRectTransform));
                        enemyPoints++;
                        enemyPointsText.text = enemyPoints.ToString();
                        break;

                    case 2:
                        // Win
                        Transform parentTransform2 = playzone.transform.GetChild(1).transform;
                        RectTransform parentRectTransform2 = parentTransform2.GetComponent<RectTransform>();
                        matchResult = results.win;
                        breakAnimObj.transform.SetParent(playzone.transform.GetChild(1).transform);
                        StartCoroutine(CardBreakAnimation(parentRectTransform2));
                        playerPoints++;
                        break;
                }
                break;

            case 1:
                switch (cardEnemyPlayed) {
                    case 0:
                        // Win
                        Transform parentTransform = playzone.transform.GetChild(1).transform;
                        RectTransform parentRectTransform = parentTransform.GetComponent<RectTransform>();
                        matchResult = results.win;
                        breakAnimObj.transform.SetParent(playzone.transform.GetChild(1).transform);
                        StartCoroutine(CardBreakAnimation(parentRectTransform));
                        playerPoints++;
                        playerPointsText.text = playerPoints.ToString();
                        break;

                    case 1:
                        // It's a draw
                        matchResult = results.draw;
                        StartCoroutine(WaitOnDraw());
                        break;

                    case 2:
                        // Lose
                        Transform parentTransform2 = playzone.transform.GetChild(0).transform;
                        RectTransform parentRectTransform2 = parentTransform2.GetComponent<RectTransform>();
                        matchResult = results.lose;
                        breakAnimObj.transform.SetParent(playzone.transform.GetChild(0).transform);
                        StartCoroutine(CardBreakAnimation(parentRectTransform2));
                        enemyPoints++;
                        enemyPointsText.text = enemyPoints.ToString();
                        break;
                }
                break;
                    
            case 2:
                switch (cardEnemyPlayed) {
                    case 0:
                        // Lose
                        Transform parentTransform = playzone.transform.GetChild(0).transform;
                        RectTransform parentRectTransform = parentTransform.GetComponent<RectTransform>();
                        matchResult = results.lose;
                        breakAnimObj.transform.SetParent(playzone.transform.GetChild(0).transform);
                        StartCoroutine(CardBreakAnimation(parentRectTransform));
                        enemyPoints++;
                        enemyPointsText.text = enemyPoints.ToString();
                        break;

                    case 1:
                        // Win
                        Transform parentTransform2 = playzone.transform.GetChild(1).transform;
                        RectTransform parentRectTransform2 = parentTransform2.GetComponent<RectTransform>();
                        matchResult = results.win;
                        breakAnimObj.transform.SetParent(playzone.transform.GetChild(1).transform);
                        StartCoroutine(CardBreakAnimation(parentRectTransform2));
                        playerPoints++;
                        playerPointsText.text = playerPoints.ToString();
                        break;
                }
                break;
        }
    }

    void DetermineOutcome() {
        if (matchResult == results.draw) {
            foreach (Transform child in playzone.transform) {
                    GameObject.Destroy(child.gameObject);
            }
            if (round < 2 ) {
                round++;
                PlayerPlay();
            } else {
                round = 0;
                nextMatch = true;
                StartPhase();
            }
        } else if (matchResult == results.lose) {
            foreach (Transform child in playzone.transform) {
                    GameObject.Destroy(child.gameObject);
            }
            if (round < 2 ) {
                round++;
                PlayerPlay();
            } if (playerPoints == 3 || enemyPoints == 3) {
                ComparePoints();
            } else {
                round = 0;
                nextMatch = true;
                StartPhase();
            }
        } else if (matchResult == results.win) {
            foreach (Transform child in playzone.transform) {
                    GameObject.Destroy(child.gameObject);
            }
            if (round < 2 ) {
                round++;
                PlayerPlay();
            } if (playerPoints == 3 || enemyPoints == 3) {
                ComparePoints();
            } else {
                round = 0;
                nextMatch = true;
                StartPhase();
            }
        }
    }

    void ComparePoints() {
        if (playerPoints > enemyPoints || playerPoints == enemyPoints) {
            Victory();
        } else if (playerPoints < enemyPoints) {
            GameOver();
        }
    }

    void Victory () {
        Debug.Log("You win!");
    }

    void GameOver() {
        Debug.Log("You lose!");
    }

    void ShuffleHand() {
         for (int i = 0; i < playerCards.Count; i++) {
            GameObject temp = playerCards[i];
            int randomIndex = Random.Range(i, playerCards.Count - 1);
            playerCards[i] = playerCards[randomIndex];
            playerCards[randomIndex] = temp;
        }
    }

    IEnumerator CardBreakAnimation(RectTransform parentRectTransform) {
        breakAnimObj.rectTransform.sizeDelta = parentRectTransform.sizeDelta;
        breakAnimObj.rectTransform.position = parentRectTransform.position;
        breakAnimObj.rectTransform.rotation = parentRectTransform.rotation;
        breakAnimObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        breakAnimObj.gameObject.SetActive(false);
        breakAnimObj.transform.SetParent(null);
        DetermineOutcome();
    }

    IEnumerator WaitOnDraw() {
        yield return new WaitForSeconds(0.6f);
        DetermineOutcome();
    }

}
