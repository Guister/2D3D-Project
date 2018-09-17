﻿using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	public float lifetime = 0.1f;

	void Start ()
	{
		Destroy (gameObject, lifetime);
	}
}