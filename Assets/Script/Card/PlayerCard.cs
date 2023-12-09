using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : Card
{
        private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            other.gameObject.GetComponent<Player_CardList>().AddCard(cname,num);
            Destroy(gameObject);
        }
    }
}
