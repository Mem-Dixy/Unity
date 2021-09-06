namespace nomad {
	public struct point {
		public System.UInt16 x;
		public System.UInt16 y;
		public point (System.UInt16 X,System.UInt16 Y) {
			x = X;
			y = Y;
		}
		public point (System.UInt32 X,System.UInt32 Y) {
			x = (System.UInt16)X;
			y = (System.UInt16)Y;
		}
		public point (System.Int32 X,System.Int32 Y) {
			x = (System.UInt16)X;
			y = (System.UInt16)Y;
		}
		/*
		public void target (System.UInt16 index) {
			return (System.UInt16)(x*y);
		}
		*/
		public System.UInt16 length () {
			return (System.UInt16)(x*y);
		}
		static public point operator+ (point import_a, point import_b) {
			point export = new point((System.UInt16)(import_a.x+import_b.x) , (System.UInt16)(import_a.y+import_b.y));
			return export;
		}
		static public point operator- (point import_a, point import_b) {
//			point export = new point((System.UInt16)(import_a.x-import_b.x) , (System.UInt16)(import_a.y-import_b.y));
			//UnityEngine.Debug.Log("<"+(import_b.x)+","+(~import_b.x)+","+(~import_b.x+0x1)+">");
//			UnityEngine.Debug.Log("<"+((System.UInt16)((System.UInt16)(+import_a.x)+(System.UInt16)(-import_b.x)))+","+((System.UInt16)(+import_a.x))+","+((System.UInt16)(-import_b.x))+">");
//			point export = new point((System.UInt16)(import_a.x+(~import_b.x+0x1)) , (System.UInt16)(import_a.y+(~import_b.y+0x1)));
			point export = new point((System.UInt16)((System.UInt16)(+import_a.x)+(System.UInt16)(-import_b.x)) , (System.UInt16)((System.UInt16)(+import_a.y)+(System.UInt16)(-import_b.y)));
			return export;
		}
		static public point operator* (point import_a, point import_b) {
			point export = new point((System.UInt16)(import_a.x*import_b.x) , (System.UInt16)(import_a.y*import_b.y));
			return export;
		}
		static public point operator* (System.UInt16 import_a , point import_b) {
			point export=new point((System.UInt16)(import_a*import_b.x) , (System.UInt16)(import_a*import_b.y));
			return export;
		}
		static public point operator/ (point import_a , System.UInt16 import_b) {
			point export=new point((System.UInt16)(import_a.x/import_b) , (System.UInt16)(import_a.y/import_b));
			return export;
		}
		static public point operator/ (point import_a , point import_b) {
			point export=new point((System.UInt16)(import_a.x/import_b.x) , (System.UInt16)(import_a.y/import_b.y));
			return export;
		}
	}
	static class data {
		static System.UInt16 s (System.Int32 import) {
			return ((System.UInt16)(import));
		}
	}
}
