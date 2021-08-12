using UnityEngine;
using System.Collections;

public class PlayerBasic : MonoBehaviour {
	// Targeting:
	private Vector3 ourPosition = Vector3.zero;
	private float objectDistance = Mathf.Infinity;
	private int otherDamage = 0;
	private float otherHealth = Mathf.Infinity;
	private float yRotate = 180;
		//
	private int targetPriority = 0;
	public Transform tracking;
	//
	public bool human = false;		// Controlled by human.
	
	public Transform node;		// The node follow in path settings. 
	public Transform laser;		// We point the laser at what we want the guns to look at.
		
	public string teamSide = "";	// Make this Enum/int?
	public float bulletDamage;
	public float bulletSpeed;
	public float bulletReload;	// = 0.05;	.25 Tank
	public AudioClip soundFire;


	////
	////
	public float turnSpeed = 0;
	public float moveSpeed = 0;

	public float healthCurrent;
	public float healthMax;
	private Quaternion startDir;
	private Vector3 startPoint;


	public float[] cameraPositions = new float[7];	// Used by GameController Script to position camera. 7 numbers, y eulerAngles, thirdPerson camera(xrot, ypos zpos), firstPerson camera(xrot, ypos zpos).
	public AssetLoad.MapIcon mapIcon;	
//	private Vector3 startPoint;


	//// Transforms.
	//var explodeFlame : Transform;
	//var explodeSmoke : Transform;
	//// AudioClips.
	//var soundHealth : AudioClip;
	//var soundPlayerHit : AudioClip;
	//var soundPlayerExplode : AudioClip;
	//var soundFire : AudioClip;
	//
//	public Vector3 destination = Vector3.zero;		// Asigned by target finder 1 Script.
	
	
	public int sightRange = 512;		// NEED? Maybe we should just do a global search and get closest one.
	public bool autopilot = false;

	// Save our velocity when we pause.
	private bool paused = true;
	private Vector3 saveTransformVelocity;
	private Vector3 saveRotateVelocity;
	// Pointing guns:
	private Vector3 lookAt = Vector3.zero;	// Where the targets/guns look.
	private Transform[] target1;		// Array of guns y axis.
	private Transform[] target2;		// Array of guns x axis.
	// Fireing of guns:
	private int weaponSelect = 0;
	private System.Collections.Generic.List<System.Collections.Generic.List<Gun>> weaponList;		// A list of lists. Each major list referes to different weapons while each minor list referes to weapons of the same type.
	private System.Collections.Generic.List<int> subWeaponSelect;		// A list of where we are at in the fire sequence for each item.

