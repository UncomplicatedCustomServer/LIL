namespace UncomplicatedCustomAbilities.LIL.Features.Script.Results
{
    internal class Error : Result
    {
        public Error(string error)
        {
            throw new System.Exception(error);
        }
    }
}
