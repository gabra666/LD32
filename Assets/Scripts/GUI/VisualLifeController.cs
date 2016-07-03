using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class VisualLifeController : MonoBehaviour {
   public string playerTag;
   private Life playerLife;
   public RectTransform lifeTransform;
   public Image visualLife;
   public float maxTimeLifebarAnimation = 1;



   private float currentLife;
   private int maxLife;

   private float visualCurrentLife;
   private float visualLifeMaxWidth;
   private float visualLifeStartPosition;

   private bool isPlayer2;

   void Start () {
      playerLife = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Life>();
      maxLife = (int) playerLife.maximunLife;
      currentLife = maxLife;
      visualCurrentLife = currentLife;
      visualLife.color = new Color(1 - ((float) visualCurrentLife / (float) maxLife), ((float) visualCurrentLife / (float) maxLife), 0f);
      visualLifeMaxWidth = lifeTransform.sizeDelta.x;
      visualLifeStartPosition = lifeTransform.localPosition.x;
      isPlayer2 = playerLife.gameObject.tag == "Player2";
   }

   void Update () {
      float damage = currentLife - playerLife.currentLife;
      currentLife -= damage;
      StartCoroutine(CoolDownDamage(damage));
   }

   void handleLife () {

      lifeTransform.sizeDelta = new Vector2((visualLifeMaxWidth / maxLife) * visualCurrentLife, lifeTransform.sizeDelta.y);
      float xDisplacement = (isPlayer2 ? 1 : -1) * ((visualLifeMaxWidth - lifeTransform.sizeDelta.x) / 2);
      lifeTransform.localPosition = new Vector2(visualLifeStartPosition + xDisplacement, lifeTransform.localPosition.y);
      visualLife.color = new Color(1 - ((float) visualCurrentLife / (float) maxLife), ((float) visualCurrentLife / (float) maxLife), 0f);
   }

   IEnumerator CoolDownDamage (float damage) {
      float timeCoolDown = maxTimeLifebarAnimation / damage;
      for (int i = 0; i < damage; ++i) {
         --visualCurrentLife;
         handleLife();
         yield return new WaitForSeconds(timeCoolDown);
      }
   }
}
