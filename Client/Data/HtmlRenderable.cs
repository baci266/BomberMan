namespace BomberMan.Client.Data
{
    public interface HtmlRenderable
    {
        /// <summary>
        /// Gets class for html atribute class
        /// </summary>
        public string GetCssClass();

        /// <summary>
        /// Gets styles for html atribute style
        /// </summary>
        public string GetCssStyles();
    }
}