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

namespace Symphony
{
	public class Location
	{
		public Location (Location location)
		{
			X = location.X;
			Y = location.Y;
			Z = location.Z;
		}

		public Location (float x, float y, float z = 0)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public float X
		{
			get;
			private set;
		}

		public float Y
		{
			get;
			private set;
		}

		public float Z
		{
			get;
			private set;
		}

		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		public bool Equals (Location other)
		{
			if (ReferenceEquals (null, other))
				return false;
			if (ReferenceEquals (this, other))
				return true;
			return other.X.Equals (this.X) && other.Y.Equals (this.Y) && other.Z.Equals (this.Z);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = this.X.GetHashCode();
				result = (result * 397) ^ this.Y.GetHashCode();
				result = (result * 397) ^ this.Z.GetHashCode();
				return result;
			}
		}
	}
}