using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BLINDED_AM_ME{

	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleTexture : MonoBehaviour {

		public float      particleSize = 0.1f;
		public float      particleSpacing = 0.1f;
		public Vector2    particleGridSize = new Vector2(100, 100);

		public Gradient   heightGradient;
		public float      heightMax = 0.5f;

		public Texture2D  heightMapTexture;
		private float[,] _heightGrid;

		public Texture2D  colorTexture;
		private Color[,] _colorGrid;


		private int                       _particleCount;
		private ParticleSystem.Particle[] _particleArray;
		private ParticleSystem            _particleSystem;


		void Reset(){

			heightGradient = new Gradient();
			heightGradient.SetKeys(new GradientColorKey[]{
				new GradientColorKey(Color.white,0),
				new GradientColorKey(Color.white,1),

			}, new GradientAlphaKey[]{
				new GradientAlphaKey(1,0),
				new GradientAlphaKey(1,1),
			});

			SetupSystem();
		}
			

		// Use this for initialization
		void Start () {

			ScanPattern();
			SetupSystem();
			SetParticles();

		}

		// if you want
//		void Update(){
//
//			SetParticles();
//
//		}

		void ScanPattern(){


			// make it zero
			if(heightMapTexture == null){

				_heightGrid = new float[,]{ 
					{ 0.0f, 0.0f, 0.0f, 0.0f },
					{ 0.0f, 0.0f, 0.0f, 0.0f }
				};

			}else{

				// now for the height map
				Color[] colors = heightMapTexture.GetPixels();

				_heightGrid = new float[heightMapTexture.width, heightMapTexture.height];

				// convert to my 2D array
				for(int x=0; x<heightMapTexture.width; x++){
					for(int y=0; y<heightMapTexture.height; y++){
						_heightGrid[x,y] = colors[y * heightMapTexture.height + x].a;
					}
				}
			}

			// make it white if null
			if(colorTexture == null){

				_colorGrid = new Color[,]{
					{ Color.white,Color.white,Color.white,Color.white },
					{ Color.white,Color.white,Color.white,Color.white }
				};

			}else{

				Color[] colors = colorTexture.GetPixels();

				_colorGrid = new Color[colorTexture.width, colorTexture.height];

				// convert to my 2D array
				for(int x=0; x<colorTexture.width; x++){
					for(int y=0; y<colorTexture.height; y++){
						_colorGrid[x,y] = colors[y * colorTexture.height + x];
					}
				}
			}

		}

		void SetupSystem(){

			_particleCount = Mathf.FloorToInt(particleGridSize.x) * Mathf.FloorToInt(particleGridSize.y);
			_particleArray = new ParticleSystem.Particle[_particleCount];
			_particleSystem = GetComponent<ParticleSystem>();
			_particleSystem.maxParticles = _particleCount;


			ParticleSystem.EmissionModule em = _particleSystem.emission;
			em.enabled = false;
			ParticleSystem.MainModule mai = _particleSystem.main;
			mai.simulationSpace = ParticleSystemSimulationSpace.Local;
			mai.loop = false;
			mai.playOnAwake = false;

		}


		float normX,normY,
		arrayWidth,arrayHeight,
		colorWidth,colorHeight,
		heightGridWidth,heightGridHeight,
		tempF;


		void SetParticles(){

			normX = 0.0f;
			normY = 0.0f;
			arrayWidth  = Mathf.Floor(particleGridSize.x);
			arrayHeight = Mathf.Floor(particleGridSize.y);
			colorWidth  = (float) _colorGrid.GetLength(0);
			colorHeight = (float) _colorGrid.GetLength(1);
			heightGridWidth  = (float) _heightGrid.GetLength(0);
			heightGridHeight = (float) _heightGrid.GetLength(1);
			tempF = 0.0f;

			int i = 0;

			for(float x=0; x<arrayWidth; x++){
				normX = x/arrayWidth;

				for(float y=0; y<arrayHeight; y++){
					normY = y/arrayHeight;
					
					_particleArray[i].startLifetime = 1.0f;
					_particleArray[i].remainingLifetime = 1.0f;
					_particleArray[i].startSize = particleSize;

					tempF = _heightGrid[
						(int)(normX * heightGridWidth),
						(int)(normY * heightGridHeight)
					];

					_particleArray[i].position = new Vector3(
						x * particleSpacing,
						tempF * heightMax,
						y * particleSpacing);

					_particleArray[i].startColor = heightGradient.Evaluate(tempF) * _colorGrid[ 
						(int)(normX * colorWidth),
						(int)(normY * colorHeight) 
					];

					i++;
				}
			}

			_particleSystem.SetParticles(_particleArray, _particleCount);

		}

	}
}