﻿using System.Collections;
using UnityEngine;


namespace BLINDED_AM_ME{

	[ExecuteInEditMode]
	public class SetDepthTextureMode : MonoBehaviour {

		public DepthTextureMode mode = DepthTextureMode.Depth;

		public void Start(){

			if(Application.isPlaying)
				Time.timeScale = 0.25f;
		}

		// before a camera renders this 
		public void OnWillRenderObject()
		{

			if(!enabled)
				return;

			Camera cam = Camera.current;
			if( !cam )
				return;
			
			cam.depthTextureMode = mode;

		}
			
	}
}