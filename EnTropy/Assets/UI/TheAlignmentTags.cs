namespace EnTropy {
	public class TheAlignmentTags : UIDocument {
		private enum GameState {
			Waiting,
			Active
		}
		private const System.String ActiveClassName = "game-button--active";
		private System.Collections.Generic.List<UnityEngine.UIElements.Button> gameButtons;
		private UnityEngine.UIElements.Button calculateButton;
		public System.Collections.Generic.List<UnityEngine.UIElements.Label> damageLabels;
		public System.Collections.Generic.List<UnityEngine.UIElements.Label> rangeLabels;
		public System.Collections.Generic.List<UnityEngine.UIElements.Label> lifeLabels;
		public System.Collections.Generic.List<UnityEngine.UIElements.Label> incomeLabels;
		public System.Collections.Generic.List<UnityEngine.UIElements.Label> rockLabels;
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

			System.String words = "moo";
			this.textField.RegisterCallback<UnityEngine.UIElements.BlurEvent, System.String>(this.NewMethod4, words, UnityEngine.UIElements.TrickleDown.NoTrickleDown);
			this.textField.RegisterCallback<UnityEngine.UIElements.BlurEvent>(this.NewMethod1, UnityEngine.UIElements.TrickleDown.NoTrickleDown);

			this.calculateButton = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(root, "update");
			this.calculateButton.RegisterCallback<UnityEngine.UIElements.ClickEvent>(this.CalculateClickMethod, UnityEngine.UIElements.TrickleDown.NoTrickleDown);
			this.damageLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "damage").ToList();
			this.rangeLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "range").ToList();
			this.lifeLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "life").ToList();
			this.incomeLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "income").ToList();
			this.rockLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "rock").ToList();

		}
		private void CalculateClickMethod(UnityEngine.UIElements.ClickEvent ClickEvent) {
			System.Object _ = ClickEvent.currentTarget;
			System.Int32 index = this.damageLabels.Count;
			while (index-- > 0) {
				this.damageLabels[index].text = index.ToString();
			}
			index = this.rangeLabels.Count;
			while (index-- > 0) {
				this.rangeLabels[index].text = index.ToString();
			}
			index = this.lifeLabels.Count;
			while (index-- > 0) {
				this.lifeLabels[index].text = index.ToString();
			}
			index = this.incomeLabels.Count;
			while (index-- > 0) {
				this.incomeLabels[index].text = index.ToString();
			}
			index = this.rockLabels.Count;
			while (index-- > 0) {
				this.rockLabels[index].text = index.ToString();
			}
			UnityEngine.Debug.Log("Click");
			this.textField.value += "!";
		}
		private void NewMethod1(UnityEngine.UIElements.BlurEvent BlurEvent) {
			System.Object _ = BlurEvent.currentTarget;
			this.textField.value += "!";
		}

		private void NewMethod4(UnityEngine.UIElements.BlurEvent BlurEvent, System.String words) {
			System.Object _ = BlurEvent.currentTarget;
			this.textField.value += "!";
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
/*
4	explodeing	317		0
5	explode 	343		0
6	strong 		120		0
7	armoring	144		16
8	unarmor		1553	-28
9	explode		1045	0
10	exlpode		??		0
11	strongling	662
12	armoring		873	46
13	unormr	9304	0
explode 7670
strong 2980
*/