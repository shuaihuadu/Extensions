using System.Drawing;
using System.Drawing.Imaging;

namespace System.Web.Mvc
{
    /// <summary>
    /// Represents a class that is used to send image content to the response.
    /// </summary>
    public class ImageResult : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the System.Web.Mvc.ImageResult class.
        /// </summary>
        public ImageResult()
        {
        }
        /// <summary>
        /// Gets or sets the image content.
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Gets or sets the image format.
        /// </summary>
        public ImageFormat ImageFormat { get; set; }
        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits
        /// from the System.Web.Mvc.ActionResult class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (null == Image)
            {
                throw new ArgumentNullException("Image");
            }
            if (null == ImageFormat)
            {
                throw new ArgumentNullException("ImageFormat");
            }
            context.HttpContext.Response.Clear();
            if (ImageFormat.Equals(ImageFormat.Bmp)) context.HttpContext.Response.ContentType = "image/bmp";
            if (ImageFormat.Equals(ImageFormat.Gif)) context.HttpContext.Response.ContentType = "image/gif";
            if (ImageFormat.Equals(ImageFormat.Icon)) context.HttpContext.Response.ContentType = "image/vnd.microsoft.icon";
            if (ImageFormat.Equals(ImageFormat.Jpeg)) context.HttpContext.Response.ContentType = "image/jpeg";
            if (ImageFormat.Equals(ImageFormat.Png)) context.HttpContext.Response.ContentType = "image/png";
            if (ImageFormat.Equals(ImageFormat.Tiff)) context.HttpContext.Response.ContentType = "image/tiff";
            if (ImageFormat.Equals(ImageFormat.Wmf)) context.HttpContext.Response.ContentType = "image/wmf";
            Image.Save(context.HttpContext.Response.OutputStream, ImageFormat);
        }
    }
}
