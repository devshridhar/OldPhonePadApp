using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace OldPhonePadApp
{
    /// <summary>
    /// Processes inputs for an old phone keypad to decode messages.
    /// </summary>
    public class OldPhonePadProcessor
    {
        private readonly ILogger<OldPhonePadProcessor> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="OldPhonePadProcessor"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        public OldPhonePadProcessor(ILogger<OldPhonePadProcessor> logger)
        {
            _logger = logger;
        }

        private static readonly Dictionary<char, string> KeypadMapping = new()
        {
            { '1', string.Empty },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" },
            { '0', " " }
        };

        /// <summary>
        /// Processes the input string according to old phone pad rules and decodes it into a message.
        /// </summary>
        /// <param name="input">The input string containing key presses.</param>
        /// <returns>A decoded message as a string.</returns>
        public string ProcessInput(string input)
        {
            if (string.IsNullOrEmpty(input) || input[^1] != '#')
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            char? currentKey = null;
            int pressCount = 0;
            int consecutiveStars = 0;

            foreach (var ch in input)
            {
                switch (ch)
                {
                    case '#':
                        if (currentKey.HasValue)
                        {
                            AppendKeyPress(result, currentKey.Value, pressCount);
                        }
                        _logger.LogInformation("Final decoded message: {Result}", result.ToString());
                        return result.ToString();

                    case '*':
                        consecutiveStars++;
                        if (consecutiveStars > 1 && result.Length > 0)
                        {
                            result.Length--; // Remove one character for each '*'
                        }
                        _logger.LogInformation("Backspace encountered. Consecutive stars: {ConsecutiveStars}, Current Result: {Result}", consecutiveStars, result.ToString());
                        currentKey = null;
                        pressCount = 0;
                        break;

                    case ' ':
                        consecutiveStars = 0;
                        if (currentKey.HasValue)
                        {
                            AppendKeyPress(result, currentKey.Value, pressCount);
                            currentKey = null;
                            pressCount = 0;
                        }
                        break;

                    default:
                        consecutiveStars = 0;
                        if (KeypadMapping.ContainsKey(ch))
                        {
                            if (ch == currentKey)
                            {
                                pressCount++;
                            }
                            else
                            {
                                if (currentKey.HasValue)
                                {
                                    AppendKeyPress(result, currentKey.Value, pressCount);
                                }
                                currentKey = ch;
                                pressCount = 1;
                            }
                        }
                        break;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Appends the character corresponding to the given key press to the result.
        /// </summary>
        /// <param name="result">The string builder that holds the decoded message.</param>
        /// <param name="key">The key that was pressed on the keypad.</param>
        /// <param name="pressCount">The number of times the key was pressed consecutively.</param>
        private void AppendKeyPress(StringBuilder result, char key, int pressCount)
        {
            if (KeypadMapping.TryGetValue(key, out var letters) && !string.IsNullOrEmpty(letters))
            {
                int index = (pressCount - 1) % letters.Length;
                result.Append(letters[index]);
            }
        }
    }
}
