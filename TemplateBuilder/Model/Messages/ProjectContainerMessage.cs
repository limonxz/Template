using GalaSoft.MvvmLight.Messaging;
using TemplateBuilder.Views;

namespace TemplateBuilder.Model.Messages
{
    public class ProjectContainerMessage : MessageBase
    {
        /// <summary>
        /// For get the ProjectView from other VW
        /// </summary>
        public ProjectView ProjectViewContainer { get; set; }
    }
}
