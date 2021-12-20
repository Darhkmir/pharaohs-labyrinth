using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardFeatures : MonoBehaviour {

    public int cardID;
    private CardBattleManager cbm;

    void Start() {
        cbm = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardBattleManager>();
    }

    public void OnPointerClick() {
        Debug.Log("Mouse clicked");
        if (cbm.playerTurn) {
            Debug.Log("Player chose a card");
            cbm.cardPlayerPlayed = cardID;
            gameObject.transform.SetParent(cbm.playzone.transform);
            cbm.playerCards.Remove(gameObject);
            cbm.playerTurn = false;
            cbm.EnemyPlay();
        }
    }

}
