using UnityEngine;
using System.Collections.Generic;

public static class TexturesLoader {
	public static Dictionary<string,Texture2D> texturesDiffuse = new Dictionary<string, Texture2D>();
	public static Dictionary<string,Texture2D> texturesNormal = new Dictionary<string, Texture2D>();
	public static Dictionary<string,Texture2D> texturesSpecular = new Dictionary<string, Texture2D>();
	public static Dictionary<string,Texture2D> texturesGlossiness = new Dictionary<string, Texture2D>();

	public static void LoadedTextures(){
		string[] pathDiffuseTextures = System.IO.Directory.GetFiles ("media/textures/Diffuse/", "*.png", System.IO.SearchOption.AllDirectories);
		for(int i =0; i < pathDiffuseTextures.Length; i++){
			texturesDiffuse.Add (pathDiffuseTextures[i], Dummiesman.ImageLoader.LoadTexture(pathDiffuseTextures[i]));
		}
	}

}
