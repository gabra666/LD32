using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Life : MonoBehaviour {
   public float damageReduceFactorIfBlocking;
   public int maximunLife;
   public float currentLife;
   public AudioSource bloqueado_snd;
   public GameObject enemy;

   private CombatController combatController;

   // Use this for initialization
   void Start () {
      currentLife = maximunLife;
      combatController = gameObject.GetComponent<CombatController>();
      enemy = GameObject.FindGameObjectWithTag((tag == "Player") ? "Player2" : "Player");
   }

   void ReceiveDamage (Attack attack) {
      if (!combatController.isBlocking()) {
         combatController.damaged();
         currentLife -= attack.damage;
         gameObject.SendMessage("Hit");
         GameObject.Find("PunchMessagesController").SendMessage("Show", enemy);
      } else if (combatController.IsParry) {
         combatController.MakeParry();
      } else {
         if (combatController.isBlocking() && attack.attackType == 1) {
            combatController.breakDefense();
            currentLife -= (float) (0.2 * attack.damage);
            gameObject.SendMessage("Hit");
         } else {
            //gameObject.SendMessage ("hitBlocked");
            currentLife -= (float) (damageReduceFactorIfBlocking * attack.damage);
            bloqueado_snd.Play();
         }
      }
      checkLife();
   }

   private void checkLife () {
      if (currentLife <= 0) {
         GameObject.FindGameObjectWithTag("GameController").SendMessage("FightFinished", gameObject);
         GameObject.Find("PunchMessagesController").SendMessage("ShowDefeatedMessage", enemy);
      }
   }
}
