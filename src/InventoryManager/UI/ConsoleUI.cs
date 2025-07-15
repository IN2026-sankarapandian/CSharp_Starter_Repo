using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignments
{
    /// <summary>
    /// Has functions related to console manipulations
    /// </summary>
    public class ConsoleUI
    {
        /// <summary>
        /// Gets the prompt and returns the user input for the prompt
        /// </summary>
        /// <param name="prompt">Prompt shown to the user</param>
        /// <returns>User input</returns>
        public static string? PromptAndGetInput(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Prompt the user with info with color
        /// </summary>
        /// <param name="info">Info to shown</param>
        /// <param name="color">Color to set</param>
        public static void PromptInfo(string info, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(info);
            Console.ResetColor();
        }
    }
}
