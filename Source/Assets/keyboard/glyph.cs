namespace keyboard {
	abstract public class glyph {


		/*
		Backspace The backspace key.
		Delete The forward delete key. 
		Tab The tab key. 
		Clear The Clear key. 
		Return Return key.
		Pause Pause on PC machines.
		Escape Escape key.
		Space Space key.
		Keypad0 Numeric keypad 0. 
		Keypad1 Numeric keypad 1. 
		Keypad2 Numeric keypad 2. 
		Keypad3 Numeric keypad 3. 
		Keypad4 Numeric keypad 4. 
		Keypad5 Numeric keypad 5. 
		Keypad6 Numeric keypad 6. 
		Keypad7 Numeric keypad 7. 
		Keypad8 Numeric keypad 8. 
		Keypad9 Numeric keypad 9. 
		KeypadPeriod Numeric keypad '.'. 
		KeypadDivide Numeric keypad '/'. 
		KeypadMultiply Numeric keypad '*'. 
		KeypadMinus Numeric keypad '-'. 
		KeypadPlus Numeric keypad '+'. 
		KeypadEnter Numeric keypad enter. 
		KeypadEquals Numeric keypad ' { get;  }'. 
		UpArrow Up arrow key. 
		DownArrow Down arrow key. 
		RightArrow Right arrow key. 
		LeftArrow Left arrow key. 
		Insert Insert key key. 
		Home Home key.
		End End key.
		PageUp Page up.
		PageDown Page down.
		F1 F1 function key. 
		F2 F2 function key. 
		F3 F3 function key. 
		F4 F4 function key. 
		F5 F5 function key. 
		F6 F6 function key. 
		F7 F7 function key. 
		F8 F8 function key. 
		F9 F9 function key. 
		F10 F10 function key. 
		F11 F11 function key. 
		F12 F12 function key. 
		F13 F13 function key. 
		F14 F14 function key. 
		F15 F15 function key. 
		Alpha0 The '0' key on the top of the alphanumeric keyboard. 
		Alpha1 The '1' key on the top of the alphanumeric keyboard. 
		Alpha2 The '2' key on the top of the alphanumeric keyboard. 
		Alpha3 The '3' key on the top of the alphanumeric keyboard. 
		Alpha4 The '4' key on the top of the alphanumeric keyboard. 
		Alpha5 The '5' key on the top of the alphanumeric keyboard. 
		Alpha6 The '6' key on the top of the alphanumeric keyboard. 
		Alpha7 The '7' key on the top of the alphanumeric keyboard. 
		Alpha8 The '8' key on the top of the alphanumeric keyboard. 
		Alpha9 The '9' key on the top of the alphanumeric keyboard. 
		Exclaim Exclamation mark key '!'. 
		DoubleQuote Double quote key '"'. 
		Hash Hash key '#'. 
		Dollar Dollar sign key '$'. 
		Ampersand Ampersand key '&'. 
		Quote Quote key '. 
		LeftParen Left Parenthesis key '('. 
		RightParen Right Parenthesis key ')'. 
		Asterisk Asterisk key '*'. 
		Plus Plus key '+'. 
		Comma Comma ',' key.
		Minus Minus '-' key.
		Period Period '.' key.
		Slash Slash '/' key.
		Colon Colon ':' key.
		Semicolon Semicolon ';' key.
		Less Less than '<' key.
		Equals Equals ' { get;  }' key.
		Greater Greater than '>' key.
		Question Question mark '?' key.
		At At key '@'. 
		LeftBracket Left square bracket key '['. 
		Backslash Backslash key '\'. 
		RightBracket Right square bracket key ']'. 
		Caret Caret key '^'. 
		Underscore Underscore '_' key.
		BackQuote Back quote key '`'. 
		A 'a' key.
		B 'b' key.
		C 'c' key.
		D 'd' key.
		E 'e' key.
		F 'f' key.
		G 'g' key.
		H 'h' key.
		I 'i' key.
		J 'j' key.
		K 'k' key.
		L 'l' key.
		M 'm' key.
		N 'n' key.
		O 'o' key.
		P 'p' key.
		Q 'q' key.
		R 'r' key.
		S 's' key.
		T 't' key.
		U 'u' key.
		V 'v' key.
		W 'w' key.
		X 'x' key.
		Y 'y' key.
		Z 'z' key.
		Numlock Numlock key.
		CapsLock Capslock key.
		ScrollLock Scroll lock key.
		RightShift Right shift key. 
		LeftShift Left shift key. 
		RightControl Right Control key. 
		LeftControl Left Control key. 
		RightAlt Right Alt key. 
		LeftAlt Left Alt key. 
		LeftCommand Left Command key. 
		LeftApple Left Command key. 
		LeftWindows Left Windows key. 
		RightCommand Right Command key. 
		RightApple Right Command key. 
		RightWindows Right Windows key. 
		AltGr Alt Gr key. 
		Help Help key.
		Print Print key.
		SysReq Sys Req key. 
		Break Break key.
		Menu Menu key.
		*/


		/*
Space Space key.
Keypad0 Numeric keypad 0. 
Keypad1 Numeric keypad 1. 
Keypad2 Numeric keypad 2. 
Keypad3 Numeric keypad 3. 
Keypad4 Numeric keypad 4. 
Keypad5 Numeric keypad 5. 
Keypad6 Numeric keypad 6. 
Keypad7 Numeric keypad 7. 
Keypad8 Numeric keypad 8. 
Keypad9 Numeric keypad 9. 
KeypadPeriod Numeric keypad '.'. 
KeypadDivide Numeric keypad '/'. 
KeypadMultiply Numeric keypad '*'. 
KeypadMinus Numeric keypad '-'. 
KeypadPlus Numeric keypad '+'. 
KeypadEnter Numeric keypad enter. 
KeypadEquals Numeric keypad ' { get;  }'. 

			Alpha0 The '0' key on the top of the alphanumeric keyboard. 
Alpha1 The '1' key on the top of the alphanumeric keyboard. 
Alpha2 The '2' key on the top of the alphanumeric keyboard. 
Alpha3 The '3' key on the top of the alphanumeric keyboard. 
Alpha4 The '4' key on the top of the alphanumeric keyboard. 
Alpha5 The '5' key on the top of the alphanumeric keyboard. 
Alpha6 The '6' key on the top of the alphanumeric keyboard. 
Alpha7 The '7' key on the top of the alphanumeric keyboard. 
Alpha8 The '8' key on the top of the alphanumeric keyboard. 
Alpha9 The '9' key on the top of the alphanumeric keyboard. 
Exclaim Exclamation mark key '!'. 
DoubleQuote Double quote key '"'. 
Hash Hash key '#'. 
Dollar Dollar sign key '$'. 
Ampersand Ampersand key '&'. 
Quote Quote key '. 
LeftParen Left Parenthesis key '('. 
RightParen Right Parenthesis key ')'. 
Asterisk Asterisk key '*'. 
Plus Plus key '+'. 
Comma Comma ',' key.
Minus Minus '-' key.
Period Period '.' key.
Slash Slash '/' key.
Colon Colon ':' key.
Semicolon Semicolon ';' key.
Less Less than '<' key.
Equals Equals ' { get;  }' key.
Greater Greater than '>' key.
Question Question mark '?' key.
At At key '@'. 
LeftBracket Left square bracket key '['. 
Backslash Backslash key '\'. 
RightBracket Right square bracket key ']'. 
Caret Caret key '^'. 
Underscore Underscore '_' key.
BackQuote Back quote key '`'. 
A 'a' key.
B 'b' key.
C 'c' key.
D 'd' key.
E 'e' key.
F 'f' key.
G 'g' key.
H 'h' key.
I 'i' key.
J 'j' key.
K 'k' key.
L 'l' key.
M 'm' key.
N 'n' key.
O 'o' key.
P 'p' key.
Q 'q' key.
R 'r' key.
S 's' key.
T 't' key.
U 'u' key.
V 'v' key.
W 'w' key.
X 'x' key.
Y 'y' key.
Z 'z' key.
*/








		abstract public System.UInt64 asterisk { get; }
		abstract public System.UInt64 bracket_angle_left { get; }
		abstract public System.UInt64 bracket_angle_right { get; }
		abstract public System.UInt64 bracket_curly_left { get; }
		abstract public System.UInt64 bracket_curly_right { get; }
		abstract public System.UInt64 bracket_round_left { get; }
		abstract public System.UInt64 bracket_round_right { get; }
		abstract public System.UInt64 bracket_square_left { get; }
		abstract public System.UInt64 bracket_square_right { get; }

		abstract public System.Char delete { get; }
		abstract public System.Char left { get; }
		abstract public System.Char right { get; }
		abstract public System.Char up { get; }
		abstract public System.Char down { get; }
		abstract public System.Char enter { get; }
		abstract public System.Char error { get; }


		abstract public System.UInt64 del { get; }


		abstract public System.UInt64 exclamation { get; }
		abstract public System.UInt64 pipe { get; }
		abstract public System.UInt64 carrot { get; }
		abstract public System.UInt64 slash { get; }
		abstract public System.UInt64 backslash { get; }
		abstract public System.UInt64 plus { get; }
		abstract public System.UInt64 minus { get; }
		abstract public System.UInt64 equals { get; }
		abstract public System.UInt64 octothorpe { get; }
		abstract public System.UInt64 percent { get; }
		abstract public System.UInt64 cursor { get; }
		abstract public System.UInt64 blank { get; }
		abstract public System.UInt64 space { get; }
		abstract public System.UInt64 zero { get; }
		abstract public System.UInt64 one { get; }
		abstract public System.UInt64 two { get; }
		abstract public System.UInt64 three { get; }
		abstract public System.UInt64 four { get; }
		abstract public System.UInt64 five { get; }
		abstract public System.UInt64 six { get; }
		abstract public System.UInt64 seven { get; }
		abstract public System.UInt64 eight { get; }
		abstract public System.UInt64 nine { get; }
		abstract public System.UInt64 ten { get; }
		abstract public System.UInt64 eleven { get; }
		abstract public System.UInt64 twelve { get; }
		abstract public System.UInt64 thirteen { get; }
		abstract public System.UInt64 fourteen { get; }
		abstract public System.UInt64 fifteen { get; }

		abstract public System.UInt64 A { get; }
		abstract public System.UInt64 B { get; }
		abstract public System.UInt64 C { get; }
		abstract public System.UInt64 D { get; }
		abstract public System.UInt64 E { get; }
		abstract public System.UInt64 F { get; }
		abstract public System.UInt64 G { get; }
		abstract public System.UInt64 H { get; }
		abstract public System.UInt64 I { get; }
		abstract public System.UInt64 J { get; }
		abstract public System.UInt64 K { get; }
		abstract public System.UInt64 L { get; }
		abstract public System.UInt64 M { get; }
		abstract public System.UInt64 N { get; }
		abstract public System.UInt64 O { get; }
		abstract public System.UInt64 P { get; }
		abstract public System.UInt64 Q { get; }
		abstract public System.UInt64 R { get; }
		abstract public System.UInt64 S { get; }
		abstract public System.UInt64 T { get; }
		abstract public System.UInt64 U { get; }
		abstract public System.UInt64 V { get; }
		abstract public System.UInt64 W { get; }
		abstract public System.UInt64 X { get; }
		abstract public System.UInt64 Y { get; }
		abstract public System.UInt64 Z { get; }
		abstract public System.UInt64 a { get; }
		abstract public System.UInt64 b { get; }
		abstract public System.UInt64 c { get; }
		abstract public System.UInt64 d { get; }
		abstract public System.UInt64 e { get; }
		abstract public System.UInt64 f { get; }
		abstract public System.UInt64 g { get; }
		abstract public System.UInt64 h { get; }
		abstract public System.UInt64 i { get; }
		abstract public System.UInt64 j { get; }
		abstract public System.UInt64 k { get; }
		abstract public System.UInt64 l { get; }
		abstract public System.UInt64 m { get; }
		abstract public System.UInt64 n { get; }
		abstract public System.UInt64 o { get; }
		abstract public System.UInt64 p { get; }
		abstract public System.UInt64 q { get; }
		abstract public System.UInt64 r { get; }
		abstract public System.UInt64 s { get; }
		abstract public System.UInt64 t { get; }
		abstract public System.UInt64 u { get; }
		abstract public System.UInt64 v { get; }
		abstract public System.UInt64 w { get; }
		abstract public System.UInt64 x { get; }
		abstract public System.UInt64 y { get; }
		abstract public System.UInt64 z { get; }
	}
}
