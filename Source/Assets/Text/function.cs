static public class function {
//	static public void analyze (ref box[] highlight , ref window main , ref UnityEngine.Rect[] pixel){
//		foreach ( box light in highlight ) {
//			light.rect.Clear();
//		}
//		for ( System.UInt16 index_b = 2048 ; index_b < 4096 ; index_b += 1 ) {
//			highlight[is_number( main.get( index_b ) )].rect.Add(pixel[ index_b - 2048 ]);
//		}
//	}
	static public void analyze (System.String import){
		UnityEngine.Debug.Log(import);
	}
//	static public void analyze ( System.UInt16 start, System.UInt16 ended, ref System.Char[] data ) {
	static public void analyze ( System.UInt16 index , ref System.Char[] data ) {
		System.UInt16 start = 0;//(System.UInt16)(((index/window.many_x)+0)*window.many_x);
		System.UInt16 ended = 0;//(System.UInt16)(((index/window.many_x)+1)*window.many_x);
//		System.Char[] word = new System.Char[256];
		while ( start < ended ) {
			if (data[start]!='~') {
				
			}
			start += 1;
		}
	}
	/*private static*/static public System.Int32 is_number (System.String import) {
		System.Int32 export = 0;
		switch (import) {
			case "1":
				export = 1;
			break;
			case "2":
				export = 2;
			break;
			case "3":
				export = 3;
			break;
//			case "4":
//			case "5":
//			case "6":
		}
		return export;
	}
	/*private*/static public System.String extract (window main , System.UInt16 index , System.UInt16 limit) {
		System.String export = "";
		while (index<limit) {
//			export += main.get(index);
			index +=1;
		}
		return export;
	}

	/*
		public System.String read_line (System.Int16 index) {
			System.String export = "";
			index *= window.many_x;
			System.Int16 limit = (System.Int16)(index+window.many_x);
			while (index<limit) {
				export += text[index];
				index +=1;
			}
			return export;
		}
		*/

}
