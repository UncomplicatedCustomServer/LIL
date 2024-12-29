namespace LIL.Features.Files
{
    internal class Info(string path) : BaseFile(path)
    {
        public override string Extension => "info";
    }
}
