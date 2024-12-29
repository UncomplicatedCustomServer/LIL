namespace LIL.Features.Script.Results
{
    internal class ExecuteRcp(int rcpIp, bool async = false) : Result
    {
        public readonly int Id = rcpIp;
        public readonly bool Async = async;
    }
}
