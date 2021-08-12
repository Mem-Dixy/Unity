using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class FunctionTimeTest : MonoBehaviour {
	public void Start() {
		long ticks = Environment.TickCount;
		MyTestObject obj = new MyTestObject();
		for (int i = 0; i < 5000000; i++) {
			obj.Increment();
		}
		Debug.Log(Environment.TickCount - ticks);
		
		ticks = Environment.TickCount;
		obj = new MyTestObject();
		MethodInfo mi = typeof(MyTestObject).GetMethod("Increment");
		for (int i = 0; i < 5000000; i++) {
			mi.Invoke(obj, null);
		}
		Debug.Log(Environment.TickCount - ticks);
		
		ticks = Environment.TickCount;
		obj = new MyTestObject();
		mi = typeof(MyTestObject).GetMethod("GetIncDelegate");
		NoArgDelegate del = (NoArgDelegate)mi.Invoke(obj, null);
		for (int i = 0; i < 5000000; i++) {
			del();
		}
		Debug.Log(Environment.TickCount - ticks);
	}
	public delegate void NoArgDelegate();	
	public class MyTestObject {
		private int _counter = 0;	
		public void Increment() {
			_counter++;
		}
		public NoArgDelegate GetIncDelegate() {
			return new NoArgDelegate(Increment);
		}
	}
}
