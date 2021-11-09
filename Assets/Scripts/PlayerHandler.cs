using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
   [Header("Player Mekanikleri")]
   [SerializeField] private float moveSpeed;
   private bool canMove;
   public Transform StackTransform;
   public List<GameObject> stackedBalls = new List<GameObject>(); 

   [Header("Componentler")] 
   private Animator anim;

   private void Start()
   {
      anim = GetComponent<Animator>();
      SetMovementAnim(true);
   }

   private void FixedUpdate()
   {
      HandleMovement();
   }

   void SetMovementAnim(bool isMovementHapenning)
   {
      anim.SetBool("Movement",isMovementHapenning);
   }
   
   void HandleMovement()
   {
      transform.position += transform.forward * moveSpeed * Time.deltaTime;

      if (Input.GetMouseButton(0))
      {
         Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, transform.position.y,
            transform.position.z));
         pos.x= Mathf.Clamp(pos.x, -3, 3f);
         Vector3  nextPos =   new Vector3(pos.x, transform.position.y, transform.position.z);
         transform.position = Vector3.Lerp(transform.position,nextPos, 3* Time.deltaTime); 
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
      
      for (int i = stackedBalls.Count - 1; i >- 1; i--)
      {
       stackedBalls[i].GetComponent<BallHandler>().PlayStackedBallAnimation();
       yield return wait;
      }
   }
  
   
   
}