	private void Start () {
		weaponList = new System.Collections.Generic.List<System.Collections.Generic.List<Gun>>();		// We use Lists to dynamicly keep track of the different weapons we find.
		subWeaponSelect = new System.Collections.Generic.List<int>();		// A list of where we are at in the fire sequence for each item.
		WeaponChange();

		Component[] tempArray = GetComponentsInChildren(typeof(Gun));
		// Find the things we use to point the guns.
		System.Collections.ArrayList target1List = new ArrayList();		// We use ArrayLists to dynamicly keep track of the different weapons we find.
		System.Collections.ArrayList target2List = new ArrayList();
		for (int i=0; i<tempArray.Length; i++) {
			Transform item = tempArray[i].transform;
			if (!target1List.Contains(item.parent)) {
				target1List.Add(item.parent);
			}
			if (!target2List.Contains(item.parent.parent)) {
				target2List.Add(item.parent.parent);
			}
		}
		target1 = new Transform[target1List.Count];
		target2 = new Transform[target2List.Count];
		target1List.CopyTo(target1);
		target2List.CopyTo(target2);
		// Make a laser.
		(laser = (Instantiate(AssetLoad.assetLoad.newGameObject, transform.position, transform.rotation) as GameObject).transform).parent = transform;
		(Instantiate(AssetLoad.assetLoad.mapIcon[(int)mapIcon], transform.position, transform.rotation) as Transform).parent = transform;
		(Instantiate(AssetLoad.assetLoad.healthBar, transform.position, transform.rotation) as Transform).parent = transform;

		if (turnSpeed==0 || moveSpeed==0) {
			Debug.LogError("Idiot. You forgot to set the speed.", this);
		}
		startPoint = transform.position;
		startDir = transform.rotation;
		healthMax = healthCurrent;
	}
	private void Update () {		// This controlls all of the guns turning. It makes sure that it is always being pointed at the laser.
		if (PauseMenu.paused || !LevelEdit.playing) {
			return;
		}
		// Get tracking object.
		RaycastHit hity;
		if (tracking) {
			Vector3 face = tracking.position-transform.position;
			if (human) {
				face = new Vector3(face.x, 0, face.z);		// Restrict to 2D plane.
			}
			if (Physics.Raycast(transform.position, face, out hity, sightRange)) {
				Debug.DrawRay(transform.position, face, Color.blue);
				if (hity.transform!=tracking) {		// If we are looking at a friend then don't fire.
					tracking = null;
				}
			}
		}
		if (!tracking) {		// If no target then reset variables.
			ourPosition = transform.position;
			objectDistance = Mathf.Infinity;
			otherDamage = 0;
			otherHealth = Mathf.Infinity;
			yRotate = 180;
			targetPriority = 0;
		}
		Collider[] colliderList = Physics.OverlapSphere(ourPosition, sightRange);		// Continually check for (better) targets.
		for (int i=0; i<colliderList.Length; i++) {															// Finds all objects near us.
			PlayerBasic other = colliderList[i].GetComponent(typeof(PlayerBasic)) as PlayerBasic;
			if (other && other.teamSide!=teamSide && Physics.Raycast(ourPosition, other.transform.position-ourPosition, out hity, Mathf.Infinity) && hity.transform==other.transform) {		// If not on the same team and if we can see them.
				int worth = (other.CompareTag("Defense") ? 4 : (other.CompareTag("Character") ? 3 : (other.CompareTag("Objective") ? 2 : (other.CompareTag("Building") ? 1 : 0))));
				if (worth>=targetPriority) {
					targetPriority = worth;		// Weights targets so it shoots at threats before buildings.
					float newY = Vector3.Angle(other.transform.position-ourPosition, transform.forward);
					if (newY+10<yRotate) {				// Target the one in front of us.
						yRotate = newY;
						float difference = Vector3.Distance(other.transform.position, ourPosition);
						if (difference-(sightRange/10)<objectDistance) {						// Find the characters that are closest.
							objectDistance = (difference<objectDistance ? difference : objectDistance);									// Have the one that is the closest be the reference.
							tracking = other.transform;						// If it makes it this far then assign the object to the target.
						}
					}
				}
			}
		}
		// Point laser.
		RaycastHit hit;
		lookAt = (Physics.Raycast(laser.position, laser.forward, out hit, Mathf.Infinity) ? hit.point : laser.position+laser.forward*10);
		Debug.DrawLine(laser.position, lookAt, Color.red);
		
		// Point all guns at laser.
		foreach (Transform self in target1) {
			RotateTo(lookAt, self, Vector3.right);
		}
		foreach (Transform self in target2) {
			RotateTo(lookAt, self, Vector3.up);
		}
		// Controlls all firing, swap weapons and shoot.
		if (subWeaponSelect.Count==0) {		// This prevents us from firing if we have no weapons.
			return;
		}
		bool fire1 = false;
		bool fire2 = false;
		bool fire3 = false;
		if (human) {
			fire1 = Input.GetButtonDown("Fire1");
			fire2 = Input.GetButtonDown("Fire2");
			fire3 = Input.GetButtonDown("Fire3");			
		}
		if (fire1) {
			Gun gun = weaponList[weaponSelect][subWeaponSelect[weaponSelect]];
			if (gun.Fire(GetComponent<Collider>())) {
	//			missileCount -= (missy.Fire(collider) ? 1 : 0);
	////			missileArray[missileSelect].renderer.enabled = false;
				subWeaponSelect[weaponSelect] = (subWeaponSelect[weaponSelect]+1<weaponList[weaponSelect].Count ? subWeaponSelect[weaponSelect]+1 : 0);
			}
		}
		if (fire2) {
			weaponSelect = (weaponSelect+1<weaponList.Count ? weaponSelect+1 : 0);
//			weaponSelect = (weaponSelect<weaponList.Count-1 ? weaponSelect : -1)+1;
		}
		else if (fire3) {
//			weaponSelect = (weaponSelect-1>=0 ? weaponSelect-1 : weaponList.Count-1);			
			weaponSelect = (weaponSelect>=1 ? weaponSelect : weaponList.Count)-1;			
		}


	}
	private void LateUpdate () {	// Wait for input to happen, then check to see if we are paused.
//		if (rigidbody && GameController.paused!=paused) {
//		if (rigidbody && PauseMenu.paused!=paused) {
//			if (paused) {
//				rigidbody.isKinematic = false;
//				rigidbody.velocity = saveTransformVelocity;
//				rigidbody.angularVelocity = saveRotateVelocity;
//			}
//			else {
//				saveTransformVelocity = rigidbody.velocity;
//				saveRotateVelocity = rigidbody.angularVelocity;
//				rigidbody.isKinematic = true;
//			}
//			paused = !paused;
//		}
		if (GetComponent<Rigidbody>() && PauseMenu.paused!=paused) {
			if (paused && LevelEdit.playing) {
				GetComponent<Rigidbody>().isKinematic = false;
				GetComponent<Rigidbody>().velocity = saveTransformVelocity;
				GetComponent<Rigidbody>().angularVelocity = saveRotateVelocity;
			}
			else {
				saveTransformVelocity = GetComponent<Rigidbody>().velocity;
				saveRotateVelocity = GetComponent<Rigidbody>().angularVelocity;
				GetComponent<Rigidbody>().isKinematic = true;
			}
			paused = !paused;
		}
	}
	private void OnTriggerEnter (Collider other) {
		if (other.transform==node) {		// If we find node then go to next one in the list.
			node = other.transform.parent;
		}
	}
	public void ResetPosition () {
		transform.rotation = startDir;
		transform.position = startPoint;
	}
	private void WeaponChange () {
		weaponList.Clear();
		subWeaponSelect.Clear();
		Component[] tempArray = GetComponentsInChildren(typeof(Gun));
		for (int i=0; i<tempArray.Length; i++) {
			Gun item = (Gun)tempArray[i];
			bool has = false;		// Do we already have any array of this type?
			for (int j=0; j<weaponList.Count; j++) {
				if (weaponList[j][0].weapon==item.weapon) {
					weaponList[j].Add(item);
					has = true;
				}
			}
			if (!has) {
				subWeaponSelect.Add(0);
				weaponList.Add(new System.Collections.Generic.List<Gun>());
				weaponList[weaponList.Count-1].Add(item);
			}
		}
	}
	public Vector3 GetDestination () {		// Where object looks at.
		float objectDistance = Mathf.Infinity;
		Vector3 objectPosition = Vector3.zero;
		Vector3 ourPosition = transform.position;
		Collider[] colliderList = Physics.OverlapSphere(ourPosition, sightRange);
		foreach (Collider other in colliderList) {															// Finds all objects near us.
			if (other.CompareTag("Node")) {
				float curentDistance = (other.transform.position-ourPosition).sqrMagnitude; 
				if (curentDistance<objectDistance) {
					objectDistance = curentDistance;									// Have the one that is the closest be the reference.
					objectPosition = other.transform.position;
				}
			}
		}
		return objectPosition;
	}
	// Overidable functions.
	public Vector3 RelativeRotationToWorldPoint (Vector3 worldPoint) {
		return RelativeRotationToWorldPoint(worldPoint, Vector3.up, transform);
	}
	public Vector3 RelativeRotationToWorldPoint (Vector3 worldPoint, Vector3 upDirection) {
		return RelativeRotationToWorldPoint(worldPoint, upDirection, transform);
	}
	public Vector3 RelativeRotationToWorldPoint (Vector3 worldPoint, Transform self) {
		return RelativeRotationToWorldPoint(worldPoint, Vector3.up, self);
	}
	public Vector3 RelativeRotationToWorldPoint (Vector3 worldPoint, Vector3 upDirection, Transform self) {
//		Debug.DrawLine(Vector3.zero, upDirection, Color.blue);
//		Debug.DrawLine(transform.position, transform.position+upDirection, Color.red);
		worldPoint = self.InverseTransformPoint(worldPoint);
		return new Vector3(-Mathf.Atan2(worldPoint.y, worldPoint.z)*Mathf.Rad2Deg, Mathf.Atan2(worldPoint.x, worldPoint.z)*Mathf.Rad2Deg, Mathf.Atan2(transform.InverseTransformDirection(-upDirection).x, transform.up.y)*Mathf.Rad2Deg);
	}
	public void InitiateCommand (Vector3 rotate, Vector3 translate) {
		InitiateCommand(rotate, translate, transform, turnSpeed);
	}
	public void InitiateCommand (Vector3 rotate, Vector3 translate, Transform self, float turnSpeed) {
		if (PauseMenu.paused || !LevelEdit.playing) {
			return;
		}
		if ((self.eulerAngles.x>45 && self.eulerAngles.x<315) || (self.eulerAngles.y>90 && self.eulerAngles.y<270)) {
			//inputSpin = RelativeRotationToPosition(self.position+Vector3.forward);
		}
		if (self.position.x<-90 || self.position.x>90 || self.position.y<-90 || self.position.y>90 || self.position.z<-90 || self.position.z>90) {
	//		inputMove = ;
		}
//		self.Rotate((rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : (rotate.magnitude>rotate.normalized.magnitude ? rotate : rotate*turnSpeed*Time.deltaTime)));
//		self.Rotate(true ? ((rotate.magnitude>rotate.normalized.magnitude ? rotate.normalized : rotate)*turnSpeed*Time.deltaTime) : (rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : rotate));

//		self.Rotate((rotate.magnitude>rotate.normalized.magnitude ? (rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : rotate) : rotate*turnSpeed*Time.deltaTime));
//		self.Rotate((rotate.magnitude>rotate.normalized.magnitude ? rotate.normalized : rotate)*turnSpeed*Time.deltaTime);
		self.Rotate((rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : rotate));
		self.Translate((translate.magnitude>=moveSpeed*Time.deltaTime ? translate.normalized*moveSpeed*Time.deltaTime : translate));

//		self.Rotate((rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : rotate));

//		self.Rotate((rotate.sqrMagnitude>=Mathf.Pow(turnSpeed*Time.deltaTime, 2) ? rotate.normalized*turnSpeed*Time.deltaTime : rotate));
//		self.Translate((translate.sqrMagnitude>=Mathf.Pow(moveSpeed*Time.deltaTime, 2) ? translate.normalized*moveSpeed*Time.deltaTime : translate));

//		self.Translate((translate.magnitude>=moveSpeed*Time.deltaTime ? translate.normalized*moveSpeed*Time.deltaTime : translate));
	}
	
