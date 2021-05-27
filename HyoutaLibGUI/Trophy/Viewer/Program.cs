﻿using HyoutaTools.Trophy.Viewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyoutaLibGUI.Trophy.Viewer {
	class Program {
		public static int Execute( List<string> args ) {
			if ( args.Count < 1 ) {
				System.Windows.Forms.MessageBox.Show("Requires 1 argument, which is the path to the trophy folder.");
				return -1;
			}

			String path = args[0];
			GameFolder GF = new GameFolder( path );
			GameSelectForm GameForm = new GameSelectForm( GF );
			GameForm.Show();

			return 0;
		}
	}
}
