﻿//
// MIT License
// Copyright ©2011 Eric Maupin
// All rights reserved.

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Runtime.InteropServices;
using Symphony.Encoding;

namespace Symphony.Xiph.Ogg
{
	public class OggPacket
		: Packet
	{
		internal OggPacket (int streamIndex, long packetNo, long granule, byte[] data)
			: base (streamIndex, packetNo, data)
		{
			Granule = granule;
		}

		public long Granule
		{
			get;
			private set;
		}
	}

	#if !SILVERLIGHT
	[StructLayout (LayoutKind.Sequential)]
	internal struct ogg_packet
	{
		public IntPtr packet;
		
		public int bytes;
		public int b_o_s;
		public int e_o_s;

		public long granulepos;
		public long packetno;
	}
	#endif
}