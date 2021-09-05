namespace EnTropy {
	[UnityEngine.RequireComponent(typeof(UnityEngine.UIElements.UIDocument))]
	public abstract class UIDocument : UnityEngine.MonoBehaviour {
		protected UnityEngine.UIElements.PanelSettings PanelSettings;
		protected UnityEngine.UIElements.VisualTreeAsset VisualTreeAsset;
		[UnityEngine.SerializeField] protected UnityEngine.UIElements.StyleSheet StyleSheet;
		void Awake() {
			UnityEngine.Random.InitState(123);
			UnityEngine.UIElements.UIDocument UIDocument = this.GetComponent<UnityEngine.UIElements.UIDocument>();
			this.PanelSettings = UIDocument.panelSettings;
			this.VisualTreeAsset = UIDocument.visualTreeAsset;
		}
	}
}