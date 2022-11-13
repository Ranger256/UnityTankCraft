using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXML {

	public float Posx { get; set;}
	public float Posy { get; set;}
	public float Posz { get; set;}
	public float Rotx { get; set;}
	public float Roty { get; set;}
	public float Rotz { get; set;}

	public float Posbx { get; set;}
	public float Posby { get; set;}
	public float Posbz { get; set;}
	public float Rotbx { get; set;}
	public float Rotby { get; set;}
	public float Rotbz { get; set;}

	void charac(){
	}

	public PlayerXML(){
	}
	public PlayerXML (float posx, float posy, float posz, float rotx, float roty, float rotz, float posbx, float posby,float posbz,float rotbx,float rotby,float rotbz ){
		Posx = posx;
		Posy = posy;
		Posz = posz;
		Rotx = rotx;
		Roty = roty;
		Rotz = rotz;

		Posbx = posbx;
		Posby = posby;
		Posbz = posbz;
		Rotbx = rotbx;
		Rotby = rotby;
		Rotbz = rotbz;
	}
}
