namespace game {
	public class Gamepad : UnityEngine.MonoBehaviour {
		[UnityEngine.SerializeField]
		private UnityEngine.Vector2 leftStickDeadZone;
		[UnityEngine.SerializeField]
		private UnityEngine.Vector2 rightStickDeadZone;
		private System.Single leftStickDeadZoneSqrMagnitude;
		private System.Single rightStickDeadZoneSqrMagnitude;

		public UnityEngine.Vector2 leftStickValue;
		public UnityEngine.Vector2 rightStickValue;
		public System.Boolean leftStickIsOn;
		public System.Boolean rightStickIsOn;
		public System.Boolean triangleButtonIsOn;

		private UnityEngine.InputSystem.Gamepad gamepad;

		public void Awake() {
			this.leftStickDeadZone = this.Normalize(this.leftStickDeadZone);
			this.rightStickDeadZone = this.Normalize(this.rightStickDeadZone);
			this.leftStickDeadZoneSqrMagnitude = this.leftStickDeadZone.sqrMagnitude;
			this.rightStickDeadZoneSqrMagnitude = this.rightStickDeadZone.sqrMagnitude;

			this.gamepad = UnityEngine.InputSystem.Gamepad.current;
		}

		public void Update() {
			UnityEngine.Vector2 leftStick = this.gamepad.leftStick.ReadValue();
			UnityEngine.Vector2 rightStick = this.gamepad.rightStick.ReadValue();

			this.leftStickIsOn = leftStick.sqrMagnitude >= this.leftStickDeadZoneSqrMagnitude;
			this.rightStickIsOn = rightStick.sqrMagnitude >= this.rightStickDeadZoneSqrMagnitude;
			this.triangleButtonIsOn = this.gamepad.triangleButton.isPressed;

			this.leftStickValue = this.leftStickIsOn ? leftStick : UnityEngine.Vector2.zero;
			this.rightStickValue = this.rightStickIsOn ? rightStick : UnityEngine.Vector2.zero;

		}
		private System.Single Normalize(System.Single f) {
			System.Single value = UnityEngine.Mathf.Abs(f);
			System.Single result = UnityEngine.Mathf.Clamp01(value);
			return result;
		}
		private UnityEngine.Vector2 Normalize(UnityEngine.Vector2 Vector2) {
			System.Single x = this.Normalize(Vector2.x);
			System.Single y = this.Normalize(Vector2.y);
			UnityEngine.Vector2 result = new UnityEngine.Vector2(x, y);
			return result;
		}
	}
}