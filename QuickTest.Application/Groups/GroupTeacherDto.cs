﻿using QuickTest.Core.Entities;

namespace QuickTest.Application.Groups
{
    public class GroupTeacherDto
    {
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}