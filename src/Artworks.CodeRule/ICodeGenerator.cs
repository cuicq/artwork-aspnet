
namespace Artworks.CodeRule
{
    /// <summary>
    ///  编号生成器接口。
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// 生成编号。
        /// </summary>
        string Generate(GenerateContext context);
    }
}
