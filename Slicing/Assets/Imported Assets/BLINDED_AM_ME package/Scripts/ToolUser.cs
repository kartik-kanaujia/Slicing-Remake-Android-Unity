using UnityEngine;
using System.Collections;

public class ToolUser : MonoBehaviour {

	public Material capMaterial;
	public AudioSource good;
	public AudioSource bad;

	public bool isPerfect = false;

	// Use this for initialization
	void Start () {

		
	}
	
	void Update(){

		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;

			if (Physics.Raycast (transform.position, transform.forward, out hit, Mathf.Infinity, 1 << 8)) //is infinity fine? no operational issues?
			{
				if (hit.collider.gameObject.tag != "over" && hit.collider.gameObject.tag != "kill")
				{
					if (hit.collider.gameObject.GetComponent<Rigidbody> ().velocity.y > 0)
					{
						print ("Power Slice!!!");
						FindObjectOfType<DestroyOnTrigger> ().IncrementScore (2);
						FindObjectOfType<DestroyOnTrigger> ().UnlockAchievementOne ();
						FindObjectOfType<DestroyOnTrigger> ().IncrementAchievementOne ();
					}

					GameObject victim = hit.collider.gameObject;

					GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.right, capMaterial);

					if (!pieces [1].GetComponent<Rigidbody> ())
						pieces [1].AddComponent<Rigidbody> ();

					if (!pieces [1].GetComponent<BoxCollider> ())
						pieces [1].AddComponent<BoxCollider> ();

					pieces [0].GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-1f, 1f), 5.3f, 0f);
					pieces [1].GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-1f, 1f), 5f, 0f);

//				pieces [0].transform.Rotate (0,0,Random.Range(pieces[0].transform.rotation.z,360));
//				pieces [1].transform.Rotate (0,0,Random.Range(pieces[1].transform.rotation.z,360));
					//pieces[1].AddComponent<RotateIt>();

					pieces [0].gameObject.tag = "over";
					pieces [1].gameObject.tag = "over";

					pieces [0].layer = 10;
					pieces [1].layer = 10;

					Color color;
					color = pieces [0].GetComponent<Renderer> ().material.color;
					color = pieces [1].GetComponent<Renderer> ().material.color;
					color.a = 0.4f;
					pieces [0].GetComponent<Renderer> ().material.color = color;
					pieces [1].GetComponent<Renderer> ().material.color = color;

					pieces [0].GetComponent<RotateIt> ().enabled = false;

					FindObjectOfType<DestroyOnTrigger> ().IncrementScore (1);
					good.Play ();

					if (hit.transform.position.y > -0.05f && hit.transform.position.y < 0.05f)
					{
						print ("perfect slice!!!");
						FindObjectOfType<AudioManager> ().emptyTaps--;
						isPerfect = true;
						FindObjectOfType<DestroyOnTrigger> ().IncrementScore (4);
						FindObjectOfType<DestroyOnTrigger> ().UnlockAchievementTwo ();
						FindObjectOfType<DestroyOnTrigger> ().IncrementAchievementTwo ();
					}

					if (isPerfect == true)
					{
						StartCoroutine (PerfectWait (0.6f));
					}
					else
					{
						FindObjectOfType<Spawner> ().SpawnPrefabs ();
					}
//				pieces [1].GetComponent<Collider> ().enabled = false;
				}
				else
				if (hit.collider.gameObject.tag == "kill")
				{
					GameObject victim = hit.collider.gameObject;

					GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut (victim, transform.position, transform.right, capMaterial);

					if (!pieces [1].GetComponent<Rigidbody> ())
						pieces [1].AddComponent<Rigidbody> ();

					if (!pieces [1].GetComponent<BoxCollider> ())
						pieces [1].AddComponent<BoxCollider> ();

					pieces [0].GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-1f, 1f), 5.3f, 0f);
					pieces [1].GetComponent<Rigidbody> ().velocity = new Vector3 (Random.Range (-1f, 1f), 5f, 0f);

					//				pieces [0].transform.Rotate (0,0,Random.Range(pieces[0].transform.rotation.z,360));
					//				pieces [1].transform.Rotate (0,0,Random.Range(pieces[1].transform.rotation.z,360));
					//pieces[1].AddComponent<RotateIt>();

					pieces [0].gameObject.tag = "over";
					pieces [1].gameObject.tag = "over";

					pieces [0].layer = 10;
					pieces [1].layer = 10;
					pieces [0].GetComponent<RotateIt> ().enabled = false;
					//				pieces [1].GetComponent<Collider> ().enabled = false;

					bad.Play ();
					FindObjectOfType<DestroyOnTrigger> ().IsGameOver ();
					FindObjectOfType<DestroyOnTrigger> ().GetRandomTip ();
				}
				else
				if (hit.collider.gameObject.tag == "over")
				{
					print ("Not ignoring layer");
				}
			}
			else //ray didnt hit anything
			{
				if (FindObjectOfType<AudioManager> ().gameStarted == true)
				{
					FindObjectOfType<AudioManager> ().emptyTaps++;
					FindObjectOfType<DestroyOnTrigger> ().ShakeCamOnEmptyTaps ();
					if (FindObjectOfType<AudioManager> ().emptyTaps == 3)
					{
						FindObjectOfType<DestroyOnTrigger> ().IsGameOver ();
						FindObjectOfType<DestroyOnTrigger> ().GetRandomTip ();
					}
				}
			}

		}
	}

	IEnumerator PerfectWait(float time)
	{
		yield return new WaitForSeconds (time);

		isPerfect = false;
		if (FindObjectOfType<Spawner> ())
		{
			FindObjectOfType<Spawner> ().SpawnPrefabs ();
		}
	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);

	}

}
