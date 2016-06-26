namespace Zone.Grid
{
    using System;
    using System.Collections.Generic;

    public class Grid
    {
        public string Name { get; set; }

        public List<Section> Sections { get; set; }

        public class Section
        {
            public int Grid { get; set; }

            public List<Row> Rows { get; set; }

            public class Row
            {
                public string Name { get; set; }

                public List<Area> Areas { get; set; }

                public Guid Id { get; set; }

                public class Area
                {
                    public int Grid { get; set; }

                    public bool AllowAll { get; set; }

                    public List<Control> Controls { get; set; }

                    public class Control
                    {
                        public object Value { get; set; }

                        public GridEditor Editor { get; set; }

                        public object TypedValue { get; set; }

                        public class GridEditor
                        {
                            public string Name { get; set; }

                            public string Alias { get; set; }

                            public string View { get; set; }

                            public object Render { get; set; }

                            public string Icon { get; set; }
                        }
                    }
                }
            }
        }
    }
}