using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectiable : MonoBehaviour
{
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if (controller.CurrentHealth < controller.maxHealth) 
            { 
                controller.ChangeHealth(1);
                controller.itemUpEffect.Play();
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
    }
}
