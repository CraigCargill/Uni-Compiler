namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to an if command
    /// </summary>
    public class IfCommandNode : ICommandNode
    {
        /// <summary>
        /// The condition expression
        /// </summary>
        public IExpressionNode Expression { get; }

        /// <summary>
        /// The then branch command
        /// </summary>
        public ICommandNode ThenCommand { get; }
        
        /// <summary>
        /// The noElse branch command
        /// </summary>
        public noElseCommandNode NoElseCommand { get; }

        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new if command node
        /// </summary>
        /// <param name="expression">The condition expression</param>
        /// <param name="thenCommand">The then branch command</param>
        /// <param name="noElseCommand">The noElse branch command</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public IfCommandNode(IExpressionNode expression, ICommandNode thenCommand, noElseCommandNode noElseCommand, Position position)
        {
            Expression = expression;
            ThenCommand = thenCommand;
            NoElseCommand = noElseCommand;
            Position = position;
        }
    }
}