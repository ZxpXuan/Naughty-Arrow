using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class TouchTrigger : MonoBehaviour
{
	private GameObject Communicate;
	[SerializeField]
	bool onlyTriggerOnce;

	[SerializeField]
	string[] TriggerTags;

	[SerializeField]
	float EnterTriggerDelay;
	[SerializeField]
	UnityEvent OnEnter;

	[SerializeField]
	float ExitTriggerDelay;
	[SerializeField]
	UnityEvent OnExit;

	bool triggered;

	bool IsTriggerable(Collision2D collision)
	{
		return IsTriggerable (collision.collider);
	}

	bool IsTriggerable(Collider2D collider)
	{
		if (triggered && onlyTriggerOnce)
			return false;

		if (TriggerTags.Length == 0)
			return true;
		
		foreach (var s in TriggerTags) {
			if (collider.tag == s) {
				return true;
			}
		}

		return false;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (!IsTriggerable (collider))
			return;
		triggered = true;

		StartCoroutine (Triggering(true));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (!IsTriggerable (collision))
			return;
		triggered = true;
		StartCoroutine (Triggering(true));
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (!IsTriggerable (collider))
			return;
		print("Exit");
		triggered = true;
		StartCoroutine (Triggering(false));
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (!IsTriggerable (collision))
			return;
		triggered = true;
		StartCoroutine (Triggering(false));
	}

	IEnumerator Triggering(bool enter)
	{
		if (enter) {
			if (EnterTriggerDelay > 0)
				yield return new WaitForSeconds (EnterTriggerDelay);
			if (OnEnter != null)
				OnEnter.Invoke ();
			print("Enter");
		} else {
			if (ExitTriggerDelay > 0)
				yield return new WaitForSeconds (ExitTriggerDelay);
			if (OnExit != null)
				OnExit.Invoke ();
		}
	}
	public void ChangeLevel(){
		Application.LoadLevel (2);
	}
	public void ChangeLevel2(){
		Application.LoadLevel (3);
	}

}
