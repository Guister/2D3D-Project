using UnityEngine;
using System.Collections;

public class movingSimulate : MonoBehaviour {

	Renderer rend;
	Material mate;
	float pulse;
	float offset;
	Color InitialColor;
	Color FinalColor;

	// Use this for initialization
	void Start () {

		rend = gameObject.GetComponent<MeshRenderer>();
		mate = rend.material;
		offset =  0;

	}
	
	// Update is called once per frame
	void Update () {
		offset -= 0.01f;
		rend.material.SetTextureOffset("_MainTex" , new Vector3(offset,0,0));


			
		 




		pulse = Mathf.PingPong (Time.time, 1);

		Color baseColor = Color.white;
		Color finalColor = baseColor * Mathf.LinearToGammaSpace (pulse);

		mate.SetColor ("_EmissionColor", finalColor);
	}
}
