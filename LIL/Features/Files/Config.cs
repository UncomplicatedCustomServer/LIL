namespace LIL.Features.Files
{
    internal class Config(string path) : BaseFile(path)
    {
        public override string Extension => "conf";
    }
}
