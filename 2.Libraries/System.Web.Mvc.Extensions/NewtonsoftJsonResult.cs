using Newtonsoft.Json;
using System.Text;

namespace System.Web.Mvc
{
    /// <summary>
    /// Represents a class that is used to send json content with Newtonsoft serializer to the response.
    /// </summary>
    public class NewtonsoftJsonResult : ActionResult
    {
        /// <summary>
        /// Gets or sets the content encoding.
        /// </summary>
        /// <value>
        /// The content encoding.
        /// </value>
        public Encoding ContentEncoding { get; set; }
        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public object Data { get; set; }
        /// <summary>
        /// Gets or sets the serializer settings.
        /// </summary>
        /// <value>
        /// The serializer settings.
        /// </value>
        public JsonSerializerSettings SerializerSettings { get; set; }
        /// <summary>
        /// Gets or sets the formatting.
        /// </summary>
        /// <value>
        /// The formatting.
        /// </value>
        public Formatting Formatting { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonResult"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="formatting">The formatting.</param>
        public NewtonsoftJsonResult(object data, Formatting formatting) : this(data)
        {
            Formatting = formatting;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonResult"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public NewtonsoftJsonResult(object data) : this()
        {
            Data = data;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NewtonsoftJsonResult"/> class.
        /// </summary>
        public NewtonsoftJsonResult()
        {
            Formatting = Formatting.None;
            SerializerSettings = new JsonSerializerSettings();
        }
        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data == null)
            {
                return;
            }
            var writer = new JsonTextWriter(response.Output) { Formatting = Formatting };
            var serializer = JsonSerializer.Create(SerializerSettings);
            serializer.Serialize(writer, Data);
            writer.Flush();
        }

    }
}
