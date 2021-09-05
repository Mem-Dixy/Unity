namespace EnTropy {
	public class TheAlignmentTags : UIDocument {
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
		private UnityEngine.UIElements.TextField textField;
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
			this.scoreLabel = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Label>(root, null, "score-number");
			this.scoreLabel.text = this.score.ToString();
			this.gameButtons = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Button>(root, null, "game-button").ToList();
			UnityEngine.UIElements.VisualElement gameBoard = UnityEngine.UIElements.UQueryExtensions.Q(root, null, "board");
			gameBoard.RegisterCallback<UnityEngine.UIElements.ClickEvent>(evt => {
				if (evt.target is UnityEngine.UIElements.Button targetButton && targetButton.ClassListContains(ActiveClassName)) {
					this.score += 1;
					this.scoreLabel.text = this.score.ToString();
					targetButton.RemoveFromClassList(ActiveClassName);
					evt.StopImmediatePropagation();
				}
			});
			root.styleSheets.Add(this.StyleSheet);
			_ = UnityEngine.UIElements.UQueryExtensions.Query(root, null, "disableRichText").ForEach(
				element => {
					return (element as UnityEngine.UIElements.Label).enableRichText = false;
				}
			);
			this.textField = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "texttalk");
			this.textField.RegisterCallback<UnityEngine.UIElements.BlurEvent>(BlurEvent => {
				this.textField.value += "!";
			});
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