	public void InitiateCommand (float turnSpeed, float moveSpeed, Transform self) {
		if (PauseMenu.paused || !LevelEdit.playing) {
			return;
		}
		Vector3 destination = GetDestination();
		Vector3 rotate = RelativeRotationToWorldPoint(destination, Vector3.up);
		self.Rotate((rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : rotate));
		Vector3 translate = self.InverseTransformPoint(destination);
		self.Translate((translate.magnitude>=moveSpeed*Time.deltaTime ? translate.normalized*moveSpeed*Time.deltaTime : translate));
	}
	
	
	
//	public void RotateTo (Vector3 destination, Transform self) {
//		RotateTo(destination, self, Vector3.one);
//	}
	public void RotateTo (Vector3 destination, Transform self, Vector3 axes) {
		if (PauseMenu.paused || !LevelEdit.playing) {
			return;
		}
		Vector3 rotate = AssetLoad.DimensionLimiter(RelativeRotationToWorldPoint(destination, Vector3.up, self), axes);
		self.Rotate((rotate.magnitude>=turnSpeed*Time.deltaTime ? rotate.normalized*turnSpeed*Time.deltaTime : rotate));
	}
	public void TranslateTo (Vector3 destination, Transform self, Vector3 axes) {
		if (PauseMenu.paused || !LevelEdit.playing) {
			return;
		}
		Vector3 translate = AssetLoad.DimensionLimiter(self.InverseTransformPoint(destination), axes);
		self.Translate((translate.magnitude>=moveSpeed*Time.deltaTime ? translate.normalized*moveSpeed*Time.deltaTime : translate));
	}
	public void PlayerInput (Transform self, Vector3 rotate, Vector3 translate) {
		if (PauseMenu.paused || !LevelEdit.playing) {
			return;
		}
		self.Rotate((rotate.magnitude>rotate.normalized.magnitude ? rotate.normalized : rotate)*turnSpeed*Time.deltaTime);
		self.Translate((translate.magnitude>translate.normalized.magnitude ? translate.normalized : translate)*moveSpeed*Time.deltaTime);
	}
//	public void PlayerInput (Transform self, Vector3 rotate, Vector3 translate, float turnSpeed, float moveSpeed) {
//		self.Rotate((rotate.magnitude>rotate.normalized.magnitude ? rotate.normalized : rotate)*turnSpeed*Time.deltaTime);
//		self.Translate((translate.magnitude>translate.normalized.magnitude ? translate.normalized : translate)*moveSpeed*Time.deltaTime);
//	}
}