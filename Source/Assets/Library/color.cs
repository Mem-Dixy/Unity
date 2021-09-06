public enum property {
	positive ,
	negative ,
	inactive ,
	reactive
}
public enum color {
	clear,
	white,
	black,
	faint,
	grey,
	half,
	cyan,
	magenta,
	yellow,
	red,
	green,
	blue,
	length
}
static public class paint {
	static private UnityEngine.Color[] array;
	static paint () {
		array = new UnityEngine.Color[(System.Byte)color.length];
		array[(System.Byte)color.clear] =   new UnityEngine.Color((System.Single)0.0,(System.Single)0.0,(System.Single)0.0,(System.Single)0.0);
		array[(System.Byte)color.white] =   new UnityEngine.Color((System.Single)1.0,(System.Single)1.0,(System.Single)1.0,(System.Single)1.0);
		array[(System.Byte)color.black] =   new UnityEngine.Color((System.Single)0.0,(System.Single)0.0,(System.Single)0.0,(System.Single)1.0);
		array[(System.Byte)color.faint] =   new UnityEngine.Color((System.Single)0.1,(System.Single)0.1,(System.Single)0.1,(System.Single)0.1);
		array[(System.Byte)color.grey] =    new UnityEngine.Color((System.Single)0.4,(System.Single)0.4,(System.Single)0.4,(System.Single)1.0);
		array[(System.Byte)color.half] =    new UnityEngine.Color((System.Single)0.5,(System.Single)0.5,(System.Single)0.5,(System.Single)0.5);
		array[(System.Byte)color.cyan] =    new UnityEngine.Color((System.Single)0.0,(System.Single)1.0,(System.Single)1.0,(System.Single)1.0);
		array[(System.Byte)color.magenta] = new UnityEngine.Color((System.Single)1.0,(System.Single)0.0,(System.Single)1.0,(System.Single)1.0);
		array[(System.Byte)color.yellow] =  new UnityEngine.Color((System.Single)1.0,(System.Single)1.0,(System.Single)0.0,(System.Single)1.0);
		array[(System.Byte)color.red] =     new UnityEngine.Color((System.Single)1.0,(System.Single)0.0,(System.Single)0.0,(System.Single)1.0);
		array[(System.Byte)color.green] =   new UnityEngine.Color((System.Single)0.0,(System.Single)1.0,(System.Single)0.0,(System.Single)1.0);
		array[(System.Byte)color.blue] =    new UnityEngine.Color((System.Single)0.0,(System.Single)0.0,(System.Single)1.0,(System.Single)1.0);
	}
	static public UnityEngine.Color select (color import) {
		return array[(System.Byte)import];
	}
}