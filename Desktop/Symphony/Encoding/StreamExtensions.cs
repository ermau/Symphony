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

using System.IO;

namespace Symphony.Encoding
{
	public static class StreamExtensions
	{
		public static int ReadBytes (this Stream stream, byte[] buffer, int offset, int size)
		{
			int originalSize = size;
			int i = offset;
			int bytes = 0;
			while (i < buffer.Length && (bytes = stream.Read (buffer, i, size)) > 0)
			{
				i += bytes;
				size -= bytes;
			}

			return originalSize - size;
		}

		public static int ReadBytes (this Stream stream, out byte[] buffer, int size)
		{
			buffer = new byte[size];
			return ReadBytes (stream, buffer, 0, size);
		}
	}
}
