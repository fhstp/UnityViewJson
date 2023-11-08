namespace At.Ac.FhStp.ViewJson
{
    /// <summary>
    /// Contains format information about a json element
    /// </summary>
    public abstract class DataFormat
    {
        internal DataFormat()
        {
        }


        internal class Container : DataFormat
        {
        }

        public static DataFormat EmptyContainer = new Container();

        public static DataFormat Default = EmptyContainer;
    }
}