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
using System.IO;

namespace Symphony.Encoding.Formats.Raw
{
	public class RawParser
		: IFormatParser
	{
		private readonly Stream stream;

		public RawParser (Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException ("stream");

			this.stream = stream;
		}

		public bool TryReadPacket (out Packet packet)
		{
			packet = null;

			byte[] data;
			if (this.stream.ReadBytes (out data, 480) > 0)
			{
				packet = new RawPacket (data);
				return true;
			}

			return false;
		}

		public bool TryReadContext (out IFormatContext context)
		{
			context = null;
			return false;
		}
	}

	public class RawPacket
		: Packet
	{
		public RawPacket (byte[] data)
			: base (0)
		{
			if (data == null)
				throw new ArgumentNullException ("data");

			this.data = data;
		}

		private readonly byte[] data;
	}
}