﻿
namespace BitStack {

	/**
     * Contains useful extension methods for the uint Value datatype.
     * uint is an unsigned 32 bit value.
     * 
     * For more info visit https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/uint
     */
	public static class ValueUIntExtensions {

		/**
         * Simple method to get a simple true/false value from data
         */
		public static bool Bool(this uint data) {
			return data > 0;
		}

		/**
         * Return the state of the bit (either 1 or 0) at provided
         * position. position value must be between [0, 31]
         */
		public static int BitAt(this uint data, int pos) {
			return (int)((data >> pos) & 1);
		}

		/**
         * Sets the state of the bit into the ON/1 at provided
         * position. position value must be between [0, 31]
         */
		public static uint SetBitAt(this uint data, int pos) {
			return (data | 1u << pos);
		}

		/**
         * Sets the state of the bit into the OFF/0 at provided
         * position. position value must be between [0, 31]
         */
		public static uint UnsetBitAt(this uint data, int pos) {
			return (data & ~(1u << pos));
		}

		/**
         * Toggles the state of the bit into the ON/1 or OFF/0 at provided
         * position. position value must be between [0, 31].
         */
		public static uint ToggleBitAt(this uint data, int pos) {
			return (data ^ (1u << pos));
		}

		/**
         * Count the number of set bits in the provided uint value (32 bits)
         * A general purpose Hamming Weight or popcount function which returns the number of
         * set bits in the argument.
         */
		public static int PopCount(this uint data) {
			data = data - ((data >> 1) & 0x55555555);
			data = (data & 0x33333333) + ((data >> 2) & 0x33333333);

			return (int)((((data + (data >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24);
		}

		/**
         * Checks if the provided value is a power of 2.
         */
		public static bool IsPowerOfTwo(this uint value) {
			return value != 0 && (value & value - 1) == 0;
		}

		/**
         * Returns the String representation of the Bit Sequence from the provided
         * Int. The String will contain 32 characters of 1 or 0 for each bit position
         */
		public static string BitString(this uint value) {
#pragma warning disable XS0001 // Find APIs marked as TODO in Mono
			var stringBuilder = new System.Text.StringBuilder(32);
#pragma warning restore XS0001 // Find APIs marked as TODO in Mono

			for (int i = 31; i >= 0; i--) {
				stringBuilder.Append(value.BitAt(i));
			}

			return stringBuilder.ToString();
		}

		/**
         * Given a string in binary form ie (10110101) convert into
         * a byte and return. Will only look at the first 8 characters
         */
		public static uint UIntFromBitString(this string data, int readIndex) {
			uint value = 0;

			for (int i = readIndex, j = 31; i < 32; i++, j--) {
				value = data[i] == '1' ? value.SetBitAt(j) : value.UnsetBitAt(j);
			}

			return value;
		}

		/**
         * Given a string in binary form ie (10110101) convert into
         * a byte and return. Will only look at the first 8 characters
         */
		public static uint UIntFromBitString(this string data) {
			return data.UIntFromBitString(0);
		}

		/**
         * Returns the Hex Value as a String.
         */
		public static string HexString(this uint value) {
			return value.ToString("X");
		}
	}
}
