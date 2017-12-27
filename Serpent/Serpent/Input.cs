﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Serpent {
	class Input {
		private static Hashtable keyTable = new Hashtable();
		
		public static bool KeyPressed(Keys key) {
			//return keyTable[key] == null ? false : (bool) keyTable[key];

			if (keyTable[key] == null) {
				return false;
			}

			return (bool) keyTable[key];
		}
		
		public static void ChangeState(Keys key, bool state) {
			keyTable[key] = state;
		}
	}
}
