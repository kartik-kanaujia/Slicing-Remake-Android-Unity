﻿using UnityEngine;
using System.Collections;


namespace BLINDED_AM_ME{

	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class MeshColliderPerMaterial : MonoBehaviour {

		#if UNITY_EDITOR


		public void Button_MakeIt(){

			Mesh         _mesh = GetComponent<MeshFilter>().sharedMesh;
			MeshRenderer _renderer = GetComponent<MeshRenderer>(); 

			Mesh[] _made_meshes = new Mesh[_mesh.subMeshCount];

			// go through the sub indices
			for(int sub=0; sub<_mesh.subMeshCount; sub++){

				Mesh_Maker maker = new Mesh_Maker();
				int[] trinagles = _mesh.GetTriangles(sub);

				// go through the triangles
				for(int i=0; i<trinagles.Length; i+=3){

					maker.AddTriangle(new Vector3[]{
						_mesh.vertices[trinagles[i]], _mesh.vertices[trinagles[i+1]], _mesh.vertices[trinagles[i+2]]
					}, new Vector3[]{
						_mesh.normals[trinagles[i]], _mesh.normals[trinagles[i+1]], _mesh.normals[trinagles[i+2]]
					}, new Vector2[]{
						_mesh.uv[trinagles[i]], _mesh.uv[trinagles[i+1]], _mesh.uv[trinagles[i+2]]
					},0);
				}

				maker.RemoveDoubles();

				_made_meshes[sub] = maker.GetMesh();
			}



			// too many
			while(transform.childCount > _mesh.subMeshCount){
				Transform obj = transform.GetChild(transform.childCount-1);
				obj.parent = null;
				DestroyImmediate(obj.gameObject);
			}

			// too little
			while(transform.childCount < _mesh.subMeshCount){
				Transform obj = new GameObject("child").transform;
				obj.SetParent(transform);
				obj.localPosition = Vector3.zero;
				obj.localRotation = Quaternion.identity;
				obj.gameObject.AddComponent<MeshCollider>();
			}
				
			for(int i=0; i<transform.childCount; i++){
				Transform obj = transform.GetChild(i);

				obj.gameObject.name = _renderer.sharedMaterials[i].name;

				_made_meshes[i].name = obj.gameObject.name;

				obj.GetComponent<MeshCollider>().sharedMesh = _made_meshes[i];

			}

		}

	
		#endif
	}
}