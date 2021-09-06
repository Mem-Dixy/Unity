namespace keyboard {
	static public class keyboarding {
		static private System.Collections.Generic.Queue<System.UInt64> onkeydown;
		static private System.Collections.Generic.Queue<System.UInt64> onkeypress;
		static private System.Collections.Generic.Queue<System.UInt64> onkeyup;
		static System.Boolean caps_lock = false;
		static System.Boolean numlock = true;
		static public System.UInt64 type () {
			if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.CapsLock)) {
				caps_lock = !caps_lock;
				UnityEngine.Debug.Log("push");
			}
			if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Numlock)) {
				numlock = !numlock;
				UnityEngine.Debug.Log("batt");
			}
			if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.KeypadEnter)) {
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad0)) {
					return alias.space;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad0)) {
					return '0';
				}

				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad0)) {
					return alias.space;
				}

				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad1)) {
					return keyboard.alias.ten;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad2)) {
					return keyboard.alias.eleven;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad3)) {
					return keyboard.alias.twelve;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad4)) {
					return keyboard.alias.thirteen;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad5)) {
					return keyboard.alias.fourteen;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad6)) {
					return keyboard.alias.fifteen;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad7)) {
					return '|';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad8)) {
					return '&';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad9)) {
					return '^';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadPeriod)) {
					return alias.delete;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadDivide)) {
					return '%';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadMultiply)) {
					return 'x';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadMinus)) {
					return ';';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadPlus)) {
					return alias.enter;
				}
			}
			else {
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad0)) {
					return keyboard.alias.zero;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad1)) {
					return keyboard.alias.one;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad2)) {
					return keyboard.alias.two;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad3)) {
					return keyboard.alias.three;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad4)) {
					return keyboard.alias.four;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad5)) {
					return keyboard.alias.five;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad6)) {
					return keyboard.alias.six;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad7)) {
					return keyboard.alias.seven;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad8)) {
					return keyboard.alias.eight;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Keypad9)) {
					return keyboard.alias.nine;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadPeriod)) {
					return '.';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadDivide)) {
					return '/';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadMultiply)) {
					return '*';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadMinus)) {
					return '-';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.KeypadPlus)) {
					return '+';
				}
			}
			if ((!caps_lock && (UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightShift))) || (caps_lock && !(UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftShift) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightShift)))) {
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.A)) {
					return keyboard.alias.A;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.B)) {
					return keyboard.alias.B;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.C)) {
					return keyboard.alias.C;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.D)) {
					return keyboard.alias.D;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.E)) {
					return keyboard.alias.E;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F)) {
					return keyboard.alias.F;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.G)) {
					return keyboard.alias.G;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.H)) {
					return keyboard.alias.H;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.I)) {
					return keyboard.alias.I;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.J)) {
					return keyboard.alias.J;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.K)) {
					return keyboard.alias.K;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.L)) {
					return keyboard.alias.L;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.M)) {
					return keyboard.alias.M;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.N)) {
					return keyboard.alias.N;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.O)) {
					return keyboard.alias.O;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.P)) {
					return keyboard.alias.P;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Q)) {
					return keyboard.alias.Q;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.R)) {
					return keyboard.alias.R;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.S)) {
					return keyboard.alias.S;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.T)) {
					return keyboard.alias.T;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.U)) {
					return keyboard.alias.U;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.V)) {
					return keyboard.alias.V;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.W)) {
					return keyboard.alias.W;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.X)) {
					return keyboard.alias.X;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Y)) {
					return keyboard.alias.Y;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Z)) {
					return keyboard.alias.Z;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha0)) {
					return keyboard.alias.bracket_round_right;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha1)) {
					return keyboard.alias.exclamation;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha2)) {
					return '2';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha3)) {
					return keyboard.alias.octothorpe;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha4)) {
					return '4';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha5)) {
					return keyboard.alias.percent;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha6)) {
					return keyboard.alias.carrot;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha7)) {
					return '7';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha8)) {
					return keyboard.alias.asterisk;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha9)) {
					return keyboard.alias.bracket_round_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.BackQuote)) {
					return '~';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftBracket)) {
					return keyboard.alias.bracket_curly_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightBracket)) {
					return keyboard.alias.bracket_curly_right;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Quote)) {
					return '"';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Comma)) {
					return keyboard.alias.bracket_angle_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Period)) {
					return keyboard.alias.bracket_angle_right;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Slash)) {
					return '?';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Equals)) {
					return keyboard.alias.plus;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Backslash)) {
					return keyboard.alias.pipe;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Minus)) {
					return '_';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Semicolon)) {
					return ':';
				}
			}
			else {
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.A)) {
					return keyboard.alias.a;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.B)) {
					return keyboard.alias.b;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.C)) {
					return keyboard.alias.c;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.D)) {
					return keyboard.alias.d;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.E)) {
					return keyboard.alias.e;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.F)) {
					return keyboard.alias.f;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.G)) {
					return keyboard.alias.g;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.H)) {
					return keyboard.alias.h;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.I)) {
					return keyboard.alias.i;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.J)) {
					return keyboard.alias.j;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.K)) {
					return keyboard.alias.k;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.L)) {
					return keyboard.alias.l;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.M)) {
					return keyboard.alias.m;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.N)) {
					return keyboard.alias.n;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.O)) {
					return keyboard.alias.o;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.P)) {
					return keyboard.alias.p;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Q)) {
					return keyboard.alias.q;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.R)) {
					return keyboard.alias.r;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.S)) {
					return keyboard.alias.s;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.T)) {
					return keyboard.alias.t;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.U)) {
					return keyboard.alias.u;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.V)) {
					return keyboard.alias.v;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.W)) {
					return keyboard.alias.w;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.X)) {
					return keyboard.alias.x;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Y)) {
					return keyboard.alias.y;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Z)) {
					return keyboard.alias.z;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha0)) {
					return '0';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha1)) {
					return keyboard.alias.slash;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha2)) {
					return keyboard.alias.bracket_curly_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha3)) {
					return keyboard.alias.bracket_square_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha4)) {
					return keyboard.alias.bracket_round_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha5)) {
					return keyboard.alias.bracket_angle_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha6)) {
					return '6';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha7)) {
					return '7';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha8)) {
					return '8';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Alpha9)) {
					return '9';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.BackQuote)) {
					return '`';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftBracket)) {
					return keyboard.alias.bracket_square_left;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightBracket)) {
					return keyboard.alias.bracket_square_right;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Quote)) {
					return '\'';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Comma)) {
					return ',';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Period)) {
					return '.';
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Slash)) {
					return keyboard.alias.slash;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Equals)) {
					return keyboard.alias.equals;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Backslash)) {
					return keyboard.alias.backslash;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Minus)) {
					return keyboard.alias.minus;
				}
				if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Semicolon)) {
					return ';';
				}
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Backspace)) {
				return alias.delete;
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Space)) {
				return alias.space;
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow)) {
				return alias.left;
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow)) {
				return alias.right;
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.DownArrow)) {
				return alias.down;
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow)) {
				return alias.up;
			}
			if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.Return)) {
				return alias.enter;
			}
			return alias.error;
		}
	}
}