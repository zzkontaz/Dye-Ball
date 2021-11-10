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
   [Header("Componentler")] 
   private Animator anim;

   private GameManager manager;

   private void Start()
   {
      anim = GetComponent<Animator>();
      manager = FindObjectOfType<GameManager>();

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

      if (Input.GetMouseButton(0) && !isNavigatingToFinish)
      {
         Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y,
            transform.position.z));
         pos.x= Mathf.Clamp(pos.x, -3, 3f);
         Vector3  nextPos =   new Vector3(pos.x, transform.position.y, transform.position.z);
         transform.position = Vector3.Lerp(transform.position,nextPos, 3* Time.deltaTime); 
      }

      if (isNavigatingToFinish)
      {
         Vector3  nextPos =   new Vector3(0, transform.position.y, transform.position.z);
         transform.position = Vector3.Lerp(transform.position,nextPos, 1* Time.deltaTime);
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
      if (stackedBalls.Count == 0)
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
