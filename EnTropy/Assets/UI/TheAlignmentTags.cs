namespace EnTropy {
	[UnityEngine.RequireComponent(typeof(UnityEngine.UIElements.UIDocument))]
	public class TheAlignmentTags : UnityEngine.MonoBehaviour {
		[UnityEngine.SerializeField] private UnityEngine.UIElements.PanelSettings PanelSettings = default;
		[UnityEngine.SerializeField] private UnityEngine.UIElements.VisualTreeAsset VisualTreeAsset = default;
		[UnityEngine.SerializeField] private UnityEngine.UIElements.StyleSheet StyleSheet = default;
		void Awake() {
			UnityEngine.UIElements.UIDocument uiDocument = this.GetComponent<UnityEngine.UIElements.UIDocument>();
			uiDocument.panelSettings = this.PanelSettings;
			uiDocument.visualTreeAsset = this.VisualTreeAsset;
		}
		private enum GameState {
			Waiting,
			Active
		}
		private const System.String ActiveClassName = "game-button--active";
		private System.Collections.Generic.List<UnityEngine.UIElements.Button> gameButtons;
		private UnityEngine.UIElements.Label scoreLabel;
		private GameState currentState = GameState.Waiting;
		private System.Int32 activeButtonIndex = -1;
		private System.Single delay = 3f;
		private System.Int32 score;
		public void SetPanelSettings(UnityEngine.UIElements.PanelSettings PanelSettings) {
			this.PanelSettings = PanelSettings;
			UnityEngine.UIElements.UIDocument uiDocument = this.GetComponent<UnityEngine.UIElements.UIDocument>();
			uiDocument.panelSettings = this.PanelSettings;
		}
		void OnEnable() {
			if (this.scoreLabel == null) {
				this.score = 0;
				//After a domain reload, we need to re-cache our VisualElements and hook our callbacks
				this.InitializeVisualTree(this.GetComponent<UnityEngine.UIElements.UIDocument>());
			}
		}
		private void InitializeVisualTree(UnityEngine.UIElements.UIDocument UIDocument) {
			UnityEngine.UIElements.VisualElement root = UIDocument.rootVisualElement;
			this.scoreLabel = Root<UnityEngine.UIElements.Label>.Q(root, className: "score-number");
			this.scoreLabel.text = this.score.ToString();
			this.gameButtons = Root<UnityEngine.UIElements.Button>.Query(root, className: "game-button").ToList();
			UnityEngine.UIElements.VisualElement gameBoard = Root<UnityEngine.UIElements.VisualElement>.Q(root, className: "board");
			gameBoard.RegisterCallback<UnityEngine.UIElements.ClickEvent>(evt => {
				if (evt.target is UnityEngine.UIElements.Button targetButton && targetButton.ClassListContains(ActiveClassName)) {
					this.score += 1;
					this.scoreLabel.text = this.score.ToString();
					targetButton.RemoveFromClassList(ActiveClassName);
					evt.StopImmediatePropagation();
				}
			});
			root.styleSheets.Add(this.StyleSheet);
			_ = Root<UnityEngine.UIElements.VisualElement>.Query(root, className: "disableRichText").ForEach(
				element => {
					return (element as Label).enableRichText = false;
				}
			);
		}
		void Update() {
			this.delay -= UnityEngine.Time.deltaTime;
			if (this.delay < 0f) {
				switch (this.currentState) {
					case GameState.Waiting:
					this.activeButtonIndex = UnityEngine.Random.Range(0, this.gameButtons.Count);
					this.gameButtons[this.activeButtonIndex].AddToClassList(ActiveClassName);
					this.currentState = GameState.Active;
					this.delay = UnityEngine.Random.Range(0.5f, 2f);
					break;
					case GameState.Active:
					this.gameButtons[this.activeButtonIndex].RemoveFromClassList(ActiveClassName);
					this.currentState = GameState.Waiting;
					this.delay = UnityEngine.Random.Range(1f, 4f);
					break;
				}
			}
		}
	}
}