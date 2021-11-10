using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
   public Enemy enemy {get; private set;}
   public float duration;

   private void Awake()
   {
        enemy = GetComponent<Enemy>();
        this.enabled = false;    
   }

   public void Enable()
   {
       Enable(duration);
   }

   public virtual void Enable(float duration)
   {
       this.enabled = true;
       CancelInvoke();
       Invoke("Disable", duration);
   }

   public virtual void Disable()
   {
       this.enabled = false;
       CancelInvoke();
   } 
}
