namespace AssemblyCSharp {
	public class screen {
		public System.Byte where_x;
		public System.Byte where_y;
		private UnityEngine.Texture2D texture;
		private UnityEngine.Color[] pretty;
		private UnityEngine.Color[] bigger;
		public screen () {
			where_x = 0x0;
			where_y = 0x0;
			// path=new directory("C:\\Users\\Clifford\\source\\");
			//
			UnityEngine.Transform page;
			page = prefab.instantiate(prefab.canvas,UnityEngine.Vector3.zero,UnityEngine.Quaternion.identity,null);
			page = prefab.instantiate(prefab.image,new UnityEngine.Vector3(-UnityEngine.Screen.width / 2.0f,-UnityEngine.Screen.height / 2.0f,0x0),UnityEngine.Quaternion.identity,page);
			texture = page.GetComponent<UnityEngine.UI.RawImage>().texture as UnityEngine.Texture2D;
			pretty = new UnityEngine.Color[0x400000];
			bigger = new UnityEngine.Color[0x400000];
			int y = 0;
			while (y < texture.height) {
				int x = 0;
				while (x < texture.width) {
					UnityEngine.Color color;
					if ((x & y) != 0) {
						color = UnityEngine.Color.white;
					}
					else {
						color = UnityEngine.Color.gray;
					}
					pretty[x + y * texture.width] = color;
					++x;
				}
				++y;
			}
			//
			//System.String url = "file:///C:\\Users\\Clifford\\source\\Pictures\\Screen shot 2010-04-22 at 15.07.54 .png";
			//
			System.String url = "file:///C:\\Users\\Clifford\\Desktop\\pov\\2048_360.png"; // -x -y
			//System.String url = "file:///C:\\Users\\Clifford\\Desktop\\pov\\z0.png"; // -x -y
																					 //System.String url = "file:///C:\\Users\\Clifford\\Desktop\\pov\\z1b.png"; // +x -y
																					 //System.String url = "file:///C:\\Users\\Clifford\\Desktop\\pov\\z2.png"; // -x +y
																					 //System.String url = "file:///C:\\Users\\Clifford\\Desktop\\pov\\z3.png"; // +x +y
			load_image(url);
		}
		private void load_image (System.String url) {
			UnityEngine.Texture2D Texture2D;
			UnityEngine.Color[] Color;
			System.UInt32 width;
			System.UInt32 height;
			System.UInt32 index;
			Texture2D = new UnityEngine.WWW(url).texture;
			Color = Texture2D.GetPixels();
			width = (System.UInt32)(Texture2D.width);
			height = (System.UInt32)(Texture2D.width * Texture2D.height);
			index = (System.UInt32)(0x400000);
			while (index > 0x0) {
				index -= 1;
				pretty[index] = Color[((index / 0x800) * width + (index % 0x800)) % height];
			}
			texture.SetPixels(pretty,0x0);
			texture.Apply(false,false);
		}
		public void update (System.UInt64 import) {

			set(keyboard.alias.blank,UnityEngine.Color.gray,UnityEngine.Color.white);
			property dir_x = property.positive;
			property dir_y = property.inactive;
			switch (import) {
			case keyboard.alias.left:
				dir_x = property.negative;
				dir_y = property.inactive;
				break;

			case keyboard.alias.right:
				dir_x = property.positive;
				dir_y = property.inactive;
				break;

			case keyboard.alias.down:
				dir_x = property.inactive;
				dir_y = property.negative;
				break;

			case keyboard.alias.up:
				dir_x = property.inactive;
				dir_y = property.positive;
				break;

			case '~':
				dir_x = property.inactive;
				dir_y = property.inactive;
				break;

			case '`':
				dir_x = property.inactive;
				dir_y = property.inactive;
				break;

			case 'O':
				UnityEngine.Debug.Log("Open");
				//			path.text(directory.make , directory.load , "seven.txt" , raw_data);
				dir_x = property.inactive;
				dir_y = property.inactive;
				break;

			case 'S':
				UnityEngine.Debug.Log("Save");
				//			path.text(directory.make , directory.save , "seven.txt" , raw_data);
				dir_x = property.inactive;
				dir_y = property.inactive;
				break;

			case keyboard.alias.delete:
				dir_x = property.inactive;
				dir_y = property.inactive;
				import = (System.Char)(0x02DC);
				import = (System.Char)(0x02DC);
				goto default;

			case keyboard.alias.enter:
				dir_x = property.inactive;
				dir_y = property.negative;
				//function.analyze(view[focus].read_line((System.UInt16)(text_icon/window.many_x)));
				//function.analyze( (System.UInt16)(text_icon/window.many_x*window.many_x) , (System.UInt16)((text_icon+1)/window.many_x*window.many_x) , ref view[focus].text );
				//function.analyze(text_icon, ref hippo);//view[focus].text);
				break;

			case 'Z':
				dir_x = property.inactive;
				dir_y = property.inactive;
				import = (System.Char)(0x02DC);
				goto default;

			default:
				//System.String(view[focus] , text_icon/window.many_x , window.many_x);

				set(import,UnityEngine.Color.black,UnityEngine.Color.white);

				//if (text_icon.x+1 == window.many_x) {
				//	dir_x = property.inactive;
				//}
				break;
			}


			if (dir_x == property.negative) {
				where_x -= 0x1;
			}
			if (dir_x == property.positive) {
				where_x += 0x1;
			}
			if (dir_y == property.negative) {
				where_y -= 0x1;
			}
			if (dir_y == property.positive) {
				where_y += 0x1;
			}

			set(keyboard.alias.cursor,UnityEngine.Color.gray,UnityEngine.Color.white);

			enlarge();
			texture.SetPixels(bigger,0x0);
			texture.Apply(false,false);
		}

		private void set (System.UInt64 import,UnityEngine.Color color1,UnityEngine.Color color2) {
			draw(import,(System.Byte)(where_x),(System.Byte)(where_y),color1,color2);
		}

		public void draw (System.UInt64 glyph,System.Byte index_x,System.Byte index_y,UnityEngine.Color color1,UnityEngine.Color color2) {
			System.UInt32 offset;
			System.UInt16 index_a;
			System.Byte index_b;
			offset = (System.UInt32)((index_x * 0x8) + (System.UInt32)(index_y * 0x4000));
			index_a = 0x4000;
			while (index_a > 0x0) {
				index_a -= 0x800;
				index_b = 0x8;
				while (index_b > 0x0) {
					index_b -= 0x1;
					pretty[offset + index_a + index_b] = ((glyph & 0x1) != 0x0) ? color1 : color2;
					glyph >>= 0x1;
				}
			}
		}


		public void enlarge () {
			System.Int32 size;
			System.Int32 index_a;
			System.Int32 index_b;
			System.Int32 index_c;
			System.Int32 index_d;
			size = 0x10;
			size += 0x1;
			index_a = 0x0;
			while (index_a < (0x800 / size)) {
				index_b = 0x0;
				while (index_b < (0x800 / size)) {
					index_c = 0x0;
					while (index_c < size) {
						index_d = 0x0;
						while (index_d < size) {
							bigger[(((index_a * size) + index_c) * 0x800) + ((index_b * size) + index_d)] = pretty[(index_a * 0x800) + index_b];
							index_d += 0x1;
						}
						index_c += 0x1;
					}
					index_b += 0x1;
				}
				index_a += 0x1;
			}
		}

	}
}
