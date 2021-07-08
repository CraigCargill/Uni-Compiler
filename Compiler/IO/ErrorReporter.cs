using System;
using System.Collections.Generic;
using static System.Console;

namespace Compiler.IO
{
    /// <summary>
    /// An object for reporting errors in the compilation process
    /// </summary>
    public class ErrorReporter
    {
        /// <summary>
        /// Whether or not any errors have been encountered
        /// </summary>
        public bool HasErrors { get; set; }

        public Position CurrentErrorPosition { get; set; }

        public int Errors { get; set; }

        public Dictionary<Position, string> errorsDict { get; set;}

        public ErrorReporter()
        {
            HasErrors = false;
            CurrentErrorPosition = new Position(0, 0);
            Errors = 0;
            errorsDict = new Dictionary<Position, string>();
        }

        public override string ToString()
        {
            return CurrentErrorPosition.ToString();
        }

        public void AddError(Position pos, String information)
        {
            string output = "ERROR " + information;
            if (pos.PositionInLine < 0)
            {
                output += " in low level system";
            }
            else
            {
                output += " at position " + pos;
            }
            WriteLine(output);
            Debugger.Write(output);
            this.Errors++;
            if (!errorsDict.ContainsKey(pos))
            {
                HasErrors = true;
                errorsDict.Add(pos, information);
            }
            
        }

        public string ReportErrorCount()
        {
            if (Errors > 0)
            {
                return "[ERROR] Comp failed" + this.Errors + " error(s)";
            }
            else
            {
                return "[SUCCESS} Comp successful, no errors";
            }
        }


        public string ReportError()
        {
            return "[Error] error at " + this.CurrentErrorPosition.ToString();
        }

    }
}