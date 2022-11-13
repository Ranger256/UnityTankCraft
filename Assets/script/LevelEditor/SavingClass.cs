using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingClass {
	public string _tdCom { get; set;}
	public int _tdSize{ get; set;}
	public bool IsTer{ get; set;}

	public	SavingClass(){
	}
	public	SavingClass(string _tdname, int _tdsize, bool isTer){
		_tdCom = _tdname;
		_tdSize = _tdsize;
		IsTer = isTer;
	}
}
