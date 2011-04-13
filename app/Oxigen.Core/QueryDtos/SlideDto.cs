using System;

namespace Oxigen.Core.QueryDtos
{
    public class SlideDto
    {
        public int Id { get; set; }
		public string Filename { get; set; }
		public string FilenameExtension { get; set; }
		public string FilenameNoPath { get; set; }
		public string GUID { get; set; }
		public char SubDir { get; set; }
		public string SlideName { get; set; }
		public string Creator { get; set; }
		public string Caption { get; set; }
		public string ClickThroughURL { get; set; }
		public string WebsiteURL { get; set; }
		public float DisplayDuration { get; set; }
		public int Length { get; set; }
		public string ImagePath { get; set; }
		public string ImagePathWinFS { get; set; }
		public string ImageFilename { get; set; }
		public string PlayerType { get; set; }
		public string PreviewType { get; set; }
		public bool bLocked { get; set; }
		public DateTime UserGivenDate { get; set; }
		public DateTime AddDate { get; set; }
		public DateTime EditDate { get; set; }
		public DateTime MadeDirtyLastDate { get; set; }
    }
}
