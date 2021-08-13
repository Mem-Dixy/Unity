using UnityEngine;
using System.Collections;

public class Placer : MonoBehaviour {

	public static bool canPlace = false;

	private Mesh mainTri;		// The mesh attached to this object, used to see if others have collided with us.
	private Vector3[][] mainPoly;
	private Transform help;		// Helps figure out if triangles intersect. = new GameObject().GetComponent(typeof(Transform)) as Transform;
	private int[] optTri;
	private Vector3[] optVer;
	private Transform mainPos;
	private Vector3[] mainNormal;
	public float tolerance = .01f;		// Value between 0 (none) and less then 0.5 (full).
	private LevelEdit levelEdit;
	private Transform activeTile;
	
	public Transform test;
	
	public Transform meshObject;
	public Boundery boundery = 0;
	
	private Mesh poly;
	private Vector3[] vertex;
	
	
	private float boundrySphere = 4;
	private Vector3 boundryCube = new Vector3(4, 4, 4);
	

	private void Start () {
		levelEdit = FindObjectOfType(typeof(LevelEdit)) as LevelEdit;
		help = new GameObject().GetComponent(typeof(Transform)) as Transform;
		help.parent = transform;
//		Make(new Vector3[]{new Vector3(0.5f, 0.25f, -1), new Vector3(1, 5, 1), new Vector3(0.5f, 0.25f, 1)});
//		Make(new Vector3[]{new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0)});
//		print(TriOverlap(new Vector3[]{new Vector3(0.5f, 0.25f, -1), new Vector3(1, 5, 1), new Vector3(0.5f, 0.25f, 1)}, new Vector3[]{new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0)}));
	}
	public bool Check () {
		if (activeTile!=levelEdit.activeTile) {
			activeTile = levelEdit.activeTile;
			if (activeTile) {
				MeshFilter get = activeTile.GetComponent(typeof(MeshFilter)) as MeshFilter;
				if (get) {
					mainTri = get.mesh;
				}
				else {
					SkinnedMeshRenderer got = activeTile.GetComponent(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
					if (got) {
						mainTri = got.sharedMesh;
					}
					else {
						return false;
					}
				}
				optTri = mainTri.triangles;
				optVer = mainTri.vertices;
				mainPos = activeTile;
				mainPoly = new Vector3[mainTri.triangles.Length/3][];
				mainNormal = new Vector3[mainTri.triangles.Length/3];
				for (int k=0; k<mainTri.triangles.Length; k+=3) {
					mainPoly[k/3] = new Vector3[]{mainPos.TransformPoint(optVer[optTri[k]]), mainPos.TransformPoint(optVer[optTri[k+1]]), mainPos.TransformPoint(optVer[optTri[k+2]])};
					mainNormal[k/3] = Vector3.Cross((mainPoly[k/3][1]-mainPoly[k/3][0]), (mainPoly[k/3][2]-mainPoly[k/3][0]));//.normalized;	// Cross costly, normalize makes it worse.
				}
			}
		}
		if (!activeTile || mainPos==null) {
			return false;	// We can't drop something that we are not holding.
		}
//		Debug.Log(activeTile, activeTile);
		for (int k=0; k<mainTri.triangles.Length; k+=3) {
			mainPoly[k/3] = new Vector3[]{mainPos.TransformPoint(optVer[optTri[k]]), mainPos.TransformPoint(optVer[optTri[k+1]]), mainPos.TransformPoint(optVer[optTri[k+2]])};
		}
		canPlace = true;
//		Collider[] contact = Physics.OverlapSphere(transform.position, Mathf.Max(Mathf.Max(collider.bounds.max.x, collider.bounds.max.y), collider.bounds.max.z));
		Collider[] contact = Physics.OverlapSphere(activeTile.position, 32);
//		print(contact.Length);
		for (int i=0; i<contact.Length; i++) {
			if (contact[i].name=="Terrain") {
				continue;
			}
			MeshFilter get = (contact[i].GetComponent(typeof(MeshFilter)) as MeshFilter);
			if (!get) {
				continue;
			}
			Mesh otherTri = get.mesh;
			if (otherTri==mainTri) {
				continue;
			}
			Transform otherPos = contact[i].transform;
			int[] speTri = otherTri.triangles;
			Vector3[] speVer = otherTri.vertices;
			for (int j=0; j<speTri.Length; j+=3) {
				Vector3[] otherPoly = new Vector3[]{otherPos.TransformPoint(speVer[speTri[j]]), otherPos.TransformPoint(speVer[speTri[j+1]]), otherPos.TransformPoint(speVer[speTri[j+2]])};
				Vector3 otherNormal = Vector3.Cross((otherPoly[1]-otherPoly[0]), (otherPoly[2]-otherPoly[0]));//.normalized;	// Cross costly, normalize makes it worse.
				for (int k=0; k<mainPoly.Length; k++) {
//					Debug.DrawLine(mainTri.vertices[k], mainTri.vertices[k]+mainTri.normals[k], Color.green);
//					Debug.DrawLine(mainTri.vertices[k], mainTri.normals[k], Color.red);


//					if (TriOverlap(mainPoly[k], otherPoly, mainTri.normals[k]) || TriOverlap(otherPoly, mainPoly[k], otherTri.normals[j])) {		// Check both triangles.
//						canPlace = false;
//					}

					if (TriOverlap(mainPoly[k], otherPoly, mainNormal[k]) || TriOverlap(otherPoly, mainPoly[k], otherNormal)) {		// Check both triangles.
//						canPlace = false;
//						print(canPlace);
						return false;
					}
//					if (TriOverlap(mainPoly[k], otherPoly, Vector3.zero) || TriOverlap(otherPoly, mainPoly[k], Vector3.zero)) {		// Check both triangles.
//						canPlace = false;
//					}

 
//
////					Make(otherPoly);
//					if (TriOverlap(mainPoly[k], otherPoly)) {
//						canPlace = false;
////						print(canPlace);
////						return;
//					}

				}
			}
		}
		return true;
//		print(canPlace);
	}
	private bool TriOverlap (Vector3[] triangle1, Vector3[] triangle2, Vector3 triNormal) {
		if (triangle1.Length!=3 || triangle2.Length!=3) {
			print("Dumb");
			return false;
		}
//		triNormal = Vector3.Cross((triangle1[1]-triangle1[0]), (triangle1[2]-triangle1[0]));//.normalized;	// Cross costly, normalize makes it worse.

		triNormal = triNormal.normalized;
		Vector3 centroid = new Vector3((triangle1[0].x+triangle1[1].x+triangle1[2].x)/3, (triangle1[0].y+triangle1[1].y+triangle1[2].y)/3, (triangle1[0].z+triangle1[1].z+triangle1[2].z)/3);
		Debug.DrawLine(centroid, centroid+triNormal, Color.green);

		for (int index=0; index<3; index++) {
			int next = (index+1<3 ? index+1 : 0);
			help.position = triangle1[index];			// Place help on the plane of triangle1.
			help.LookAt(triangle1[next], triNormal);	// Rotate help so that up is along normal.

			float vert = help.InverseTransformPoint(triangle2[index]).y;		// Project verticie onto other triangle plane in such a way that y is outside of plane.
			try {
				vert = vert/(vert-help.InverseTransformPoint(triangle2[next]).y);		// If points are equal distance or on the same plane from the other triangle a zero will result.
//				print("norm");
			}
			catch {
//				print("Error");
				vert = 0.5f;		// If points are equal distance the result would be 0.5.
			}
//			if (Mathf.Abs(vert)<=1) {									// If true then the two points are on opposite sides of the triangle.
			if (vert>=0+tolerance && vert<=1-tolerance) {									// If true then the two points are on opposite sides of the triangle.
				int count = 0;
				Vector3 spot1 = Vector3.Lerp(triangle2[index], triangle2[next], vert);
				Debug.DrawLine(triangle2[index], spot1, Color.red);
				Debug.DrawLine(triangle2[next], spot1, Color.blue);
//				print(help.InverseTransformPoint(triangle2[index]).y);
//				print(help.InverseTransformPoint(triangle2[next]).y);
//				print((help.InverseTransformPoint(triangle2[index]).y-help.InverseTransformPoint(triangle2[next]).y));
//				print(help.InverseTransformPoint(triangle2[index]).y/(help.InverseTransformPoint(triangle2[index]).y-help.InverseTransformPoint(triangle2[next]).y));
//				print(vert);
//				print(count);
				for (int i=0; i<3; i++) {
					help.position = triangle1[i];
					help.LookAt(triangle1[(i+1<3 ? i+1 : 0)], triNormal);
					Vector3 spot = help.InverseTransformPoint(spot1);
					count += (int)Mathf.Sign(Mathf.Atan2(spot.x, spot.z)*Mathf.Rad2Deg);//*Mathf.Rad2Deg);
//					print(Mathf.Atan2(spot.x, spot.z)*Mathf.Rad2Deg);//*Mathf.Rad2Deg);
//					Debug.DrawLine(triangle1[i], triangle1[(i+1<3 ? i+1 : 0)], Color.white);
//					Debug.DrawLine(triangle1[i], triangle1[i]+triNormal, Color.black);
//					print(count);
				}
				if (Mathf.Abs(count)>=3) {
//					help.position = triangle1[2];
//					help.LookAt(triangle1[0], triNormal);
//
//					print("End");
//					Debug.DrawLine(centroid, triangle2[0], Color.cyan);
//					Debug.DrawLine(centroid, triangle2[1], Color.cyan);
//					Debug.DrawLine(centroid, triangle2[2], Color.cyan);
					Debug.DrawLine(triangle1[0], triangle2[0], Color.yellow);
					Debug.DrawLine(triangle1[1], triangle2[1], Color.yellow);
					Debug.DrawLine(triangle1[2], triangle2[2], Color.yellow);
					Debug.DrawLine(new Vector3((triangle2[0].x+triangle2[1].x+triangle2[2].x)/3, (triangle2[0].y+triangle2[1].y+triangle2[2].y)/3, (triangle2[0].z+triangle2[1].z+triangle2[2].z)/3), centroid, Color.cyan);

//					Debug.DrawLine(triangle1[0], triangle1[1], Color.white);
//					Debug.DrawLine(triangle1[1], triangle1[2], Color.white);
//					Debug.DrawLine(triangle1[2], triangle1[0], Color.white);

					return true;
				}
			}
		}
		return false;
	}
	private Vector3 Make (Vector3[] input) {
		GameObject make = new GameObject();
		make.AddComponent<MeshFilter>();
		make.AddComponent<MeshRenderer>();
		MeshFilter found = make.GetComponent(typeof(MeshFilter)) as MeshFilter;
		Mesh mesh = found.mesh;		
		mesh.Clear();
		mesh.vertices = input;
		mesh.uv = new Vector2[]{new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1)};
		mesh.triangles = new int[]{0, 1, 2};
		mesh.Optimize();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		Vector3 triNormal = Vector3.Cross((input[1]-input[0]), (input[2]-input[0])).normalized;		
		Vector3 centroid = new Vector3((input[0].x+input[1].x+input[2].x)/3, (input[0].y+input[1].y+input[2].y)/3, (input[0].z+input[1].z+input[2].z)/3);
		Debug.DrawLine(centroid, centroid+triNormal, Color.green);
		return triNormal;
	}
	private bool Check (Vector3 input) {
		Vector3 location = transform.position;
		if (boundery==Boundery.Circle) {
			return (Vector3.Distance(input, location)>boundrySphere);
		}
		else if (boundery==Boundery.Square) {
			return (Mathf.Abs(input.x-location.x)>boundryCube.x || Mathf.Abs(input.y-location.y)>boundryCube.y || Mathf.Abs(input.z-location.z)>boundryCube.z);
		}
		return false;
	}
//	private void OnDrawGizmos () {
////		vertex = poly.vertices;
////		for (int i=0; i<vertex.Length; i++) {
////			Gizmos.DrawSphere(meshObject.TransformPoint(vertex[i]), 0.1f);
////		}
//		if (canPlace) {
//			Gizmos.color = Color.green;
//		}
//		else {
//			Gizmos.color = Color.red;
//		}		
//		if (boundery==Boundery.Circle) {
//			Gizmos.DrawSphere(transform.position, boundrySphere);
//		}
//		else if (boundery==Boundery.Square) {
//			Gizmos.DrawCube(transform.position, boundryCube*2);
//		}
//	}
}

public enum Boundery {		// Final.
	Circle,
	Square
}
