﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HyoutaTools.Other.NitroidDataBinEx {
	class Program {
		static void Execute( string[] args ) {
			if ( args.Length != 1 ) {
				Console.WriteLine( "Usage: NitroidDataBinEx data.bin" );
				Console.WriteLine( "       Or drag & drop data.bin on this file" );
				return;
			}
			String filepath = args[0];
			byte[] File = System.IO.File.ReadAllBytes( filepath );

			if ( !( File[0] == 0x50 && File[1] == 0x41 && File[2] == 0x43 && File[3] == 0x4B ) ) {
				Console.WriteLine( "File is not the Nitroid data.bin or a file of the same format, exiting." );
				return;
			}

			int FileAmount = BitConverter.ToInt32( File, 4 );
			DataBinFileInfo.CreateDirectory( "data.bin.ex" );
			for ( int i = 0; i < FileAmount; i++ ) {
				new DataBinFileInfo( File, 0x08 + ( i * 0x50 ) ).ExtractFile( "data.bin.ex\\" );
			}

			return;
		}
	}
}
