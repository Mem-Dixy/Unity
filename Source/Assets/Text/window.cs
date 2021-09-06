public struct window {
/*
	private  UnityEngine.UI.Text[] show;
	public window (screen data , nomad.point offset) {
		UnityEngine.Transform page;
		UnityEngine.Transform work;
		nomad.point many = new nomad.point((System.UInt16)(data.resolution.x / (data.splittings.x * prefab.text_size.x)) , (System.UInt16)(data.resolution.y / (data.splittings.y * prefab.text_size.y)));
		UnityEngine.Debug.Log(many.x+"<"+many.y);
		System.UInt32 length = (System.UInt32)(many.x * many.y);

		page = prefab.instantiate(prefab.canvas , UnityEngine.Vector3.zero , UnityEngine.Quaternion.identity , null);
		//UnityEngine.Canvas canvas = page.GetComponent<UnityEngine.Canvas>();
		show = new UnityEngine.UI.Text[length];

		nomad.point spot = new nomad.point(cast.u2(0x0) , cast.u2(0x0));
		nomad.point compute = new nomad.point(cast.u2(0x0) , cast.u2(0x0));
		System.UInt32 index = length;
		while (index > 0) {
			index -= 1;
			spot.x = (System.UInt16)(index % many.x);
			spot.y = (System.UInt16)(index / many.x);
			//compute = prefab.text_size * (0x2 * spot + many * (0x2 * offset + data.splittings));
			//work=prefab.instantiate(prefab.text , new UnityEngine.Vector3(compute.x/2.0f-data.resolution.x , compute.y/2.0f-data.resolution.y , 0x0) , UnityEngine.Quaternion.identity , page);
			compute=spot*prefab.text_size+(offset*many*prefab.text_size);
			work=prefab.instantiate(prefab.text , new UnityEngine.Vector3(compute.x-data.resolution.x/2.0f , compute.y-data.resolution.y/2.0f , 0x0) , UnityEngine.Quaternion.identity , page);

			show[index]=work.GetComponent<UnityEngine.UI.Text>();
		}
	}
	public void set (System.UInt16 index , System.UInt32 import) {
		show[index].text = System.Char.ConvertFromUtf32((System.Int32)(import));
	}
	*/
}