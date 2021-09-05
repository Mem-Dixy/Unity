using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

using Random = UnityEngine.Random;
namespace EnTropy {
	[RequireComponent(typeof(UIDocument))]
	public class TheAlignmentTags : MonoBehaviour {
		[SerializeField] private PanelSettings panelSettings = default;
		[SerializeField] private VisualTreeAsset sourceAsset = default;
		[SerializeField] private StyleSheet styleAsset = default;
		void Awake() {
			UIDocument uiDocument = this.GetComponent<UIDocument>();
			uiDocument.panelSettings = this.panelSettings;
			uiDocument.visualTreeAsset = this.sourceAsset;
		}

		private enum GameState {
			Waiting,
			Active
		}

		private const System.String ActiveClassName = "game-button--active";


		private List<Button> gameButtons;
		private Label scoreLabel;

		private GameState currentState = GameState.Waiting;
		private System.Int32 activeButtonIndex = -1;
		private System.Single delay = 3f;
		private System.Int32 score;

		public void SetPanelSettings(PanelSettings newPanelSettings) {
			this.panelSettings = newPanelSettings;
			UIDocument uiDocument = this.GetComponent<UIDocument>();
			uiDocument.panelSettings = this.panelSettings;
		}


		void OnEnable() {
			if (this.scoreLabel == null) {
				this.score = 0;
				//After a domain reload, we need to re-cache our VisualElements and hook our callbacks
				this.InitializeVisualTree(this.GetComponent<UIDocument>());
			}
		}

		private void InitializeVisualTree(UIDocument doc) {
			VisualElement root = doc.rootVisualElement;

			this.scoreLabel = root.Q<Label>(className: "score-number");
			this.scoreLabel.text = this.score.ToString();

			this.gameButtons = root.Query<Button>(className: "game-button").ToList();
			VisualElement gameBoard = root.Q<VisualElement>(className: "board");
			gameBoard.RegisterCallback<ClickEvent>(evt => {
				if (evt.target is Button targetButton && targetButton.ClassListContains(ActiveClassName)) {
					this.score += 1;
					this.scoreLabel.text = this.score.ToString();
					targetButton.RemoveFromClassList(ActiveClassName);
					evt.StopImmediatePropagation();
				}
			});
			root.styleSheets.Add(this.styleAsset);

			_ = root.Query(className: "disableRichText").ForEach(
				element => {
					return (element as Label).enableRichText = false;
				}
			);

		}

		void Update() {
			this.delay -= Time.deltaTime;
			if (this.delay < 0f) {
				switch (this.currentState) {
					case GameState.Waiting:
					this.activeButtonIndex = Random.Range(0, this.gameButtons.Count);
					this.gameButtons[this.activeButtonIndex].AddToClassList(ActiveClassName);
					this.currentState = GameState.Active;
					this.delay = Random.Range(0.5f, 2f);
					break;
					case GameState.Active:
					this.gameButtons[this.activeButtonIndex].RemoveFromClassList(ActiveClassName);
					this.currentState = GameState.Waiting;
					this.delay = Random.Range(1f, 4f);
					break;
				}
			}
		}
	}
}