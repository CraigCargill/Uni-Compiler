namespace Compiler.Nodes
{
    /// <summary>
    /// A node corresponding to a noElseCommandNode command
    /// </summary>
    public class noElseCommandNode : ICommandNode
    {
        /// <summary>
        /// The position in the code where the content associated with the node begins
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Creates a new noElseCommandNode command node
        /// </summary>
        /// <param name="position">The position in the code where the content associated with the node begins</param>
        public noElseCommandNode(Position position)
        {
            Position = position;
        }
    }
}