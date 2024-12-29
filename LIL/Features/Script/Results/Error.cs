namespace LIL.Features.Script.Results
{
    internal class Error : Result
    {
        public Error(string error, bool fatal = true)
        {
            if (fatal)
                throw new System.Exception(error);
        }
    }
}
