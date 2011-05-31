//
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

		#if !SILVERLIGHT
		internal OggPacket (int streamIndex, ogg_packet packet)
			: this (streamIndex, packet.packetno, packet.granulepos, packet.packet)
		{
		}
		#endif

		public long Granule
		{
			get;
			private set;
		}

		#if !SILVERLIGHT
		internal ogg_packet ToInternalPacket (OggPacket packet)
		{
			ogg_packet p = new ogg_packet();
			p.packetno = packet.PacketNumber;
			p.bytes = (IntPtr)packet.Data.Length;
			p.packet = packet.Data;
			p.granulepos = packet.Granule;

			return p;
		}
		#endif
	}

	#if !SILVERLIGHT
	internal struct ogg_packet
	{
		public byte[] packet;
		public IntPtr bytes;
		public IntPtr b_o_s;
		public IntPtr e_o_s;

		public long granulepos;
		public long packetno;
	}
	#endif
}