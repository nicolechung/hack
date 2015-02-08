using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void NotifyAction();

	public static event NotifyAction Flee;

	public static event NotifyAction Seek;

	public static event NotifyAction Locked;
}
