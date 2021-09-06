namespace AssemblyCSharp {
	public class prefab {
		public delegate void method (UnityEngine.GameObject GameObject);
		static public UnityEngine.Transform instantiate (method function,UnityEngine.Vector3 position,UnityEngine.Quaternion rotation,UnityEngine.Transform parent) {
			UnityEngine.GameObject box;
			UnityEngine.Transform data;
			box = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
			UnityEngine.Object.Destroy(box.GetComponent<UnityEngine.MeshFilter>());
			UnityEngine.Object.Destroy(box.GetComponent<UnityEngine.BoxCollider>());
			UnityEngine.Object.Destroy(box.GetComponent<UnityEngine.MeshRenderer>());
			function(box);
			data = box.GetComponent<UnityEngine.Transform>();
			data.position = position;
			data.rotation = rotation;
			data.transform.SetParent(parent,true);
			return data;
		}
		static public UnityEngine.RectTransform rect_transform (UnityEngine.GameObject box) {
			box.layer |= 0x5;
			UnityEngine.RectTransform rect_transform = box.AddComponent<UnityEngine.RectTransform>();
			rect_transform.anchoredPosition = new UnityEngine.Vector2(0x0,0x0);
			rect_transform.anchoredPosition3D = new UnityEngine.Vector3(0x0,0x0,0x0);
			rect_transform.anchorMax = new UnityEngine.Vector2(0x0,0x0);
			rect_transform.anchorMin = new UnityEngine.Vector2(0x0,0x0);
			rect_transform.offsetMax = new UnityEngine.Vector2(0x0,0x0);
			rect_transform.offsetMin = new UnityEngine.Vector2(0x0,0x0);
			rect_transform.pivot = new UnityEngine.Vector2(0x0,0x0);
			rect_transform.sizeDelta = new UnityEngine.Vector2(0x0,0x0);
			return rect_transform;
		}
		static public UnityEngine.CanvasRenderer canvas_renderer (UnityEngine.GameObject box) {
			UnityEngine.CanvasRenderer canvas_renderer = box.AddComponent<UnityEngine.CanvasRenderer>();
			return canvas_renderer;
		}

		static public void canvas (UnityEngine.GameObject page) {
			rect_transform(page);

			UnityEngine.Canvas canvas = page.AddComponent<UnityEngine.Canvas>();
			canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
			canvas.pixelPerfect = true;
			canvas.sortingOrder = 0;

			UnityEngine.UI.CanvasScaler canvasScaler = page.AddComponent<UnityEngine.UI.CanvasScaler>();
			canvasScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
			canvasScaler.scaleFactor = 1;
			canvasScaler.referencePixelsPerUnit = 100;

			UnityEngine.UI.GraphicRaycaster graphicRaycaster = page.AddComponent<UnityEngine.UI.GraphicRaycaster>();
			graphicRaycaster.ignoreReversedGraphics = true;
			graphicRaycaster.blockingObjects = UnityEngine.UI.GraphicRaycaster.BlockingObjects.None;
		}
		static public void image (UnityEngine.GameObject box) {
			UnityEngine.RectTransform RectTransform;
			UnityEngine.UI.RawImage RawImage;

			RectTransform = rect_transform(box);
			//RectTransform.sizeDelta = new UnityEngine.Vector2(UnityEngine.Screen.width,UnityEngine.Screen.height);
			RectTransform.sizeDelta = new UnityEngine.Vector2(0x800,0x800);

			canvas_renderer(box);

			RawImage = box.AddComponent<UnityEngine.UI.RawImage>();
			RawImage.texture = new UnityEngine.Texture2D(0x800,0x800,UnityEngine.TextureFormat.ARGB32,false,true);
			RawImage.uvRect = new UnityEngine.Rect(0.0f,0.0f,1.0f,1.0f);
		}
	}
}