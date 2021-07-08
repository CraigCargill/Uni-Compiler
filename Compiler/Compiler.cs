﻿using Compiler.CodeGeneration;
using Compiler.IO;
using Compiler.Nodes;
using Compiler.SemanticAnalysis;
using Compiler.SyntacticAnalysis;
using Compiler.Tokenization;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace Compiler
{
    /// <summary>
    /// Compiler for code in a source file
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// The file reader
        /// </summary>
        public IFileReader Reader { get; }

        /// <summary>
        /// The tokenizer
        /// </summary>
        public Tokenizer Tokenizer { get; }

        /// <summary>
        /// The parser
        /// </summary>
        public Parser Parser { get; }

        /// <summary>
        /// The identifier
        /// </summary>
        public DeclarationIdentifier Identifier { get; }

        /// <summary>
        /// The type checker
        /// </summary>
        public TypeChecker Checker { get; }

        /// <summary>
        /// The code generator
        /// </summary>
        public CodeGenerator Generator { get; }

        /// <summary>
        /// The target code writer
        /// </summary>
        public TargetCodeWriter Writer { get; }

        /// <summary>
        /// Creates a new compiler
        /// </summary>
        /// <param name="inputFile">The file containing the source code</param>
        /// <param name="binaryOutputFile">The file to write the binary target code to</param>
        /// <param name="textOutputFile">The file to write the text asembly code to</param>
        public Compiler(string inputFile, string binaryOutputFile, string textOutputFile)
        {
            Reporter = new ErrorReporter();
            Reader = new FileReader(inputFile);
            Tokenizer = new Tokenizer(Reader, Reporter);
            Parser = new Parser(Reporter);
            Identifier = new DeclarationIdentifier(Reporter);
            Checker = new TypeChecker(Reporter);
            Generator = new CodeGenerator(Reporter);
            Writer = new TargetCodeWriter(binaryOutputFile, textOutputFile, Reporter);
        }

        /// <summary>
        /// Performs the compilation process
        /// </summary>
        public void Compile()
        {
            // Tokenize
            Write("Tokenising...\n");
            List<Token> tokens = Tokenizer.GetAllTokens();
            WriteLine(Reporter.Errors + " Errors so far");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            // Parse
            Write("Parsing...\n");
            ProgramNode tree = Parser.Parse(tokens);
            WriteLine(Reporter.Errors + " Errors so far");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            // Identify
            Write("Identifying...\n");
            Identifier.PerformIdentification(tree);
            WriteLine(Reporter.Errors + " Errors so far");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            // Type check
            Write("Type Checking...\n");
            Checker.PerformTypeChecking(tree);
            WriteLine(Reporter.Errors + " Errors so far");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            // Code generation
            Write("Generating code...\n");
            TargetCode targetCode = Generator.GenerateCodeFor(tree);
            WriteLine(Reporter.Errors + " Errors so far");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            // Output
            Write("Writing to file...\n");
            Writer.WriteToFiles(targetCode);
            WriteLine(Reporter.Errors + " Errors so far");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            WriteLine(Reporter.Errors + " Errors in final compile");
            WriteLine(TreePrinter.ToString(tree));
        }

        /// <summary>
        /// Writes a message reporting on the success of compilation
        /// </summary>
        private void WriteFinalMessage()
        {
            WriteLine("Compilation Finished");
        }

        /// <summary>
        /// Compiles the code in a file
        /// </summary>
        /// <param name="args">Should be three arguments - input file (*.tri), binary output file (*.tam), text output file (*.txt)</param>
        public static void Main(string[] args)
        {

            if (args == null | args.Length != 3 | args[0] == null | args[1] == null | args[2] == null)
                WriteLine("ERROR: Must call the program with exactly three arguments - input file (*.tri), binary output file (*.tam), text output file (*.txt)");
            else if (!File.Exists(args[0]))
                WriteLine($"ERROR: The input file \"{Path.GetFullPath(args[0])}\" does not exist");
            else
            {
                string inputFile = args[0];
                string binaryOutputFile = args[1];
                string textOutputFile = args[2];
                Compiler compiler = new Compiler(inputFile, binaryOutputFile, textOutputFile);
                WriteLine("Compiling...");
                compiler.Compile();
                compiler.WriteFinalMessage();
            }
        }
    }
}
