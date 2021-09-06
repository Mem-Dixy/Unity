public struct directory {
	static public readonly System.Text.Encoding unicode;
	static directory () {
		unicode = new System.Text.UnicodeEncoding(true , true , true);
	}
	public System.String data;
	public System.String path;
	public System.String[] directories;
	private System.String root;
	public directory (System.String import) {
		// Assert --> Fix issue with near root directory being passed in causing null reference exceptions.
		data = "";
		path = "";
		directories = new System.String[0];
		root = import;
	}
	public void back () {
//		load(System.IO.Directory.GetParent(path).FullName);
	}
	public System.Boolean move (System.String source , System.String destination) {
		System.Boolean output = false;
		try {
			System.IO.Directory.Move(source , destination);
			UnityEngine.Debug.Log("The directory move is complete.");
			output = true;
		}
		catch (System.ArgumentNullException) {
			UnityEngine.Debug.Log("Path is a null reference.");
		}
	catch (System.Security.SecurityException) {
		UnityEngine.Debug.Log("The caller does not have the required permission.");
	}
	catch (System.ArgumentException) {
		UnityEngine.Debug.Log("Path is an empty System.String, contains only white spaces, or contains invalid characters.");
	}
	catch (System.IO.IOException) {
		UnityEngine.Debug.Log("An attempt was made to move a directory to a different volume, or destDirName already exists.");
	}
	return output;
}
public const System.Int32 size = 0x1000;
static public void save (System.IO.Stream stream , System.Byte[] import) {
	stream.Write(import, 0x0 , size);
}
static public void load (System.IO.Stream stream , System.Byte[] import) {
	stream.Read(import , 0x0 , size);
}
static public System.Boolean make (System.Action<System.IO.Stream , System.Byte[]> function , System.String fileName , System.Byte[] import) {
	System.IO.FileStream stream;
	try { 
		stream = new System.IO.FileStream(fileName , System.IO.FileMode.Create , System.IO.FileAccess.ReadWrite , System.IO.FileShare.None , size , false);
		function(stream , import);
		stream.Dispose();
		return true;
	}
	catch (System.Exception error) {
		UnityEngine.Debug.Log("The file could not be used: " + error.Message);
	}
	return false;
}
public System.Boolean text (
	System.Func<
		System.Action<
			System.IO.Stream,
			System.Byte[]
		>,
		System.String,
		System.Byte[],
		System.Boolean
	> function,
	System.Action<
		System.IO.Stream,
		System.Byte[]
	> callback,
	System.String import,
	System.Byte[] data
) {
	System.String location;
	location = System.IO.Path.Combine(root , import);
/*
	System.String location = root;
	System.String[] array = import.TrimEnd().Split(' ');
	foreach (System.String item in array) {
		location = System.IO.Path.Combine(location , item);
		UnityEngine.Debug.Log("one.");
	}
*/
	UnityEngine.Debug.Log("root["+root+"]");
	UnityEngine.Debug.Log("location["+location+"]");
	UnityEngine.Debug.Log("path["+path+"]");
//	if (System.IO.Directory.Exists(location)) {
		try {
//			directories = System.IO.Directory.GetDirectories(location);
			function(callback , location , data);
//      function( callback , System.IO.Path.Combine(location , " ") , new System.Byte[0x2000]);//data );
//      function( callback , System.IO.Path.Combine(location , "dog.txt") , dump());//data );
			path = location;
			return true;
		}
		catch (System.IO.IOException) {
			UnityEngine.Debug.Log("Failed to load [" + path + "].");
		}
//	}
	return false;
}
static public System.Char[] dump () {
	System.UInt16 index = size;
	System.Char[] export = new System.Char[index];
	while (index > 0) {
		index -= 1;
		export[index] = ((System.Char)0xA727);
	}
	return export;
}

}