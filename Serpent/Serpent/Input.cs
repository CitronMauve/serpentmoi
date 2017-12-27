using System.Collections;
using System.Windows.Forms;

namespace Serpent {
	class Input {
		private static Hashtable keyTable = new Hashtable();
		
		public static bool KeyPressed(Keys key) {
			return keyTable[key] == null ? false : (bool) keyTable[key];
		}
		
		public static void ChangeState(Keys key, bool state) {
			keyTable[key] = state;
		}
	}
}
