﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyoutaPluginBase;

namespace HyoutaTools.Tales.Vesperia.SaveData {
	// 0x100 bytes in all versions
	public class SaveDataBlockFieldCamera {
		public DuplicatableStream Stream;

		public SaveDataBlockFieldCamera( DuplicatableStream blockStream ) {
			Stream = blockStream.Duplicate();
		}
	}
}
