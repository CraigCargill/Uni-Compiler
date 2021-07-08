namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a repeat command
    /// </summary>
    public class RepeatCommandNode : ICommandNode
    {
        /// <summary>
        /// The command to be repeated
        /// </summary>
        public ICommandNode Command { get; }


        /// <summary>
        /// The condition associated with the loop
        /// </summary>
        public IExpressionNode UntilExpression { get; }


        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new while node
        /// </summary>
        /// <param name="command">The command to be repeated</param>
        /// <param name="untilExpression">The condition associated with the loop</param>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public RepeatCommandNode(ICommandNode command, IExpressionNode untilExpression, Position position)
        {
            Command = command;
            UntilExpression = untilExpression;
            Position = position;
        }
    }
}