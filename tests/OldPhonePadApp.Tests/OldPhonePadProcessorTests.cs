// -------------------------------------------------------------------------------------------------
// <copyright file="OldPhonePadProcessorTests.cs" company="OldPhonePadApp">
// Copyright (c) OldPhonePadApp. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace OldPhonePadApp.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="OldPhonePadProcessor"/> class.
    /// </summary>
    public class OldPhonePadProcessorTests
    {
        private readonly OldPhonePadProcessor processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="OldPhonePadProcessorTests"/> class.
        /// </summary>
        public OldPhonePadProcessorTests()
        {
            // Use NullLogger to handle logger dependency
            var logger = NullLogger<OldPhonePadProcessor>.Instance;
            processor = new OldPhonePadProcessor(logger);
        }

        /// <summary>
        /// Tests valid inputs and ensures the correct output is returned.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="expected">The expected decoded message.</param>
        [Theory]
        [InlineData("33#", "E")]
        [InlineData("227*#", "B")]
        [InlineData("4433555 555666#", "HELLO")]
        [InlineData("8 88777444666*664#", "TURING")]
        [InlineData("222 2 22#", "CAB")]
        public void ProcessInput_ValidInput_ReturnsExpectedMessage(string input, string expected)
        {
            string result = processor.ProcessInput(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests invalid input (missing '#' at the end) and expects an empty string.
        /// </summary>
        [Fact]
        public void ProcessInput_InvalidInput_ReturnsEmptyString()
        {
            string input = "4433555 555666";
            string result = processor.ProcessInput(input);
            Assert.Equal(string.Empty, result);
        }

        /// <summary>
        /// Tests an empty input string and expects an empty string as output.
        /// </summary>
        [Fact]
        public void ProcessInput_EmptyInput_ReturnsEmptyString()
        {
            string input = string.Empty;
            string result = processor.ProcessInput(input);
            Assert.Equal(string.Empty, result);
        }

        /// <summary>
        /// Tests input with a single backspace and verifies the last character is removed.
        /// </summary>
        [Fact]
        public void ProcessInput_Backspace_RemovesLastCharacter()
        {
            string input = "4433*#";
            string expected = "H";
            string result = processor.ProcessInput(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests input with consecutive backspaces and verifies the behavior is handled correctly.
        /// </summary>
        [Fact]
        public void ProcessInput_ConsecutiveBackspaces_RemovesMultipleCharacters()
        {
            string input = "4433555**#";
            string expected = "H";
            string result = processor.ProcessInput(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests input with more backspaces than characters and verifies no exceptions occur.
        /// </summary>
        [Fact]
        public void ProcessInput_MoreBackspacesThanCharacters_ReturnsEmptyString()
        {
            string input = "4***#";
            string expected = string.Empty;
            string result = processor.ProcessInput(input);
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Tests input where backspaces are mixed with valid characters.
        /// </summary>
        [Fact]
        public void ProcessInput_MixedBackspacesAndCharacters_ReturnsCorrectMessage()
        {
            string input = "22*3*4*#";
            string expected = string.Empty; // All characters are backspaced
            string result = processor.ProcessInput(input);
            Assert.Equal(expected, result);
        }
    }
}
