using UnityEngine;
using System.Collections;

public class Mouseover2 : MonoBehaviour {

	Renderer shader;
	public GameObject YellowRobot;
	public GameObject PinkRobot;
	float speedCamera;
	float speedYRobot;
	float speedPRobot;



	void OnMouseOver()
	{

		if (gameObject.name == "Level1Cube" || gameObject.name == "Level2Cube") {
			shader = gameObject.GetComponent<Renderer> ();
			shader.material.shader = Shader.Find ("Standard");
			shader.material.color = new Color (1, 0.3f, 0.7f, 1);
		}
		else {
			shader = gameObject.GetComponent<Renderer>();
			shader.material.shader = Shader.Find ("Standard");
			shader.material.color = Color.yellow;
		}

	}

	void OnMouseExit()
	{
		shader.material.color = Color.white;
	}

	void OnMouseDown() 
	{
		if (gameObject.name == "Level1Cube") {
			Application.LoadLevel ("Level 1");
		} else if (gameObject.name == "Level2Cube") {
			Application.LoadLevel ("Level 2");
		}
		Debug.Log ("YouClicked");


		speedCamera = 3.5f;
		speedYRobot = 3.5f;
		speedPRobot = 3.5f;
	}

	void FixedUpdate () {
		if (Camera.main.transform.position.x >= 430)
			speedCamera = 0f;

		if (YellowRobot.transform.position.x >= 500)
			speedYRobot = 0f;

		if (PinkRobot.transform.position.x >= 360)
			speedPRobot = 0f;

		YellowRobot.transform.Translate (-speedYRobot, 0, 0);
		Camera.main.transform.Translate (speedCamera, 0, 0);
		PinkRobot.transform.Translate (-speedPRobot, 0, 0);
	}

}
