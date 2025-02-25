namespace Common.Data
{
    public class TrainAction
    {
        public int Code { get; init; }

        public string Abbr { get; init; }

        public string Name { get; init; }

        public TrainAction(int code, string abbr, string name)
        {
            Code = code;
            Abbr = abbr;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
