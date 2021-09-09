namespace EnTropy {
	public class TheAlignmentTags : UIDocument {
		private enum GameState {
			Waiting,
			Active
		}
		private const System.String ActiveClassName = "game-button--active";
		private System.Collections.Generic.List<UnityEngine.UIElements.Button> gameButtons;
		private UnityEngine.UIElements.Button actionButton;
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

		UnityEngine.UIElements.TextField damage1;
		UnityEngine.UIElements.TextField damage2;
		UnityEngine.UIElements.TextField damage3;
		UnityEngine.UIElements.TextField range1;
		UnityEngine.UIElements.TextField range2;
		UnityEngine.UIElements.TextField range3;
		UnityEngine.UIElements.TextField life1;
		UnityEngine.UIElements.TextField life2;
		UnityEngine.UIElements.TextField life3;
		UnityEngine.UIElements.TextField income1;
		UnityEngine.UIElements.TextField income2;
		UnityEngine.UIElements.TextField income3;
		UnityEngine.UIElements.TextField rock1;
		UnityEngine.UIElements.TextField rock2;
		UnityEngine.UIElements.TextField rock3;

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
			this.actionButton = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.Button>(root, "action");
			this.actionButton.RegisterCallback<UnityEngine.UIElements.ClickEvent>(this.ActionClickMethod, UnityEngine.UIElements.TrickleDown.NoTrickleDown);
			this.damageLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "damage").ToList();
			this.rangeLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "range").ToList();
			this.lifeLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "life").ToList();
			this.incomeLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "income").ToList();
			this.rockLabels = UnityEngine.UIElements.UQueryExtensions.Query<UnityEngine.UIElements.Label>(root, null, "rock").ToList();

			this.damage1 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "damage-1");
			this.damage2 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "damage-2");
			this.damage3 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "damage-3");
			this.range1 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "range-1");
			this.range2 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "range-2");
			this.range3 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "range-3");
			this.life1 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "life-1");
			this.life2 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "life-2");
			this.life3 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "life-3");
			this.income1 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "income-1");
			this.income2 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "income-2");
			this.income3 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "income-3");
			this.rock1 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "rock-1");
			this.rock2 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "rock-2");
			this.rock3 = UnityEngine.UIElements.UQueryExtensions.Q<UnityEngine.UIElements.TextField>(root, "rock-3");
		}
		private void CalculateClickMethod(UnityEngine.UIElements.ClickEvent ClickEvent) {
			System.Object _ = ClickEvent.currentTarget;
			System.Int32 index = this.damageLabels.Count;
			Upgrade damage = new UpgradeDamage();
			damage.Convert(this.damage1.text, this.damage2.text, this.damage3.text);
			Upgrade range = new UpgradeRange();
			range.Convert(this.range1.text, this.range2.text, this.range3.text);
			Upgrade life = new UpgradeLife();
			life.Convert(this.life1.text, this.life2.text, this.life3.text);
			Upgrade income = new UpgradeIncome();
			income.Convert(this.income1.text, this.income2.text, this.income3.text);
			Upgrade rock = new UpgradeIncome();
			rock.Convert(this.rock1.text, this.rock2.text, this.rock3.text);
			while (index-- > 0) {
				this.damageLabels[index].text = damage.array[index].ToString();
			}
			index = this.rangeLabels.Count;
			while (index-- > 0) {
				this.rangeLabels[index].text = range.array[index].ToString();
			}
			index = life.array.Length;
			while (index-- > 0) {
				this.lifeLabels[index].text = life.array[index].ToString();
			}
			index = this.incomeLabels.Count;
			while (index-- > 0) {
				this.incomeLabels[index].text = income.array[index].ToString();
			}
			index = this.rockLabels.Count;
			while (index-- > 0) {
				this.rockLabels[index].text = rock.array[index].ToString();
			}
			this.textField.value += "!";
		}

		private void ActionClickMethod(UnityEngine.UIElements.ClickEvent ClickEvent) {
			UnityEngine.Debug.Log(">" + UnityEngine.Time.realtimeSinceStartupAsDouble);
			System.Int32 index = 1000000;
			Upgrade damage = new UpgradeDamage();
			while (index-- > 0) {
				damage.Convert(this.damage1.text, this.damage2.text, this.damage3.text);
			}
			UnityEngine.Debug.Log("<" + UnityEngine.Time.realtimeSinceStartupAsDouble);
		}

		private System.String UpgradeFormula(System.Int32 upgrade, System.String addition, System.String multiplication, System.String exponentiation) {
			_ = System.Single.TryParse(addition, out System.Single _addition);
			_ = System.Single.TryParse(multiplication, out System.Single _multiplication);
			_ = System.Single.TryParse(exponentiation, out System.Single _exponentiation);
			System.Double answer = System.Math.Pow((upgrade + _addition) * _multiplication, _exponentiation);
			System.String result = System.Math.Round(answer).ToString();
			return result;
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