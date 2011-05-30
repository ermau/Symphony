using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Symphony.Xiph.Vorbis.Wrapper
{
	internal static class Vorbis
	{
		[DllImport ("libvorbis.dll")]
		public static extern void vorbis_info_init();
	}
}