
namespace Artworks.Infrastructure
{
    /// <summary>
    /// 系统资源。
    /// </summary>
    internal class Resource
    {
        public const string COOKIE_NAME = @"TOKEN";//cookie

        //xpath
        public const string CONFIG_XPATH_APPLICATION = "configuration/application/add";//默认


        public const string EXCEPTION_NOTFOUND_VALIDATOR = "未找实现{0}验证实体类。";
        public const string EXCEPTION_QUERY_WHERECLAUSE = "QueryObject：查询条件语句个数和连接词个数不匹配。";
        public const string EXCEPTION_QUERY_CONNECTOR = "QueryObject：查询筛选器连接器未定义。";
        public const string EXCEPTION_QUERY_CLAUSE_INORNOTIN = "QueryObject：查询(in/not in)只支持int,string类型。";

        //服务层提示
        public const string SERVICE_VALIDATE_OBJECT_TEMPT = @"输入对象为空.";
        public const string SERVICE_OPERATE_FAIL_REPEAT_RECORD = @"操作失败，数据重复 .";
        public const string SERVICE_OPERATE_FAIL = @"操作失败.";
        public const string SERVICE_OPERATE_SUCCEED = @"操作成功.";
    }
}
