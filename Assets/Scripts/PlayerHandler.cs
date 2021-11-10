using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
   [Header("Player Mekanikleri")]
   [SerializeField] private float moveSpeed;
   public bool canMove;
   public Transform StackTransform;
   public List<GameObject> stackedBalls = new List<GameObject>();
   private bool isNavigatingToFinish = false;
   private Transform localTrans;
   private Vector3 lastMousePos;
   private Vector3 mousePos;
   private Vector3 newTransPos;

   [Header("Componentler")] 
   private Animator anim;
   private GameManager manager;
   

   private void Start()
   {
      anim = GetComponent<Animator>();
      manager = FindObjectOfType<GameManager>();
      localTrans = GetComponent<Transform>();

   }

   public void StartPlayer()
   {
      canMove = true;
      SetMovementAnim(true);
   }
   private void FixedUpdate()
   {
     if(canMove)HandleMovement();
   }

   private void LateUpdate()
   {
      CheckFinishCannon();
   }

   void CheckFinishCannon()
   {
      if (stackedBalls.Count == 0 && isNavigatingToFinish)
      {
         manager.GameFinish();
         SetMovementAnim(false); 
         isNavigatingToFinish = false;
      }
     
   }

   void SetMovementAnim(bool isMovementHapenning)
   {
      anim.SetBool("Movement",isMovementHapenning);
   }
   
   void HandleMovement()
   {
      transform.position += transform.forward * moveSpeed * Time.deltaTime;

      if (Input.GetMouseButton(0) && !isNavigatingToFinish) // YENİ HAREKET SİSTEMİ İLE EKRANDA DOKUNULAN YERE DAHA İYİ ULAŞIYORUZ
      {
         mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
            10f));
         
         float xDifference = mousePos.x - transform.position.x;
         
         newTransPos.x = transform.position.x + xDifference * Time.deltaTime * 10;
         newTransPos.y = transform.position.y;
         newTransPos.z = transform.position.z;
         newTransPos.x = Mathf.Clamp(newTransPos.x, -3, 3);
         Vector3 truePos = new Vector3(newTransPos.x,transform.position.y,transform.position.z);
         transform.position = truePos;
         lastMousePos = mousePos;

      }
      
      if (isNavigatingToFinish)
      {
         Vector3  nextPos =   new Vector3(0, transform.position.y, transform.position.z);
         transform.position = Vector3.Lerp(transform.position,nextPos, 1* Time.deltaTime);
      }
   }

   public void UpdateBallOrder()
   {
      for (int i = 0; i < stackedBalls.Count; i++) // TOPLAR ÖZELİNDE DEĞİŞİKLİK GERÇEKLEŞTİĞİ ZAMAN LİSTEMİZİ GÜNCELLİYORUZ
      {
         stackedBalls[i].GetComponent<BallHandler>().ballOrder = i;
      }
   }
   
   public void AddBallToStackList(GameObject ball)
   {
      stackedBalls.Add(ball);
      ball.GetComponent<BallHandler>().ballOrder = stackedBalls.Count;
      StartCoroutine(AnimateBalls());
   }

   IEnumerator AnimateBalls()
   {
      WaitForSeconds wait = new WaitForSeconds(0.1f);
      
      for (int i = stackedBalls.Count - 1; i > 0; i--)
      {
       stackedBalls[i].GetComponent<BallHandler>().PlayStackedBallAnimation();
       yield return wait;
      }
   }

   void CheckFail()
   {
      if (stackedBalls.Count == 0 && !isNavigatingToFinish)
      {
        Death();
      }
   }

   void Death()
   {
      canMove = false;
      manager.MissionFailed();
      SetMovementAnim(false);
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Finish"))
      {
         isNavigatingToFinish = true;
      }

      if (other.CompareTag("Obstacle") || other.CompareTag("Wall"))
      {
         CheckFail();
      }
   }
}
