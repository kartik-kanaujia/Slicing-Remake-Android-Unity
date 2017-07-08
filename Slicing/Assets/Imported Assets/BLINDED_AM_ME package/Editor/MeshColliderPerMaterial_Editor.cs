using UnityEngine;
using UnityEditor;
using System.Collections;
using BLINDED_AM_ME;

namespace BLINDED_AM_ME.Inspector{


	[CustomEditor(typeof(MeshColliderPerMaterial))]
	[CanEditMultipleObjects]
	public class MeshColliderPerMaterial_Editor : Editor{

		public override void OnInspectorGUI()
		{

			DrawDefaultInspector();

			Object[] myScripts = targets;
			if(GUILayout.Button("Make It"))
			{
				MeshColliderPerMaterial maker;
				for(int i=0; i<myScripts.Length; i++){
					maker = (MeshColliderPerMaterial) myScripts[i];

					maker.Button_MakeIt();
				}
			}
		}

		public void Update(){



		}

	}

}