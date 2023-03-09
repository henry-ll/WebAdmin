namespace CodeGenerator
{
    /// <summary>
    /// 表单字段信息
    /// </summary>
    public class FormFieldModel
    {
        /// <summary>
        /// 字段标识
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 字段验证
        /// </summary>
        public string Validator { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string tp { get; set; }
        /// <summary>
        /// 合并列
        /// </summary>
        public int? ControlColspan { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string Validatorstr { get; set; }
    }
}
