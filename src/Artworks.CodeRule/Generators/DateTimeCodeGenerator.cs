using System;

namespace Artworks.CodeRule.Generators
{
    /// <summary>
    /// 日期编号生成器。
    /// </summary>
    public sealed class DateTimeCodeGenerator : ICodeGenerator
    {
        private readonly string format;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public DateTimeCodeGenerator(string format)
        {
            Guard.ArgumentNotNullOrEmpty(format, "format");
            this.format = format;
        }

        /// <inheritdoc />
        public string Generate(GenerateContext context)
        {
            return DateTime.Now.ToString(format);
        }
    }
}
