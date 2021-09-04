using UnityEngine.UIElements;
using UnityEngine;

namespace Samples.Runtime.Text {
	[RequireComponent(typeof(UIDocument))]
	public class AlignmentTags : MonoBehaviour {
		[SerializeField] private PanelSettings panelSettings = default;
		[SerializeField] private VisualTreeAsset sourceAsset = default;
		[SerializeField] private StyleSheet styleAsset = default;
		void Awake() {
			UIDocument uiDocument = this.GetComponent<UIDocument>();
			uiDocument.panelSettings = this.panelSettings;
			uiDocument.visualTreeAsset = this.sourceAsset;
		}
		void OnEnable() {
			this.InitializeVisualTree(this.GetComponent<UIDocument>());
		}
		private void InitializeVisualTree(UIDocument uiDocument) {
			VisualElement root = uiDocument.rootVisualElement;
			root.styleSheets.Add(this.styleAsset);

			_ = root.Query(className: "disableRichText").ForEach(
				element => {
					return (element as Label).enableRichText = false;
				});
		}
	}
}