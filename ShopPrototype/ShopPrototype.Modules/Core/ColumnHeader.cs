namespace ShopPrototype.Modules.Core
{
	public class ColumnHeader
	{
		public string DisplayName { get; set; }

		public string CurrentSort { get; set; }

		public string SortKey { get; set; }

		string DescendingKey
		{
			get
			{
				return string.Format("{0}_desc", SortKey);
			}
		}

		string AscendingKey
		{
			get
			{
				return string.Format("{0}_asc", SortKey);
			}
		}

		public string SortCommand
		{
			get
			{
				if (CurrentSort == AscendingKey)
					return DescendingKey;
				else if (CurrentSort == DescendingKey)
					return AscendingKey;

				return AscendingKey;
			}
		}

		public string BootstrapGlyphiconClass
		{
			get
			{
				if (CurrentSort == AscendingKey)
					return "glyphicon glyphicon-sort-by-attributes";
				else if (CurrentSort == DescendingKey)
					return "glyphicon glyphicon-sort-by-attributes-alt";

				return "glyphicon glyphicon-none";
			}
		}
	}
}
