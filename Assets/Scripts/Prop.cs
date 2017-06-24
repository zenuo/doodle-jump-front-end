using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{
		if (
			other.tag.Equals ("Player")
			&&
			!GameManager.INSTANCE.gaming.doodle.isUsingProp
			&&
			GameManager.INSTANCE.gaming.doodle.rebrithTimer < 0F) {
			Destroy (this.gameObject);
		}
	}
}
